using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CuisineCompanion.ViewModels;

public partial class NutrientContentViewModel : ObservableObject
{
    [ObservableProperty] private LinearGradientBrush gradientBrush;

    [ObservableProperty] private ImageSource icon;
    [ObservableProperty] private string str;
    [ObservableProperty] private double value;
}