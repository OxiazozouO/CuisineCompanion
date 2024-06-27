using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CuisineCompanion.Common;
using CuisineCompanion.Helper;
using CuisineCompanion.ViewModels;
using Generators;

namespace CuisineCompanion.Models;

public partial class RecipeModel : ObservableObject, ILike
{
    [ObservableProperty] private int recipeId;
    [ObservableProperty] private string title;
    [ObservableProperty] private string rName;
    [ObservableProperty] private string fileUri;
    [ObservableProperty] private string summary;
    [ObservableProperty] private List<CategoryModel> category;
    [ObservableProperty] private List<IngredientModel> ingredients;
    [ObservableProperty] private List<StepModel> steps;
    [ObservableProperty] private List<MealPlanModel> mealPlans;

    [ObservableProperty] private bool isLike;
    public void DeLike() => IsLike = false;
    public void Like() => IsLike = true;


    #region 计算出来的变量

    [ObservableProperty] private TimeSpan spendTime; //总时间

    [ObservableProperty] private decimal totalPrice; //总价格

    [ObservableProperty, AddOnlyUpdate] public decimal dosage; //总克重(源数据)

    [ObservableProperty, AddOnlyUpdate] public decimal outputDosage; //总克重(输出)

    [ObservableProperty, AddOnlyUpdate] public decimal estimatedDosage; //净重

    [ObservableProperty] private Dictionary<string, decimal> allNutritional = new(); //所有营养成分

    [ObservableProperty] private List<PieSegmentModel> pieSegmentModels; //百分比饼图模型

    //三大营养素
    [ObservableProperty] private List<NutrientContentViewModel> proteinContent;

    //千焦 千卡
    [ObservableProperty] private List<Tuple<string, decimal>> energyContent;

    //其他营养素
    [ObservableProperty] private List<NutrientContentModel> otherNutrientContent;

    //简单的评价
    [ObservableProperty] private Dictionary<EvaluateTag, List<string>> evaluate;

    #endregion


    public void Init()
    {
        foreach (var ingredient in Ingredients)
            ingredient.RecipeRoot = this;

        foreach (var step in Steps)
            step.RecipeRoot = this;

        InitSpendTime();
        InitDosage();


        var mp = Ingredients.ToDictionary(d => d.IName);
        foreach (var step in Steps)
        {
            step.RecipeRoot = this;
            var list = new List<IngredientModel>();
            if (step.RequiredIngredient is not null)
            {
                foreach (var s in step.RequiredIngredient.Item2)
                {
                    if (mp.TryGetValue(s, out var i))
                    {
                        list.Add(i);
                    }
                }
            }

            step.IngredientRoots = list;

            foreach (var model in list)
            {
                model.stepRoots.Add(step);
            }
        }

        InitAllNutritional();
    }

    public void InitDosage()
    {
        OnlyUpdateFlag[nameof(Dosage)]++;
        foreach (var ingredient in Ingredients)
        {
            ingredient.InputUnit = ingredient.Unit;
            //先减到0重置
            ingredient.InputDosage = 0m;
            //然后恢复默认，不用OnlyUpdate是因为需要更新OutputUint和OutputDosage
            ingredient.InputDosage = ingredient.Dosage;
        }

        OnlyUpdateFlag[nameof(Dosage)]--;

        OnlyUpdateEstimatedDosage = OnlyUpdateDosage = 0;
        foreach (var ingredient in Ingredients)
        {
            AddDosage(ingredient.Dosage, ingredient.Content);
        }
    }

    private void InitSpendTime()
    {
        spendTime = TimeSpan.Zero;
        foreach (var step in Steps)
        {
            spendTime += step.RequiredTime ?? TimeSpan.Zero;
        }
    }


    public void InitAllNutritional()
    {
        var dic = new Dictionary<string, decimal>(); //所有营养素
        //先按比例计算出营养素
        foreach (var ingredient in Ingredients)
        {
            ingredient.GetUpdatedNutritional(ref dic);
        }

        NutritionalHelper.InitAllNutritional(dic, out var list1, out var energy, out var otherNutrient,
            out var protein);

        AllNutritional = dic;
        PieSegmentModels = list1;
        EnergyContent = energy;
        OtherNutrientContent = otherNutrient;
        ProteinContent = protein;
    }

    public void AddDosage(decimal value, double content)
    {
        if (IsOnlyUpdate(nameof(Dosage))) return;

        Console.Write(Dosage + "+   " + value + "=   ");
        OnlyUpdateDosage = Dosage + value;
        Console.WriteLine(Dosage);

        if (IsOnlyUpdate(nameof(EstimatedDosage))) return;
        OnlyUpdateEstimatedDosage = EstimatedDosage + value * (decimal)content;
    }

    partial void OnDosageChanged(decimal oldAns, decimal newAns)
    {
        if (IsOnlyUpdate(nameof(Dosage))) return;
        if (Dosage < 0.00000001m)
        {
            InitDosage();
            InitAllNutritional();
            return;
        }

        decimal dx = newAns - oldAns;

        List<decimal> mp = Ingredients.Select(ingredient =>
            UnitHelper.ConvertToBaseUnit(ingredient.InputDosage, ingredient.InputUnit)).ToList();
        decimal ans = mp.Sum();

        OnlyUpdateFlag[nameof(Dosage)]++;
        for (var i = 0; i < Ingredients.Count; i++)
        {
            var ingredient = Ingredients[i];
            decimal baseDx = dx * mp[i] / ans;
            //+=增量*占比

            ingredient.InputDosage += UnitHelper.ConvertBaseUnitTo(baseDx, ingredient.InputUnit);

            OnlyUpdateEstimatedDosage = EstimatedDosage + baseDx * (decimal)ingredient.Content;
        }

        OnlyUpdateFlag[nameof(Dosage)]--;
        InitAllNutritional();
    }

    partial void OnEstimatedDosageChanged(decimal oldAns, decimal newAns)
    {
        if (IsOnlyUpdate(nameof(EstimatedDosage))) return;
        if (newAns < 0.00000001m)
        {
            InitDosage();
            InitAllNutritional();
            return;
        }

        OnlyUpdateFlag[nameof(EstimatedDosage)]++;
        foreach (var ingredient in Ingredients)
        {
            ingredient.InputDosage = ingredient.InputDosage * newAns / oldAns;
        }

        OnlyUpdateFlag[nameof(EstimatedDosage)]--;
        InitAllNutritional();
    }

    partial void OnPieSegmentModelsChanged(List<PieSegmentModel>? oldValue, List<PieSegmentModel> newValue)
    {
        var list = NutritionalHelper.GetEvaluateTag(AllNutritional, EstimatedDosage);
        list.AddRange(ingredients.Where(i => i is { Allergy: not null, InputDosage: > 0 })
            .Select(i => new Tuple<EvaluateTag, string>(EvaluateTag.Allergy, i.Allergy)));
        Evaluate = NutritionalHelper.ToDictionary(list);
    }
}