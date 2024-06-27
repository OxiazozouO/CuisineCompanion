using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CuisineCompanion.Models;

namespace CuisineCompanion.ViewModels;

public partial class CategoryViewModel : ObservableObject
{
    [ObservableProperty] private CategoryModel category;
    [ObservableProperty] private ObservableCollection<IngredientInfoViewModel> ingredientInfos;
    [ObservableProperty] private ObservableCollection<RecipeInfoViewModel> recipes;
    
    
    [ObservableProperty] private ObservableCollection<IngredientModel> ingredients;

    partial void OnIngredientsChanged(ObservableCollection<IngredientModel>? oldValue,
        ObservableCollection<IngredientModel> newValue)
    {
        IngredientInfos = IngredientInfoViewModel.Create(Ingredients);
    }
}