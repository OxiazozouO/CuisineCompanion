using System.Windows;
using CommunityToolkit.Mvvm.Input;
using CuisineCompanion.Helper;
using CuisineCompanion.Models;
using CuisineCompanion.ViewModels;

namespace CuisineCompanion.Views;

public partial class IngredientView
{
    public IngredientView()
    {
        InitializeComponent();
    }

    public void Init(IngredientModel model)
    {
        if (DataContext is not IngredientViewModel i) return;
        i.IngredientModel = model;
        i.InitAllNutritional();
    }

    [RelayCommand]
    private void Share()
    {
        if (DataContext is not IngredientViewModel i) return;
        i.IsSaveVisible = Visibility.Collapsed;
        MainPanel.ShowSaveToBmp(i.IngredientModel.IName + " 食材");
        i.IsSaveVisible = Visibility.Visible;
    }
}