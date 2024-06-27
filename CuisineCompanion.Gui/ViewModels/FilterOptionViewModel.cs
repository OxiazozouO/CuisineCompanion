using CommunityToolkit.Mvvm.ComponentModel;
using CuisineCompanion.Models;

namespace CuisineCompanion.ViewModels;

public partial class FilterOptionViewModel : ObservableObject
{
    [ObservableProperty] private string imageSource;
    [ObservableProperty] private string label;
    [ObservableProperty] private SearchFlags searchFlag;
}