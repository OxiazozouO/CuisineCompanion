using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CuisineCompanion.Helper;
using CuisineCompanion.Models;
using CuisineCompanion.Views;

namespace CuisineCompanion.ViewModels;

public partial class IngredientInfoViewModel : ObservableObject
{
    [ObservableProperty] private IngredientModel ingredient;

    [RelayCommand]
    private void GoToIngredient()
    {
        var view = new IngredientView();
        view.Init(Ingredient);
        MainViewModel.Navigate.Navigate($"{Ingredient.IName} 详情页", view);
    }

    public static ObservableCollection<IngredientInfoViewModel> Create(IEnumerable<IngredientModel> ingredient)
    {
        var list = new ObservableCollection<IngredientInfoViewModel>();
        foreach (var i in ingredient)
            list.Add(new IngredientInfoViewModel { Ingredient = i });

        return list;
    }

    partial void OnIngredientChanged(IngredientModel? oldValue, IngredientModel newValue)
    {
        InitNutritional();
    }

    public void InitNutritional()
    {
        var n = Ingredient.Nutritional;

        n.TryGetValue("碳水化合物", out var a);
        n.TryGetValue("蛋白质", out var b);
        n.TryGetValue("脂肪", out var c);

        if (a == 0 && b == 0 && c == 0) return;
        var f = a + b + c;
        (a, b, c) = (a * 100 / f, b * 100 / f, c * 100 / f);
        Ingredient.NutrientContent = new List<NutrientContentViewModel>
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