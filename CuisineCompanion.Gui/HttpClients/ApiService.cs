using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CuisineCompanion.Helper;
using CuisineCompanion.Models;
using CuisineCompanion.ViewModels;

namespace CuisineCompanion.HttpClients;

public static class ApiService
{
    public static MyInfoModel? GetMyAllInfo()
    {
        var req = ApiEndpoints.MyAllInfo(new { MainViewModel.UserToken });
        MyInfoModel? myInfo = null;
        if (req.Execute(out var res))
        {
            myInfo = res.Data.ToEntity<MyInfoModel>();
            myInfo.Init();
        }

        return myInfo;
    }

    public static List<EatingDiaryAtViewModel>? GetEatingDiaries()
    {
        var req = ApiEndpoints.GetEatingDiaries(new { MainViewModel.UserToken });
        if (req.Execute(out var res))
        {
            var list = res.Data.ToEntity<List<EatingDiaryModel>>();
            var viewModel =
                list.Select(i => new EatingDiaryAtViewModel { EatingDiary = i }).ToList();
            return viewModel;
        }

        return null;
    }

    public static IngredientModel? GetIngredientAt(int id)
    {
        var req = ApiEndpoints.IngredientAt(new { MainViewModel.UserToken, Id = id });
        IngredientModel? ingredient = null;
        if (req.Execute(out var res))
            ingredient = res.Data.ToEntity<IngredientModel>();
        else
            MsgBoxHelper.TryError(res.Message);

        return ingredient;
    }

    public static RecipeModel? GetRecipeAt(int id)
    {
        var req = ApiEndpoints.RecipeAt(new { MainViewModel.UserToken, Id = id });
        RecipeModel? recipe = null;
        if (req.Execute(out var res))
        {
            recipe = res.Data.ToEntity<RecipeModel>();
            recipe.Init();
            var req2 = ApiEndpoints.IsLike(new { MainViewModel.UserToken, TId = id, Flag = ModelFlags.Recipe });
            if (req2.Execute(out var res2)) recipe.IsLike = bool.Parse(res2.Data.ToString());
        }
        else
        {
            MsgBoxHelper.TryError(res.Message);
        }

        return recipe;
    }

    public static (List<CategoryModel>? category, bool isLike) GetIngredientInfo(int id)
    {
        List<CategoryModel> category = null;
        var isLike = false;

        var req = ApiEndpoints.IngredientCategory(new
            { MainViewModel.UserToken, Id = id });

        if (req.Execute(out var res)) category = res.Data.ToEntity<List<CategoryModel>>();

        var req2 = ApiEndpoints.IsLike(new
            { MainViewModel.UserToken, TId = id, Flag = ModelFlags.Ingredient });
        if (req2.Execute(out var res2)) isLike = bool.Parse(res2?.Data?.ToString() ?? "False");

        return (category, isLike);
    }

    public static ObservableCollection<FavoriteModel> GetUserFavorites()
    {
        var req = ApiEndpoints.UserFavorites(MainViewModel.UserToken);

        if (!req.Execute(out var res)) return null;
        return res.Data.ToEntity<ObservableCollection<FavoriteModel>>();
    }
}