using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CuisineCompanion.Common;
using CuisineCompanion.Helper;
using CuisineCompanion.HttpClients;
using CuisineCompanion.Models;
using CuisineCompanion.Views;

namespace CuisineCompanion.ViewModels;

public partial class RecipeViewModel : ObservableObject
{
    [ObservableProperty] private RecipeModel recipe;

    [ObservableProperty] private Visibility isSaveVisible = Visibility.Visible;

    public RecipeViewModel()
    {
        // var json = File.ReadAllText(@"..\..\..\TempJson\Recipe.json");
        // Recipe = json.ToEntity<RecipeModel>();
    }

    partial void OnRecipeChanged(RecipeModel? oldValue, RecipeModel newValue)
    {
        Recipe.Init();
        InitNutritional();
    }

    private void InitNutritional()
    {
        foreach (var ingredient in Recipe.Ingredients)
        {
            var n = ingredient.Nutritional;

            n.TryGetValue("碳水化合物", out var a);
            n.TryGetValue("蛋白质", out var b);
            n.TryGetValue("脂肪", out var c);

            if (a == 0 && b == 0 && c == 0) continue;
            var f = a + b + c;
            (a, b, c) = (a * 100 / f, b * 100 / f, c * 100 / f);
            ingredient.NutrientContent = new List<NutrientContentViewModel>
            {
                new()
                {
                    Value = (double)a,
                    Icon = XamlResourceHelper.Icon("IconCarbohydrate"),
                    Str = $"{a:0.##}%",
                    GradientBrush = ColorHelper.Blue
                },
                new()
                {
                    Value = (double)b,
                    Icon = XamlResourceHelper.Icon("IconProtein"),
                    Str = $"{b:0.##}%",
                    GradientBrush = ColorHelper.Purple
                },
                new()
                {
                    Value = (double)c,
                    Icon = XamlResourceHelper.Icon("IconFat"),
                    Str = $"{c:0.##}%",
                    GradientBrush = ColorHelper.Orange
                }
            };
        }
    }


    [RelayCommand]
    private static void GoToIngredient(IngredientModel model)
    {
        var view = new IngredientView();
        view.Init(model);
        MainViewModel.Navigate.Navigate($"{model.IName} 详情页", view);
    }

    private bool tag = false;

    [RelayCommand]
    private void Reset()
    {
        if (tag) return;
        tag = true;
        Recipe.InitDosage();
        Recipe.InitAllNutritional();
        tag = false;
    }

    [RelayCommand]
    private void Collection()
    {
        if (Recipe.IsLike)
        {
            if (MsgBoxHelper.OkCancel($"是否取消有关于 \"{Recipe.RName}\" 的所有收藏？"))
            {
                var req = ApiEndpoints.RemoveFavoriteItems(new
                    { MainViewModel.UserToken, TId = Recipe.RecipeId, Flag = ModelFlags.Recipe });
                if (!req.Execute(out var res))
                {
                    MsgBoxHelper.TryError(res.Message);
                }
                else
                {
                    Recipe.DeLike();
                }

                return;
            }

            if (!MsgBoxHelper.OkCancel($"添加到收藏夹？"))
            {
                return;
            }
        }

        MainViewModel.Navigate.Navigate(
            text: $"添加{Recipe.RName} 到个人收藏夹",
            view: new FavoriteView
            {
                DataContext = new FavoriteViewModel
                {
                    Data = Recipe,
                    MyFavorites = new MyFavoritesViewModel
                    {
                        VisibilityMode = ModelFlags.Recipe | ModelFlags.Menu,
                        Favorite = ApiService.GetUserFavorites()
                    }
                }
            },
            isOnlyNavigate: true
        );
    }


    [RelayCommand]
    private void AddEatingDiary()
    {
        var e = ApiService.GetEatingDiaries();
        bool isAll = e?.Count == 0;
        MainViewModel.Navigate.Navigate($"添加{Recipe.RName} 到饮食日记",
            view: new EditEatingDiaryView
            {
                DataContext = new EditEatingDiaryViewModel
                {
                    EatingDiary = new EatingDiaryViewModel
                    {
                        EatingDiaries = e,
                    },
                    IsUpdate = isAll,
                    AddTo = AddTo
                }
            },
            isOnlyNavigate: true
        );
    }

    public EatingDiaryAtViewModel AddTo(DateTime time)
    {
        var dic = Recipe.Ingredients.ToDictionary(i => i.IngredientId, i =>
            UnitHelper.ConvertToBaseUnit(i.InputDosage, i.InputUnit));
        var req = ApiEndpoints.AddEatingDiary(new
        {
            MainViewModel.UserToken,
            Flag = ModelFlags.Recipe,
            Dosages = dic,
            Nutrients = Recipe.AllNutritional,
            UpdateTime = time,
            Tid = Recipe.RecipeId
        });

        if (req.Execute(out var res))
        {
            try
            {
                var model = new EatingDiaryAtViewModel
                {
                    EatingDiary = new EatingDiaryModel
                    {
                        TId = Recipe.RecipeId,
                        Flag = ModelFlags.Recipe,
                        EdId = int.Parse(res.Data.ToString()),
                        FileUrl =  Recipe.FileUri,
                        Name = Recipe.RName,
                        Dosages = dic.ToDictionary(d => d.Key.ToString(), d => d.Value),
                        Nutrients = Recipe.AllNutritional,
                        UpdateTime = time,
                    }
                };
                return model;
            }
            catch (Exception e)
            {
                MsgBoxHelper.TryError("ui初始化失败");
                return null;
            }
        }

        MsgBoxHelper.TryError("添加失败");
        return null;
    }
}