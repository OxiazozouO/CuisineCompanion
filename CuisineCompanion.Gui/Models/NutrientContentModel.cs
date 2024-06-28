using System;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CuisineCompanion.Helper;
using static CuisineCompanion.ViewModels.MainViewModel;

namespace CuisineCompanion.Models;

public partial class NutrientContentModel : ObservableObject
{
    [ObservableProperty] private Brush color;
    [ObservableProperty] private string name;
    [ObservableProperty] private string unit;
    [ObservableProperty] private decimal value;
}

public partial class NutrientContentRatioViewModel : ObservableObject
{
    [ObservableProperty] private Brush color;
    [ObservableProperty] private decimal ear;

    [ObservableProperty] private decimal maxValue;
    [ObservableProperty] private string name;

    [ObservableProperty] private decimal nrv;
    [ObservableProperty] private string outUnit;

    [ObservableProperty] private decimal outValue;
    [ObservableProperty] private decimal rni;
    [ObservableProperty] private decimal ul;
    [ObservableProperty] private string unit;
    [ObservableProperty] private decimal value;

    public void InitMaxValue()
    {
        if (string.IsNullOrEmpty(Name)) return;
        if (EAR.TryGetValue(Name, out var v1))
        {
            MaxValue = Math.Max(Value, (decimal)v1);
            Ear = (decimal)v1;
        }

        if (RNI.TryGetValue(Name, out var v2))
        {
            MaxValue = Math.Max(Value, (decimal)v2);
            Rni = (decimal)v2;
        }

        if (UL.TryGetValue(Name, out var v3))
        {
            MaxValue = Math.Max(Value, (decimal)v3);
            Ul = (decimal)v3;
        }

        if (MaxValue == 0) MaxValue = 10;

        Color = ColorHelper.GetColor(Value, Ear, Rni, Ul);

        if (Rni > 0)
            Nrv = Value * 100m / Rni;
    }


    partial void OnMaxValueChanged(decimal oldValue, decimal newValue)
    {
        Unit = NutritionalMap?.TryGetValue(Name, out var v) ?? false ? v : "";
        UnitHelper.ConvertToClosestUnit(Value, Unit, out var result, out var output);
        OutUnit = output;
        OutValue = result;
    }
}