using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CuisineCompanion.ViewModels;

public partial class NutrientContentViewModel : ObservableObject
{
    [ObservableProperty] private double value;
    [ObservableProperty] private string str;

    [ObservableProperty] private ImageSource icon;

    [ObservableProperty] private LinearGradientBrush gradientBrush;
}