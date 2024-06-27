using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CuisineCompanion.Common;
using CuisineCompanion.Helper;
using CuisineCompanion.HttpClients;
using CuisineCompanion.Models;
using CuisineCompanion.Views;
using Enumerable = System.Linq.Enumerable;

namespace CuisineCompanion.ViewModels;

public partial class IngredientViewModel : ObservableObject, ILike
{
    [ObservableProperty] private IngredientModel ingredientModel;

    [ObservableProperty] private List<CategoryModel> category;

    #region 计算出来的变量

    [ObservableProperty] private Dictionary<EvaluateTag, List<string>> evaluate;

    [ObservableProperty] private List<PieSegmentModel> proteinPieSegmentModels; //百分比饼图模型

    [ObservableProperty] private List<PieSegmentModel> otherPieSegmentModels; //百分比饼图模型

    [ObservableProperty] private List<Tuple<string, decimal>> energyContent; //简单评价

    [ObservableProperty] private List<NutrientContentViewModel> proteinContent; //三大营养素模型

    //其他营养素
    [ObservableProperty] private List<NutrientContentModel> otherNutrientContent;

    #endregion

    #region 辅助变量

    [ObservableProperty] private Visibility isSaveVisible = Visibility.Visible;
    [ObservableProperty] private bool isLike;

    #endregion

    partial void OnIngredientModelChanged(IngredientModel? oldValue, IngredientModel newValue)
    {
        IngredientModel.PropertyChanged += IngredientModelOnPropertyChanged;

        (Category, IsLike) = ApiService.GetIngredientInfo(IngredientModel.IngredientId);

        decimal ans = UnitHelper.ConvertToBaseUnit(IngredientModel.Dosage, IngredientModel.Unit) *
                      (decimal)IngredientModel.Content;
        var list = NutritionalHelper.GetEvaluateTag(IngredientModel.Nutritional, ans);
        if (IngredientModel is { Allergy: not null, InputDosage: > 0 })
        {
            list.Add(new Tuple<EvaluateTag, string>(EvaluateTag.Allergy, IngredientModel.Allergy));
        }

        if (Category is not null && Enumerable.Any(Category, model => model.CName.Contains("酒")))
        {
            list.Add(new Tuple<EvaluateTag, string>(EvaluateTag.Negative, "含酒精"));
        }

        Evaluate = NutritionalHelper.ToDictionary(list);
    }


    private void IngredientModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IngredientModel.EstimatedDosage))
        {
            if (IngredientModel.EstimatedDosage > 0)
                InitAllNutritional();
        }
    }

    public void InitAllNutritional()
    {
        var dic = IngredientModel.GetUpdatedNutritional();
        NutritionalHelper.InitAllNutritional(dic, out var list1, out var list2, out var energy, out var otherNutrient,
            out var protein);

        OtherPieSegmentModels = list1;
        ProteinPieSegmentModels = list2;

        OtherNutrientContent = otherNutrient;
        EnergyContent = energy;
        ProteinContent = protein;
    }

    [RelayCommand]
    private void Reset()
    {
        IngredientModel.InputDosage = IngredientModel.Dosage;
    }

    [RelayCommand]
    private void Collection()
    {
        if (IsLike)
        {
            if (MsgBoxHelper.OkCancel($"是否取消有关于 \"{IngredientModel.IName}\" 的所有收藏？"))
            {
                var req = ApiEndpoints.RemoveFavoriteItems(new
                    { MainViewModel.UserToken, TId = IngredientModel.IngredientId, Flag = ModelFlags.Ingredient });
                if (!req.Execute(out var res))
                {
                    MsgBoxHelper.TryError(res.Message);
                }
                else
                {
                    DeLike();
                }

                return;
            }

            if (!MsgBoxHelper.OkCancel($"添加到收藏夹？"))
            {
                return;
            }
        }

        MainViewModel.Navigate.Navigate(
            text: $"添加{IngredientModel.IName} 到个人收藏夹",
            view: new FavoriteView
            {
                DataContext = new FavoriteViewModel
                {
                    Data = this,
                    MyFavorites = new MyFavoritesViewModel
                    {
                        VisibilityMode = ModelFlags.Ingredient,
                        Favorite = ApiService.GetUserFavorites()
                    }
                }
            },
            isOnlyNavigate: true
        );
    }

    [RelayCommand]
    private void AddEatingDiary()
    {
        var e = ApiService.GetEatingDiaries();
        bool isAll = e?.Count == 0;
        MainViewModel.Navigate.Navigate($"添加{IngredientModel.IName} 到饮食日记",
            view: new EditEatingDiaryView
            {
                DataContext = new EditEatingDiaryViewModel
                {
                    EatingDiary = new EatingDiaryViewModel
                    {
                        EatingDiaries = e,
                    },
                    IsUpdate = isAll,
                    AddTo = AddTo
                }
            },
            isOnlyNavigate: true
        );
    }

    public EatingDiaryAtViewModel AddTo(DateTime time)
    {
        var n = IngredientModel.GetUpdatedNutritional();
        var dos = new Dictionary<int, decimal>
        {
            [IngredientModel.IngredientId] = UnitHelper.ConvertToBaseUnit(IngredientModel.InputDosage,
                IngredientModel.InputUnit)
        };
        var req = ApiEndpoints.AddEatingDiary(new
        {
            MainViewModel.UserToken,
            Flag = ModelFlags.Ingredient,
            Dosages = dos,
            Nutrients = n,
            UpdateTime = time,
            Tid = IngredientModel.IngredientId
        });


        if (req.Execute(out var res))
        {
            try
            {
                var model = new EatingDiaryAtViewModel
                {
                    EatingDiary = new EatingDiaryModel
                    {
                        TId = IngredientModel.IngredientId,
                        Flag = ModelFlags.Recipe,
                        EdId = int.Parse(res.Data.ToString()),
                        FileUrl = IngredientModel.FileUri,
                        Name = IngredientModel.IName,
                        Dosages = dos.ToDictionary(d => d.Key.ToString(), d => d.Value),
                        Nutrients = n,
                        UpdateTime = time,
                    }
                };
                return model;
            }
            catch (Exception e)
            {
                MsgBoxHelper.TryError("ui初始化失败");
                return null;
            }
        }

        MsgBoxHelper.TryError("添加失败");
        return null;
    }

    public void DeLike() => IsLike = false;

    public void Like() => IsLike = true;
}