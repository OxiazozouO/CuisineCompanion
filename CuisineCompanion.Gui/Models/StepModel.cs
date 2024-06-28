using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CuisineCompanion.Common.JsonConverter;
using CuisineCompanion.Helper;
using Newtonsoft.Json;

namespace CuisineCompanion.Models;

public partial class StepModel : ObservableObject
{
    [ObservableProperty] private string fileUri;
    [ObservableProperty] private string refer;

    [ObservableProperty] private TimeSpan? requiredTime = TimeSpan.Zero;

    [ObservableProperty] private string summary;
    [ObservableProperty] private string title;

    [JsonConverter(typeof(StringToRequiredIngredientJsonConverter))]
    public Tuple<List<string>, List<string>, List<int>>? RequiredIngredient { get; set; }


    public void InitOutputRequiredTime()
    {
        if (stepDosageAns == 0m)
        {
            Ans += RequiredIngredient.Item3.Sum();
            var sum = IngredientRoots.Select((e, i) =>
                    UnitHelper.ConvertToBaseUnit(e.Dosage, e.Unit) * RequiredIngredient.Item3[i] / Ans)
                .Sum();

            stepDosageAns = sum;
        }

        var now = IngredientRoots.Select((e, i) =>
                UnitHelper.ConvertToBaseUnit(e.InputDosage, e.InputUnit) * RequiredIngredient.Item3[i] / Ans)
            .Sum();

        var time = RequiredTime * (double)(now / stepDosageAns);
        if (time is { Ticks: 0 }) time = null;

        OutputRequiredTime = time;
    }

    partial void OnRequiredTimeChanged(TimeSpan? oldValue, TimeSpan? newValue)
    {
        OutputRequiredTime = RequiredTime;
    }

    partial void OnOutputRequiredTimeChanged(TimeSpan? oldValue, TimeSpan? newValue)
    {
        newValue ??= TimeSpan.Zero;
        oldValue ??= TimeSpan.Zero;
        if (RecipeRoot != null) RecipeRoot.SpendTime += newValue.Value - oldValue.Value;
    }

    #region Root

    public List<IngredientModel> IngredientRoots { get; set; } = new();
    public RecipeModel? RecipeRoot;

    #endregion


    #region 计算出来的变量

    [ObservableProperty] private TimeSpan? outputRequiredTime = TimeSpan.Zero;

    private decimal stepDosageAns;
    private decimal Ans;

    #endregion
}