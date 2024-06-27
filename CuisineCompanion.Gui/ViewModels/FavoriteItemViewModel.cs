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
    [ObservableProperty] private ObservableCollection<RecipeInfoViewModel> recipes;
    [ObservableProperty] private ObservableCollection<RecipeInfoViewModel> menus;
    [ObservableProperty] private ObservableCollection<IngredientInfoViewModel> ingredients;

    private readonly Dictionary<string, object> _searchCache = new Dictionary<string, object>();
    [ObservableProperty] private string searchText = "";
    [ObservableProperty] private FavoriteModel root;
    [ObservableProperty] private bool isEdit = true;

    partial void OnRootChanged(FavoriteModel? oldValue, FavoriteModel newValue)
    {
        var f = (ModelFlags)Root.Flag;
        var req = ApiEndpoints.FavoriteListItems(new
            { MainViewModel.UserToken, Flag = Root.Flag, FavoriteId = Root.FavoriteId });
        if (!req.Execute(out var res)) return;
        if (f.Exists(ModelFlags.Ingredient))
        {
            Ingredients = IngredientInfoViewModel.Create(res.Data.ToEntity<IEnumerable<IngredientModel>>());
        }
        else if (f.Exists(ModelFlags.Recipe))
        {
            Recipes = res.Data.ToEntity<ObservableCollection<RecipeInfoViewModel>>();
        }
        else if (f.Exists(ModelFlags.Menu))
        {
            Menus = res.Data.ToEntity<ObservableCollection<RecipeInfoViewModel>>();
        }
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
        bool isExists = _searchCache.TryGetValue(SearchText, out var res);
        if (f.Exists(ModelFlags.Ingredient))
        {
            if (isExists)
            {
                Ingredients = res as ObservableCollection<IngredientInfoViewModel>;
            }
            else
            {
                if (_searchCache[""] is ObservableCollection<IngredientInfoViewModel> list)
                {
                    Ingredients = new ObservableCollection<IngredientInfoViewModel>(
                        list.Where(i =>
                            (i.Ingredient.IName != null && i.Ingredient.IName.Contains(SearchText))
                            || (i.Ingredient.Refer != null && i.Ingredient.Refer.Contains(SearchText))
                        ));
                    if (Ingredients.Count != 0)
                        _searchCache[SearchText] = Ingredients;
                }
            }
        }
        else if (f.Exists(ModelFlags.Recipe))
        {
            if (isExists)
            {
                Recipes = res as ObservableCollection<RecipeInfoViewModel>;
            }
            else
            {
                if (_searchCache[""] is ObservableCollection<RecipeInfoViewModel> list)
                {
                    Recipes = new ObservableCollection<RecipeInfoViewModel>(list.Where(r =>
                        (r.Summary != null && r.Summary.Contains(SearchText))
                        || (r.Title != null && r.Title.Contains(SearchText))
                    ));
                    if (Recipes.Count != 0)
                        _searchCache[SearchText] = Recipes;
                }
            }
        }
        else if (f.Exists(ModelFlags.Menu))
        {
            if (isExists)
            {
                Menus = res as ObservableCollection<RecipeInfoViewModel>;
            }
            else
            {
                if (_searchCache[""] is ObservableCollection<RecipeInfoViewModel> list)
                {
                    Menus = new ObservableCollection<RecipeInfoViewModel>(list.Where(r =>
                        (r.Summary != null && r.Summary.Contains(SearchText))
                        || (r.Title != null && r.Title.Contains(SearchText))
                    ));
                    if (Menus.Count != 0)
                        _searchCache[SearchText] = Menus;
                }
            }
        }
    }

    [RelayCommand]
    private void DeleteItem(int id)
    {
        if (!MsgBoxHelper.Confirmation($"是否删除？"))
        {
            return;
        }

        var f = (ModelFlags)Root.Flag;
        var req = ApiEndpoints.RemoveFavoriteItem(new
            { MainViewModel.UserToken, TId = id, Flag = f, FavoriteId = Root.FavoriteId });
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