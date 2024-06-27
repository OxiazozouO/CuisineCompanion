using CommunityToolkit.Mvvm.ComponentModel;

namespace CuisineCompanion.Models;

public partial class CategoryModel : ObservableObject
{
    [ObservableProperty] private int categoryId;
    [ObservableProperty] private string cName;
}