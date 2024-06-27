using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CuisineCompanion.Models;

namespace CuisineCompanion.ViewModels;

public partial class FavoriteAtModel : ObservableObject
{
    [ObservableProperty] private byte flag;
    [ObservableProperty] private ObservableCollection<FavoriteModel> favoriteList;

    public FavoriteAtModel()
    {
        FavoriteList = new ObservableCollection<FavoriteModel>();
    }
}