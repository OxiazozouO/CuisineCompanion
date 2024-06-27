using CuisineCompanion.WebApi.Constant;
using CuisineCompanion.WebApi.DataModel;
using CuisineCompanion.WebApi.DTOs;
using CuisineCompanion.WebApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace CuisineCompanion.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class IngredientInfoController : MyControllerBase
{
    public IngredientInfoController(RecipeManagementSystemContext db) : base(db)
    {
    }

    [HttpPost]
    public IActionResult IngredientCategory(UserSelectionDto dto)
    {
        try
        {
            if (!UserService.TryVerifyUserToken(_db, dto.UserToken, out var user, out var error))
            {
                return Ok(error);
            }

            var categories = (from ci in _db.CategoryItems
                join c in _db.Categories on ci.CategoryId equals c.CategoryId
                where ci.IdCategory == IdCategoryConstant.Ingredient && ci.TId == dto.Id
                select new
                {
                    c.CategoryId,
                    c.CName
                }).ToList();

            return Ok(new ApiResponses
            {
                Code = 1,
                Message = "获取食谱详细信息成功",
                Data = categories
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok(ApiResponses.ErrorResult);
        }
    }

    [HttpPost]
    public IActionResult IngredientAt(UserSelectionDto dto)
    {
        try
        {
            if (!UserService.TryVerifyUserToken(_db, dto.UserToken, out var user, out var error))
            {
                return Ok(error);
            }

            var i = _db.Ingredients
                .FirstOrDefault(i => i.IngredientId == dto.Id);
            object? ret = null;
            if (i is not null)
                ret = new
                {
                    i.IngredientId,
                    i.IName,
                    i.Refer,
                    i.Unit,
                    i.Quantity,
                    Nutritional = i.NutritionalComposition,
                    i.Allergy,
                    i.Content,
                    Dosage = 100,
                    FileUri = Url.GetIngredientUrl(Request, i.FileUri),
                };
            return Ok(new ApiResponses
            {
                Code = 1,
                Message = "获取食材详细信息成功",
                Data = ret
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok(ApiResponses.ErrorResult);
        }
    }
}