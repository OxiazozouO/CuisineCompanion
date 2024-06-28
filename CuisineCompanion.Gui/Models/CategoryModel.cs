using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CuisineCompanion.Models;

public partial class CategoryModel : ObservableObject
{
    [ObservableProperty] private int categoryId;
    [ObservableProperty] private string cName;
}