using System.ComponentModel.DataAnnotations;
using CuisineCompanion.Models;
using CuisineCompanion.WebApi.Constant;
using CuisineCompanion.WebApi.DataModel;
using CuisineCompanion.WebApi.DTOs;
using CuisineCompanion.WebApi.Service;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace CuisineCompanion.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class MyEatingDiaryController : MyControllerBase
{
    public MyEatingDiaryController(RecipeManagementSystemContext db) : base(db)
    {
    }

    [HttpPost]
    public IActionResult AddEatingDiary(EatingDiaryAddDto dto)
    {
        if (!DbFlagsHelper.TryGetIdCategory(dto.Flag, out sbyte idCategory))
        {
            return Ok(new ApiResponses { Code = -1, Message = "参数错误" });
        }

        int userid = dto.UserToken.UserId;

        try
        {
            if (!UserService.TryVerifyUserToken(_db, dto.UserToken, out var user, out var error))
            {
                return Ok(error);
            }
            //时间是用户出生之后，100年之前

            if (dto.UpdateTime < user.BirthDate || dto.UpdateTime > user.BirthDate.AddYears(100))
            {
                return Ok(new ApiResponses { Code = -1, Message = "选择的时间错误" });
            }

            var eatingDiary = new EatingDiary
            {
                UserId = userid,
                IdCategory = idCategory,
                UpdateTime = dto.UpdateTime,
                Tid = dto.TId,
                Dosages = dto.Dosages.ToJson(),
                ShortNutrientContent = dto.Nutrients.ToJson(),
            };
            _db.Add(eatingDiary);
            if (_db.SaveChanges() == 1)
            {
                switch (idCategory)
                {
                    case IdCategoryConstant.Recipe:
                        var data1 = _db.Recipes.Where(r => r.RecipeId == dto.TId)
                            .Select(r => new EatingDiaryRetInfoDTO
                            {
                                Flag = ModelFlags.Recipe,
                                TId = r.RecipeId,
                                Name = r.RName,
                                FileUrl = Url.GetRecipeUrl(Request, r.FileUri)
                            }).FirstOrDefault();
                        break;
                    case IdCategoryConstant.Ingredient:
                        var data2 = _db.Ingredients.Where(i => i.IngredientId == dto.TId)
                            .Select(i => new EatingDiaryRetInfoDTO
                            {
                                Flag = ModelFlags.Ingredient,
                                TId = i.IngredientId,
                                Name = i.IName,
                                FileUrl = Url.GetIngredientUrl(Request, i.FileUri)
                            }).ToList();
                        break;
                }

                return Ok(new ApiResponses { Code = 1, Message = "添加食用日记成功", Data = eatingDiary.EdId });
            }

            return Ok(new ApiResponses { Code = -1, Message = "添加食用日记失败" });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Ok(ApiResponses.ErrorResult);
    }

    [HttpPost]
    public IActionResult DeleteEatingDiary(EatingDiaryDeleteDto dto)
    {
        if (!DbFlagsHelper.TryGetIdCategory(dto.Flag, out sbyte idCategory))
        {
            return Ok(new ApiResponses { Code = -1, Message = "参数错误" });
        }

        int userid = dto.UserToken.UserId;
        try
        {
            if (!UserService.TryVerifyUserToken(_db, dto.UserToken, out var user, out var error))
            {
                return Ok(error);
            }

            var eatingDiaries = _db.EatingDiaries.FirstOrDefault(ed =>
                ed.IdCategory == idCategory && ed.UserId == userid && ed.EdId == dto.EdId);
            if (eatingDiaries is null)
            {
                return Ok(new ApiResponses { Code = -1, Message = "食用日记不存在" });
            }

            _db.EatingDiaries.Remove(eatingDiaries);
            if (_db.SaveChanges() == 1)
            {
                return Ok(new ApiResponses { Code = 1, Message = "删除食用日记成功" });
            }

            return Ok(new ApiResponses { Code = -1, Message = "删除食用日记失败" });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Ok(ApiResponses.ErrorResult);
    }

    [HttpPost]
    public IActionResult GetEatingDiaries(AuthenticationDto dto)
    {
        try
        {
            int userid = dto.UserToken.UserId;
            if (!UserService.TryVerifyUserToken(_db, dto.UserToken, out var user, out var error))
            {
                return Ok(error);
            }

            var data = _db.EatingDiaries.Where(ed => ed.UserId == userid)
                .Select(ed => new
                {
                    ed.EdId,
                    ed.Tid,
                    ed.UpdateTime,
                    Flag = DbFlagsHelper.GetFlags((byte)ed.IdCategory),
                    ed.Dosages,
                    Nutrients = ed.ShortNutrientContent
                }).ToList();

            return Ok(new ApiResponses
            {
                Code = 1,
                Message = "食用日记获取成功",
                Data = data
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Ok(ApiResponses.ErrorResult);
    }

    [HttpPost]
    public IActionResult GetEatingEatingDiaryInfos(EatingDiaryInfoDTO dto)
    {
        if (dto.IdFlags.Count == 0)
        {
            return Ok(new ApiResponses { Code = -1, Message = "参数错误" });
        }

        var recipeIds = new List<int>();
        var ingredientIds = new List<int>();
        foreach (var dtoFlag in dto.IdFlags)
        {
            if (!DbFlagsHelper.TryGetIdCategory(dtoFlag.Key, out sbyte idCategory))
            {
                return Ok(new ApiResponses { Code = -1, Message = "参数错误" });
            }

            switch (idCategory)
            {
                case IdCategoryConstant.Recipe:
                    recipeIds.Add(dtoFlag.Value);
                    break;
                case IdCategoryConstant.Ingredient:
                    ingredientIds.Add(dtoFlag.Value);
                    break;
            }
        }

        try
        {
            var data1 = _db.Recipes.Where(r => recipeIds.Contains(r.RecipeId))
                .Select(r => new EatingDiaryRetInfoDTO
                {
                    Flag = ModelFlags.Recipe,
                    TId = r.RecipeId,
                    Name = r.RName,
                    FileUrl = Url.GetRecipeUrl(Request, r.FileUri)
                }).ToList();

            var data2 = _db.Ingredients.Where(i => ingredientIds.Contains(i.IngredientId))
                .Select(i => new EatingDiaryRetInfoDTO
                {
                    Flag = ModelFlags.Ingredient,
                    TId = i.IngredientId,
                    Name = i.IName,
                    FileUrl = Url.GetIngredientUrl(Request, i.FileUri)
                }).ToList();

            data1.AddRange(data2);
            return Ok(new ApiResponses
            {
                Code = 1,
                Message = "获取食用日记详细信息成功",
                Data = data1
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Ok(ApiResponses.ErrorResult);
    }
}