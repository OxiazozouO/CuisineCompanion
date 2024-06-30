using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CuisineCompanion.Helper;
using CuisineCompanion.HttpClients;
using CuisineCompanion.Models;

namespace CuisineCompanion.ViewModels;

public partial class FavoriteItemViewModel : ObservableObject
{
    private readonly Dictionary<string, object> _searchCache = new();
    [ObservableProperty] private ObservableCollection<IngredientInfoViewModel> ingredients;
    [ObservableProperty] private bool isEdit = true;
    [ObservableProperty] private ObservableCollection<RecipeInfoViewModel> menus;
    [ObservableProperty] private ObservableCollection<RecipeInfoViewModel> recipes;
    [ObservableProperty] private FavoriteModel root;
    [ObservableProperty] private string searchText = "";

    partial void OnRootChanged(FavoriteModel? oldValue, FavoriteModel newValue)
    {
        var f = (ModelFlags)Root.Flag;
        var req = ApiEndpoints.FavoriteListItems(new
        {
            MainViewModel.UserToken,
            Root.Flag,
            Root.FavoriteId
        });
        if (!req.Execute(out var res)) return;
        if (f.Exists(ModelFlags.Ingredient))
            Ingredients = IngredientInfoViewModel.Create(res.Data.ToEntity<IEnumerable<IngredientModel>>());
        else if (f.Exists(ModelFlags.Recipe))
            Recipes = res.Data.ToEntity<ObservableCollection<RecipeInfoViewModel>>();
        else if (f.Exists(ModelFlags.Menu)) Menus = res.Data.ToEntity<ObservableCollection<RecipeInfoViewModel>>();
    }

    partial void OnIngredientsChanged(ObservableCollection<IngredientInfoViewModel>? oldValue,
        ObservableCollection<IngredientInfoViewModel> newValue)
    {
        if (_searchCache.Count == 0)
            _searchCache[""] = Ingredients;
    }

    partial void OnRecipesChanged(ObservableCollection<RecipeInfoViewModel>? oldValue,
        ObservableCollection<RecipeInfoViewModel> newValue)
    {
        if (_searchCache.Count == 0)
            _searchCache[""] = Recipes;
    }

    partial void OnMenusChanged(ObservableCollection<RecipeInfoViewModel>? oldValue,
        ObservableCollection<RecipeInfoViewModel> newValue)
    {
        if (_searchCache.Count == 0)
            _searchCache[""] = Menus;
    }

    partial void OnSearchTextChanged(string? oldValue, string newValue)
    {
        Search();
    }


    [RelayCommand]
    private void Search()
    {
        var f = (ModelFlags)Root.Flag;
        var isExists = _searchCache.TryGetValue(SearchText, out var res);

        if (f.Exists(ModelFlags.Ingredient))
        {
            Ingredients = GetSearchResults<IngredientInfoViewModel>(isExists, res, i =>
                (i.Ingredient.IName != null && i.Ingredient.IName.Contains(SearchText))
                || (i.Ingredient.Refer != null && i.Ingredient.Refer.Contains(SearchText))
            );
        }
        else if (f.Exists(ModelFlags.Recipe))
        {
            Recipes = GetSearchResults<RecipeInfoViewModel>(isExists, res, r =>
                (r.Summary != null && r.Summary.Contains(SearchText))
                || (r.Title != null && r.Title.Contains(SearchText))
            );
        }
        else if (f.Exists(ModelFlags.Menu))
        {
            Menus = GetSearchResults<RecipeInfoViewModel>(isExists, res, r =>
                (r.Summary != null && r.Summary.Contains(SearchText))
                || (r.Title != null && r.Title.Contains(SearchText))
            );
        }
    }

    private ObservableCollection<T> GetSearchResults<T>(bool isExists, object res, Func<T, bool> predicate)
    {
        if (isExists)
        {
            return res as ObservableCollection<T>;
        }

        if (_searchCache[""] is ObservableCollection<T> list)
        {
            var results = new ObservableCollection<T>(list.Where(predicate));
            if (results.Count != 0)
                _searchCache[SearchText] = results;
            return results;
        }

        return new ObservableCollection<T>();
    }

    [RelayCommand]
    private void DeleteItem(int id)
    {
        if (!MsgBoxHelper.Confirmation("是否删除？")) return;

        var f = (ModelFlags)Root.Flag;
        var req = ApiEndpoints.RemoveFavoriteItem(new
        {
            MainViewModel.UserToken, TId = id, Flag = f,
            Root.FavoriteId
        });
        if (!req.Execute(out var res))
        {
            MsgBoxHelper.TryError(res.Message);
        }
        else
        {
            Root.ItemsCount--;
            if (f.Exists(ModelFlags.Ingredient))
            {
                var ii = Ingredients.FirstOrDefault(i => i.Ingredient.IngredientId == id);
                if (ii is not null)
                    Ingredients.Remove(ii);
            }
            else if (f.Exists(ModelFlags.Recipe))
            {
                var ii = Recipes.FirstOrDefault(i => i.RecipeId == id);
                if (ii is not null)
                    Recipes.Remove(ii);
            }
            else if (f.Exists(ModelFlags.Menu))
            {
                var ii = Menus.FirstOrDefault(i => i.RecipeId == id);
                if (ii is not null)
                    Menus.Remove(ii);
            }
        }
    }
}