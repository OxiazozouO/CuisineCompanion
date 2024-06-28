using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CuisineCompanion.Helper;
using CuisineCompanion.HttpClients;
using CuisineCompanion.Models;
using CuisineCompanion.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CuisineCompanion.ViewModels;

public partial class HomeIndexViewModel : ObservableObject
{
    public static HomeIndexViewModel _instance = new();
    [ObservableProperty] private ObservableCollection<CategoryViewModel> categories;

    [ObservableProperty] private EatingDiaryViewModel eatingDiary;
    [ObservableProperty] private ObservableCollection<IngredientInfoViewModel> ingredients;

    [ObservableProperty] private MainViewModel mainRoot;

    [ObservableProperty] private ObservableCollection<FavoriteModel> menus;

    [ObservableProperty] private MyFavoritesViewModel myFavoritesViewModel;
    [ObservableProperty] private FilterOptionViewModel option;
    [ObservableProperty] private ObservableCollection<RecipeInfoViewModel> recipes;


    public HomeIndexViewModel()
    {
        FilterOptions = new ObservableCollection<FilterOptionViewModel>
        {
            new() { ImageSource = "../Images/1.jpg", Label = "食材", SearchFlag = SearchFlags.Food },
            new() { ImageSource = "../Images/2.jpg", Label = "食谱", SearchFlag = SearchFlags.Recipe },
            new() { ImageSource = "../Images/4.jpg", Label = "分类", SearchFlag = SearchFlags.Category },
            new() { ImageSource = "../Images/3.jpg", Label = "菜单", SearchFlag = SearchFlags.Menu }
        };
        MainRoot = MainViewModel.Instance;
    }

    public void InitOption(SearchFlags flags)
    {
        var op = Option;
        Option = FilterOptions.FirstOrDefault(x => x.SearchFlag == flags);
        if (op == Option)
        {
            Search();
        }
    }

    public ObservableCollection<FilterOptionViewModel> FilterOptions { get; set; }

    public HomeIndexViewModel Instance
    {
        get => _instance;
        set => SetProperty(ref _instance, value);
    }


    public string? SearchText { get; set; } = "";


    partial void OnEatingDiaryChanged(EatingDiaryViewModel? oldValue, EatingDiaryViewModel newValue)
    {
        if (EatingDiary is not null)
            EatingDiary.MaxWidth = 160;
    }

    [RelayCommand]
    private void Search()
    {
        var req = ApiEndpoints.IndexSearch(new
            { MainViewModel.UserToken, Flag = (byte)Option.SearchFlag, Text = SearchText });
        if (req.Execute(out var res) && Clear())
        {
            if (Option.SearchFlag == SearchFlags.Recipe)
            {
                Recipes = res.Data.ToEntity<ObservableCollection<RecipeInfoViewModel>>();
            }
            else if (Option.SearchFlag == SearchFlags.Food)
            {
                Ingredients = IngredientInfoViewModel.Create(res.Data.ToEntity<IEnumerable<IngredientModel>>());
            }
            else if (Option.SearchFlag == SearchFlags.Category)
            {
                if (JsonConvert.DeserializeObject(res.Data.ToString()) is JObject obj)
                {
                    Categories = obj["recipes"].ToObject<ObservableCollection<CategoryViewModel>>() ??
                                 new ObservableCollection<CategoryViewModel>();
                    var list2 = obj["ingredients"].ToObject<List<CategoryViewModel>>();
                    if (list2 != null)
                        foreach (var model in list2)
                            Categories.Add(model);
                }
            }
            else if (Option.SearchFlag == SearchFlags.Menu)
            {
                Menus = res.Data.ToEntity<ObservableCollection<FavoriteModel>>();
            }
        }
    }

    partial void OnOptionChanged(FilterOptionViewModel? oldValue, FilterOptionViewModel newValue)
    {
        Search();
    }

    private bool Clear()
    {
        Recipes = null;
        Ingredients = null;
        Categories = null;
        Menus = null;
        return true;
    }

    [RelayCommand]
    private static void OpenFavoriteView(FavoriteModel model)
    {
        MainViewModel.Navigate.Navigate($"{model.FName} 收藏夹详情",
            new FavoriteItemView
            {
                DataContext = new FavoriteItemViewModel
                {
                    Root = model,
                    IsEdit = false
                }
            }, true);
    }

    [RelayCommand]
    private void GotoUserInfo()
    {
        var view = new EditMyInfoView
        {
            DataContext = new EditMyInfoViewModel()
        };

        MainViewModel.Navigate.Navigate("编辑个人信息", view, true);
    }
}