using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommunityToolkit.Mvvm.ComponentModel;
using CuisineCompanion.Models;

namespace CuisineCompanion.ViewModels;

public partial class MyFavoritesViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<FavoriteModel> favorite;

    #region Root

    [ObservableProperty] private FavoriteViewModel? favoriteViewRoot;

    #endregion

    [ObservableProperty] private ModelFlags visibilityMode;

    public MyFavoritesViewModel()
    {
        Favorite = new ObservableCollection<FavoriteModel>();
        Ingredients = new FavoriteAtModel();
        Recipes = new FavoriteAtModel();
        Menus = new FavoriteAtModel();

        Favorite.CollectionChanged += FavoriteOnCollectionChanged;
    }

    partial void OnFavoriteChanged(ObservableCollection<FavoriteModel>? oldValue,
        ObservableCollection<FavoriteModel> newValue)
    {
        var dic = new Dictionary<string, FavoriteAtModel>();

        foreach (var model in Favorite)
        {
            var name = FavoriteFlagsHelper.GetName(model.Flag);
            if (dic.ContainsKey(name))
            {
                dic[name] ??= new FavoriteAtModel();
                dic[name].FavoriteList.Add(model);
            }
            else
            {
                dic[name] = new FavoriteAtModel { FavoriteList = new ObservableCollection<FavoriteModel> { model } };
            }
        }

        if (dic.TryGetValue("食材", out var a))
            Ingredients = a;
        if (dic.TryGetValue("食谱", out var b))
            Recipes = b;
        if (dic.TryGetValue("菜单", out var c))
            Menus = c;

        Favorite.CollectionChanged += FavoriteOnCollectionChanged;
    }

    private void FavoriteOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            CollectDic(e.NewItems, out var a, out var b, out var c);

            if (a != null)
                foreach (var model in a.FavoriteList)
                    if (!Ingredients.FavoriteList.Contains(model))
                        Ingredients.FavoriteList.Add(model);
            if (b != null)
                foreach (var model in b.FavoriteList)
                    if (!Recipes.FavoriteList.Contains(model))
                        Recipes.FavoriteList.Add(model);
            if (c != null)
                foreach (var model in c.FavoriteList)
                    if (!Menus.FavoriteList.Contains(model))
                        Menus.FavoriteList.Add(model);
        }
        else if (e.Action == NotifyCollectionChangedAction.Remove)
        {
            CollectDic(e.OldItems, out var a, out var b, out var c);

            if (a != null)
                foreach (var model in a.FavoriteList)
                    if (Ingredients.FavoriteList.Contains(model))
                        Ingredients.FavoriteList.Remove(model);
            if (b != null)
                foreach (var model in b.FavoriteList)
                    if (Recipes.FavoriteList.Contains(model))
                        Recipes.FavoriteList.Remove(model);
            if (c != null)
                foreach (var model in c.FavoriteList)
                    if (Menus.FavoriteList.Contains(model))
                        Menus.FavoriteList.Remove(model);
        }
    }

    /// <summary>
    ///     收集字典
    /// </summary>
    /// <param name="oldValue"></param>
    /// <param name="newValue"></param>
    private static void CollectDic(IList? list, out FavoriteAtModel? a, out FavoriteAtModel? b,
        out FavoriteAtModel? c)
    {
        var dic = new Dictionary<string, FavoriteAtModel>();
        if (list != null)
            foreach (FavoriteModel model in list)
            {
                var name = FavoriteFlagsHelper.GetName(model.Flag);
                if (dic.ContainsKey(name))
                {
                    dic[name] ??= new FavoriteAtModel();
                    dic[name].FavoriteList.Add(model);
                }
                else
                {
                    dic[name] = new FavoriteAtModel
                        { FavoriteList = new ObservableCollection<FavoriteModel> { model } };
                }
            }

        dic.TryGetValue("食材", out a);
        dic.TryGetValue("食谱", out b);
        dic.TryGetValue("菜单", out c);
    }

    partial void OnIngredientsChanged(FavoriteAtModel? oldValue, FavoriteAtModel newValue)
    {
        Ingredients.Flag = (byte)(ModelFlags.Ingredient | ModelFlags.Private);
    }

    partial void OnRecipesChanged(FavoriteAtModel? oldValue, FavoriteAtModel newValue)
    {
        Recipes.Flag = (byte)(ModelFlags.Recipe | ModelFlags.Private);
    }

    partial void OnMenusChanged(FavoriteAtModel? oldValue, FavoriteAtModel newValue)
    {
        Menus.Flag = (byte)(ModelFlags.Menu | ModelFlags.Private);
    }

    #region 计算出来的变量

    //食材收藏夹
    [ObservableProperty] private FavoriteAtModel ingredients;

    //食谱收藏夹
    [ObservableProperty] private FavoriteAtModel recipes;

    //菜单收藏夹
    [ObservableProperty] private FavoriteAtModel menus;

    #endregion
}