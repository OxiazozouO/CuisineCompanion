using CuisineCompanion.WebApi.Constant;
using CuisineCompanion.WebApi.DataModel;
using CuisineCompanion.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CuisineCompanion.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class RecipeInfoController : MyControllerBase
{
    public RecipeInfoController(RecipeManagementSystemContext db) : base(db)
    {
    }

    [HttpPost]
    public IActionResult RecipeAt(UserSelectionDto dto)
    {
        try
        {
            var recipeDetails = _db.Recipes
                .Where(r => r.RecipeId == dto.Id)
                .Select(r => new
                {
                    r.RecipeId,
                    r.Title,
                    r.RName,
                    FileUri = Url.GetRecipeUrl(Request, r.FileUri),
                    r.Summary
                }).FirstOrDefault();

            if (recipeDetails == null) return Ok(new ApiResponses { Code = -1, Message = "没有找到此食谱" });

            var categories = (from ci in _db.CategoryItems
                join c in _db.Categories on ci.CategoryId equals c.CategoryId
                where ci.IdCategory == IdCategoryConstant.Recipe && ci.TId == dto.Id
                select new
                {
                    c.CategoryId,
                    c.CName
                }).ToList();

            var steps = _db.PreparationSteps
                .Where(step => step.RecipeId == dto.Id)
                .OrderBy(step => step.SequenceNumber)
                .Select(step => new
                {
                    step.Title,
                    FileUri = Url.GetRecipeUrl(Request, step.FileUri),
                    step.Refer,
                    step.RequiredTime,
                    step.RequiredIngredient,
                    step.Summary
                }).ToList();

            var ingredients = (from item in _db.RecipeItems
                join i in _db.Ingredients on item.IngredientId equals i.IngredientId
                where item.RecipeId == dto.Id
                select new
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
                    item.Dosage
                }).ToList();

            var result = new
            {
                recipeDetails.RecipeId,
                recipeDetails.Title,
                recipeDetails.RName,
                recipeDetails.FileUri,
                recipeDetails.Summary,
                Category = categories,
                Steps = steps,
                Ingredients = ingredients
            };

            return Ok(new ApiResponses
            {
                Code = 1,
                Message = "获取食谱详细信息成功",
                Data = result
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok(ApiResponses.ErrorResult);
        }
    }
}