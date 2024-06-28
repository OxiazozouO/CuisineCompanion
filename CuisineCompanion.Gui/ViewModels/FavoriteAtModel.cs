using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CuisineCompanion.Models;

namespace CuisineCompanion.ViewModels;

public partial class FavoriteAtModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<FavoriteModel> favoriteList;
    [ObservableProperty] private byte flag;

    public FavoriteAtModel()
    {
        FavoriteList = new ObservableCollection<FavoriteModel>();
    }
}