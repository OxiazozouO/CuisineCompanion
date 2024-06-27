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
    [ObservableProperty] private string title;
    [ObservableProperty] private string fileUri;
    [ObservableProperty] private string refer;

    [ObservableProperty] private TimeSpan? requiredTime = TimeSpan.Zero;

    [JsonConverter(typeof(StringToRequiredIngredientJsonConverter))]
    public Tuple<List<string>, List<string>, List<int>>? RequiredIngredient { get; set; }

    [ObservableProperty] private string summary;

    #region Root

    public List<IngredientModel> IngredientRoots { get; set; } = new List<IngredientModel>();
    public RecipeModel? RecipeRoot;

    #endregion


    #region 计算出来的变量

    [ObservableProperty] private TimeSpan? outputRequiredTime = TimeSpan.Zero;

    private decimal stepDosageAns = 0m;
    private decimal Ans = 0m;

    #endregion


    public void InitOutputRequiredTime()
    {
        if (stepDosageAns == 0m)
        {
            Ans += RequiredIngredient.Item3.Sum();
            decimal sum = IngredientRoots.Select((e, i) =>
                    UnitHelper.ConvertToBaseUnit(e.Dosage, e.Unit) * RequiredIngredient.Item3[i] / Ans)
                .Sum();

            stepDosageAns = sum;
        }

        decimal now = IngredientRoots.Select((e, i) =>
                UnitHelper.ConvertToBaseUnit(e.InputDosage, e.InputUnit) * RequiredIngredient.Item3[i] / Ans)
            .Sum();

        var time = RequiredTime * (double)(now / stepDosageAns);
        if (time is { Ticks: 0 })
        {
            time = null;
        }

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
        if (RecipeRoot != null)
        {
            RecipeRoot.SpendTime += newValue.Value - oldValue.Value;
        }
    }
}