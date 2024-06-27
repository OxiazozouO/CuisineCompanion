using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CuisineCompanion.Helper;
using CuisineCompanion.HttpClients;
using CuisineCompanion.Models;
using CuisineCompanion.Views;
using static CuisineCompanion.ViewModels.MenuBarViewModel.PageUrl;

namespace CuisineCompanion.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    [ObservableProperty] private List<MenuBarViewModel> menuBars;

    [ObservableProperty] private MenuBarViewModel selectedItem;

    public HomeViewModel()
    {
        InitMenuBars();
    }

    public void InitMenuBars()
    {
        MenuBars = new List<MenuBarViewModel>
        {
            new() { IconStr = "IconHome", Url = Home },
            new() { IconStr = "IconCollection", Url = Collection },
            new() { IconStr = "IconAnalysis", Url = Analysis },
            new() { IconStr = "IconName", Url = MyInfo }
        };
    }


    partial void OnSelectedItemChanged(MenuBarViewModel? oldValue, MenuBarViewModel newValue)
    {
        if (oldValue is null) return;
        switch (SelectedItem.Url)
        {
            case Home:
                MainViewModel.Navigate.GoHome();
                break;
            case Collection:
                MainViewModel.Navigate.Navigate(
                    text: SelectedItem.PageName,
                    view: new FavoriteView
                    {
                        DataContext = new FavoriteViewModel
                        {
                            MyFavorites = new MyFavoritesViewModel
                            {
                                VisibilityMode = ModelFlags.All,
                                Favorite = ApiService.GetUserFavorites()
                            }
                        }
                    },
                    isOnlyNavigate: true
                );
                break;
            case MyInfo:
                MainViewModel.Navigate.ReNavigate("编辑个人信息", new EditMyInfoView
                {
                    DataContext = new EditMyInfoViewModel()
                }, true);
                break;
            case Analysis:
                var e = ApiService.GetEatingDiaries();
                bool isAll = e?.Count == 0;
                MainViewModel.Navigate.Navigate("饮食日记详情",
                    view: new EditEatingDiaryView
                    {
                        DataContext = new EditEatingDiaryViewModel
                        {
                            EatingDiary = new EatingDiaryViewModel
                            {
                                EatingDiaries = e,
                            },
                            IsUpdate = isAll
                        }
                    },
                    isOnlyNavigate: true
                );
                break;
        }
    }
}