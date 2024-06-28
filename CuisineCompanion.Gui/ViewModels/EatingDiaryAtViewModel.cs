using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CuisineCompanion.Helper;
using CuisineCompanion.HttpClients;
using CuisineCompanion.Models;
using CuisineCompanion.Views;
using Generators;

namespace CuisineCompanion.ViewModels;

public partial class EatingDiaryAtViewModel : ObservableObject
{
    [ObservableProperty] [AddOnlyUpdate] private decimal dosage;
    [ObservableProperty] private EatingDiaryModel eatingDiary;

    [ObservableProperty] private decimal energy;

    [ObservableProperty] private List<NutrientContentViewModel> nutrientContent;
    [ObservableProperty] private string outUnit;


    partial void OnEatingDiaryChanged(EatingDiaryModel? oldValue, EatingDiaryModel newValue)
    {
        Dosage = EatingDiary.Dosages.Values.Sum();
        EatingDiary.Nutrients.TryGetValue("热量 kcal", out var e);
        Energy = e;
    }

    partial void OnDosageChanged(decimal oldValue, decimal newValue)
    {
        if (IsOnlyUpdate(nameof(Dosage))) return;

        UnitHelper.ConvertToClosestUnit(Dosage, "g", out var n, out var output);
        OnlyUpdateDosage = n;
        OutUnit = output;
    }

    [RelayCommand]
    private void GotoByFlag()
    {
        switch (EatingDiary.Flag)
        {
            case ModelFlags.Recipe:
                var r = ApiService.GetRecipeAt(EatingDiary.TId);
                if (r is null) return;
                foreach (var ingredient in r.Ingredients)
                    if (EatingDiary.Dosages.TryGetValue(ingredient.IngredientId.ToString(), out var d))
                        ingredient.Dosage = d;

                MainViewModel.Navigate.Navigate($"{r.RName} 详细页", new RecipeView
                {
                    DataContext = new RecipeViewModel
                    {
                        Recipe = r
                    }
                });
                break;
            case ModelFlags.Ingredient:
                var i = ApiService.GetIngredientAt(EatingDiary.TId);
                i.Dosage = Dosage;

                var ivm = new IngredientViewModel
                {
                    IngredientModel = i
                };

                ivm.InitAllNutritional();

                var view = new IngredientView
                {
                    DataContext = ivm
                };

                MainViewModel.Navigate.Navigate($"{EatingDiary.Name} 详情页", view);
                break;
        }
    }

    public void InitNutritional(decimal tdee)
    {
        var n = EatingDiary.Nutrients;

        n.TryGetValue("热量 kcal", out var e);


        n.TryGetValue("碳水化合物", out var a);
        n.TryGetValue("蛋白质", out var b);
        n.TryGetValue("脂肪", out var c);

        if (a == 0 && b == 0 && c == 0) return;
        var f = a + b + c;
        (a, b, c) = (a * 100 / f, b * 100 / f, c * 100 / f);
        if (tdee == 0)
            e = 0;
        else
            e = e * 100 / tdee;
        NutrientContent = new List<NutrientContentViewModel>
        {
            new()
            {
                Value = (double)e,
                Icon = XamlResourceHelper.Icon("IconFire"),
                Str = $"{e:0.##}NRV%",
                GradientBrush = ColorHelper.OrangeRed
            },
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