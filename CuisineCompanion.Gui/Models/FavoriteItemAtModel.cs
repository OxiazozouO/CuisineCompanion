using CommunityToolkit.Mvvm.ComponentModel;

namespace CuisineCompanion.Models;

public partial class FavoriteItemAtModel : ObservableObject
{
    [ObservableProperty] private string fileUri;
    [ObservableProperty] private int iD;
    [ObservableProperty] private string name;
    [ObservableProperty] private string refer;
}