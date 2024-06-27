using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CuisineCompanion.Helper;
using CuisineCompanion.Models;
using CuisineCompanion.Views;

namespace CuisineCompanion.ViewModels;

/// <summary>
/// 系统导航栏视图模型
/// </summary>
public partial class MenuBarViewModel : ObservableObject
{
    [ObservableProperty] private string title;

    public string IconStr
    {
        set => Icon = XamlResourceHelper.Icon(value);
    }

    [ObservableProperty] private ImageSource icon;

    [ObservableProperty] private PageUrl url;

    public enum PageUrl
    {
        Home = 1,
        Collection,
        Analysis,
        MyInfo,
    }

    public string PageName
    {
        get
        {
            return url switch
            {
                PageUrl.Home => "首页",
                PageUrl.Collection => "收藏的菜谱",
                PageUrl.Analysis => "饮食日记",
                PageUrl.MyInfo => "我的信息",
                _ => ""
            };
        }
    }

    partial void OnUrlChanged(PageUrl oldValue, PageUrl newValue)
    {
        Title = PageName;
    }
    //
    // [RelayCommand]
    // private void Navigate()
    // {
    //     switch (Url)
    //     {
    //         case PageUrl.Home:
    //             MainViewModel.Navigate.GoHome();
    //             break;
    //         case PageUrl.Collection:
    //             MainViewModel.Navigate.Navigate(
    //                 text: PageName,
    //                 view: new FavoriteView
    //                 {
    //                     DataContext = new FavoriteViewModel
    //                     {
    //                         MyFavorites = new MyFavoritesViewModel
    //                         {
    //                             VisibilityMode = ModelFlags.All,
    //                             Favorite = ApiService.GetUserFavorites()
    //                         }
    //                     }
    //                 },
    //                 isOnlyNavigate: true
    //             );
    //             break;
    //         case PageUrl.MyInfo:
    //             var model = ApiService.GetMyAllInfo();
    //             if (model is null) return;
    //             model.Init();
    //             MainViewModel.Navigate.ReNavigate("编辑个人信息", new EditMyInfoView
    //             {
    //                 DataContext = new EditMyInfoViewModel(model)
    //             }, true);
    //             break;
    //     }
    // }
}