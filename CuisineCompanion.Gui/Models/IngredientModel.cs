using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CuisineCompanion.Common.JsonConverter;
using CuisineCompanion.Helper;
using CuisineCompanion.ViewModels;
using Generators;
using Newtonsoft.Json;

namespace CuisineCompanion.Models;

public partial class IngredientModel : ObservableObject
{
    [ObservableProperty] private int ingredientId;
    [ObservableProperty] private string iName;
    [ObservableProperty] private string fileUri;
    [ObservableProperty] private decimal dosage;
    [ObservableProperty] private string refer;
    [ObservableProperty] private decimal price;
    [ObservableProperty] private string unit;

    /// <summary>
    /// 计量单位转换表
    /// </summary>
    [JsonConverter(typeof(StringToObjectJsonConverter))]
    public Dictionary<string, decimal> Quantity { get; set; }

    [JsonConverter(typeof(StringToObjectJsonConverter))]
    public Dictionary<string, decimal> Nutritional { get; set; }

    [ObservableProperty] private string? allergy;
    [ObservableProperty] private double content;

    [ObservableProperty] private List<NutrientContentViewModel> nutrientContent;

    #region 计算出来的变量

    [ObservableProperty, AddOnlyUpdate] private decimal inputDosage;

    [ObservableProperty, AddOnlyUpdate] private string inputUnit;

    [ObservableProperty, AddOnlyUpdate] private decimal outputDosage;

    [ObservableProperty, AddOnlyUpdate] private string outputUint;

    [ObservableProperty] private decimal estimatedDosage; //净重

    #endregion

    #region Root

    public RecipeModel? RecipeRoot;
    public List<StepModel> stepRoots = new List<StepModel>();
    private decimal stepDosageAns = 0m;

    #endregion

    public Dictionary<string, decimal> GetUpdatedNutritional()
    {
        var dic = new Dictionary<string, decimal>(); //所有营养素
        GetUpdatedNutritional(ref dic);
        return dic;
    }

    public void GetUpdatedNutritional(ref Dictionary<string, decimal> dic)
    {
        //先按比例计算出营养素
        foreach (var (key, value) in Nutritional)
        {
            dic.TryAdd(key, 0m);
            dic[key] += UnitHelper.ConvertToBaseUnit(InputDosage, InputUnit) * value / 100;
        }
    }


    partial void OnUnitChanged(string value)
    {
        InputUnit = value;
    }

    partial void OnDosageChanged(decimal value)
    {
        InputDosage = value;
    }

    partial void OnOutputDosageChanged(decimal value)
    {
        if (IsOnlyUpdate(nameof(OutputDosage))) return;
        OnlyUpdateFlag[nameof(OutputDosage)]++;
        if (Quantity.TryGetValue(OutputUint, out decimal vv))
        {
            decimal result = UnitHelper.ConvertBaseUnitTo(value * vv, InputUnit);
            InputDosage = result;
        }

        OnlyUpdateFlag[nameof(OutputDosage)]--;
    }


    partial void OnInputDosageChanged(decimal oldValue, decimal newValue)
    {
        UnitHelper.ConvertToClosestUnit(newValue, InputUnit, out var a, out var b);
        string oldUnit = InputUnit;

        if (InputUnit != b)
        {
            inputDosage = a;
            OnlyUpdateInputUnit = a == 0 ? Unit : b;
        }

        if (IsOnlyUpdate(nameof(InputDosage))) return;
        if (RecipeRoot is not null)
        {
            RecipeRoot.AddDosage(UnitHelper.ConvertToBaseUnit(newValue - oldValue, oldUnit), Content);

            bool isNotUpdateAllNutritional = RecipeRoot.IsOnlyUpdate(nameof(RecipeModel.Dosage))
                                             || RecipeRoot.IsOnlyUpdate(nameof(RecipeModel.EstimatedDosage));
            if (!isNotUpdateAllNutritional)
            {
                if (RecipeRoot.Dosage < 0.00000001m)
                {
                    RecipeRoot.InitDosage();
                    return;
                }

                RecipeRoot.InitAllNutritional();
            }
        }


        if (stepRoots != null)
            foreach (var root in stepRoots)
                root.InitOutputRequiredTime();


        newValue = UnitHelper.ConvertToBaseUnit(InputDosage, InputUnit);
        EstimatedDosage = InputDosage * (decimal)Content;

        List<(decimal, string, decimal)> ret = new List<(decimal, string, decimal)>();
        foreach (var (key, value1) in Quantity) //3 5 7.6 9.8    7
        {
            decimal t = newValue / value1;
            t %= 1;
            if (t > (decimal)0.5)
            {
                t = 1 - t;
            }

            ret.Add((t, key, value1));
        }

        ret.Sort((l, r) => (int)(100 * (l.Item1 - r.Item1)));

        if (ret.Count > 0)
        {
            OnlyUpdateOutputUint = ret[0].Item2;
            OnlyUpdateOutputDosage = newValue / ret[0].Item3;
        }
    }
}