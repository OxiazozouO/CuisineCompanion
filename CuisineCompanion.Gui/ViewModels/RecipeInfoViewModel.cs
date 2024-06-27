using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CuisineCompanion.Helper;
using CuisineCompanion.HttpClients;
using CuisineCompanion.Models;
using CuisineCompanion.Views;

namespace CuisineCompanion.ViewModels;

public partial class RecipeInfoViewModel : ObservableObject
{
    [ObservableProperty] private int recipeId;

    [ObservableProperty] private string fileUri;

    [ObservableProperty] private string title;

    [ObservableProperty] private string summary;

    [RelayCommand]
    private void GoToRecipe()
    {
        var r = ApiService.GetRecipeAt(RecipeId);
        if (r is null) return;

        MainViewModel.Navigate.Navigate($"{r.RName} 详细页", new RecipeView
        {
            DataContext = new RecipeViewModel
            {
                Recipe = r
            }
        });
    }
}