using CuisineCompanion.Models;
using CuisineCompanion.WebApi.Constant;
using CuisineCompanion.WebApi.DataModel;
using CuisineCompanion.WebApi.DTOs;
using CuisineCompanion.WebApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace CuisineCompanion.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class IndexController : MyControllerBase
{
    public IndexController(RecipeManagementSystemContext db) : base(db)
    {
    }

    [HttpPost]
    public IActionResult Search(SearchDto dto)
    {
        try
        {
            if (!UserService.TryVerifyUserToken(_db, dto.UserToken, out var user, out var error)) return Ok(error);

            object data = null;
            var text = dto.Text;
            var flag = (SearchFlags)dto.Flag;
            switch (flag)
            {
                case SearchFlags.Food:
                    data = _db.Ingredients.OrderBy(r => Guid.NewGuid())
                        .Where(i => string.IsNullOrEmpty(text)
                                    || i.IName.Contains(text)
                                    || (i.Refer != null && i.Refer.Contains(text))
                                    || (i.Unit != null && i.Unit.Contains(text))
                                    || (i.Quantity != null && i.Quantity.Contains(text))
                                    || (i.NutritionalComposition != null && i.NutritionalComposition.Contains(text))
                                    || (i.Allergy != null && i.Allergy.Contains(text))
                        ).Select(i => new
                        {
                            i.IngredientId,
                            i.IName,
                            i.Refer,
                            i.Unit,
                            i.Quantity,
                            Nutritional = i.NutritionalComposition,
                            i.Allergy,
                            i.Content,
                            FileUri = Url.GetIngredientUrl(Request, i.FileUri),
                            Dosage = 100
                        }).Take(50)
                        .ToList();
                    break;
                case SearchFlags.Recipe:
                    data = _db.Recipes.OrderBy(r => Guid.NewGuid())
                        .Where(r => string.IsNullOrEmpty(text)
                                    || (r.Title != null && r.Title.Contains(text))
                                    || (r.RName != null && r.RName.Contains(text))
                                    || (r.Summary != null && r.Summary.Contains(text))
                        )
                        .Select(r => new
                        {
                            r.RecipeId,
                            FileUri = Url.GetRecipeUrl(Request, r.FileUri),
                            r.Title,
                            r.Summary
                        })
                        .Take(50)
                        .ToList();
                    break;
                case SearchFlags.Menu:
                    data = _db.Favorites.OrderBy(f => Guid.NewGuid())
                        .Where(f =>
                            f.Authority == FavoriteAuthorityConstant.Public
                            && f.IdCategory == IdCategoryConstant.Recipe
                            && (string.IsNullOrEmpty(text)
                                || (f.CName != null && f.CName.Contains(text))
                                || (f.Refer != null && f.Refer.Contains(text))
                            )).Select(f => new
                        {
                            f.FavoriteId,
                            f.CName,
                            f.Refer,
                            FileUri = Url.GetFavoriteUrl(Request, f.FileUri),
                            Flag = DbFlagsHelper.GetFavoriteFlags((byte)f.Authority, (byte)f.IdCategory),
                            ItemsCount = _db.FavoriteItems
                                .Count(fi => fi.FavoriteId == f.FavoriteId)
                        }).Where(tmp => tmp.ItemsCount > 0)
                        .Take(50)
                        .ToList();
                    break;
                case SearchFlags.Category:
                {
                    var data1 = (from ci in _db.CategoryItems
                        join c in _db.Categories on ci.CategoryId equals c.CategoryId
                        where ci.IdCategory == IdCategoryConstant.Recipe
                        join r in _db.Recipes on ci.TId equals r.RecipeId
                        where string.IsNullOrEmpty(text)
                              || c.CName.Contains(text)
                              || (r.Title != null && r.Title.Contains(text))
                              || (r.RName != null && r.RName.Contains(text))
                              || (r.Summary != null && r.Summary.Contains(text))
                        orderby Guid.NewGuid()
                        group new
                        {
                            r.RecipeId,
                            FileUri = Url.GetRecipeUrl(Request, r.FileUri),
                            r.Title,
                            r.Summary
                        } by new
                        {
                            c.CategoryId,
                            c.CName
                        }
                        into g
                        select new
                        {
                            Category = g.Key,
                            Recipes = g.ToList()
                        }).Take(50).ToList();

                    var data2 = (from ci in _db.CategoryItems
                        join c in _db.Categories on ci.CategoryId equals c.CategoryId
                        where ci.IdCategory == IdCategoryConstant.Ingredient
                        join i in _db.Ingredients on ci.TId equals i.IngredientId
                        where string.IsNullOrEmpty(text)
                              || c.CName.Contains(text)
                              || (i.Refer != null && i.Refer.Contains(text))
                              || (i.Unit != null && i.Unit.Contains(text))
                              || (i.Quantity != null && i.Quantity.Contains(text))
                              || (i.NutritionalComposition != null && i.NutritionalComposition.Contains(text))
                              || (i.Allergy != null && i.Allergy.Contains(text))
                        orderby Guid.NewGuid()
                        group new
                        {
                            i.IngredientId,
                            i.IName,
                            i.Refer,
                            i.Unit,
                            i.Quantity,
                            Nutritional = i.NutritionalComposition,
                            i.Allergy,
                            i.Content,
                            FileUri = Url.GetIngredientUrl(Request, i.FileUri),
                            Dosage = 100
                        } by new
                        {
                            c.CategoryId,
                            c.CName
                        }
                        into g
                        select new
                        {
                            Category = g.Key,
                            Ingredients = g.ToList()
                        }).Take(50).ToList();
                    data = new
                    {
                        Recipes = data1,
                        Ingredients = data2
                    };
                }
                    break;
                default: goto bad;
            }

            return Ok(new ApiResponses
            {
                Code = 1,
                Message = "获取信息成功",
                Data = data
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        bad:
        return Ok(ApiResponses.ErrorResult);
    }
}