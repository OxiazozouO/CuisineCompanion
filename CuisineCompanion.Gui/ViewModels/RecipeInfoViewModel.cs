using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CuisineCompanion.HttpClients;
using CuisineCompanion.Views;

namespace CuisineCompanion.ViewModels;

public partial class RecipeInfoViewModel : ObservableObject
{
    [ObservableProperty] private string fileUri;
    [ObservableProperty] private int recipeId;

    [ObservableProperty] private string summary;

    [ObservableProperty] private string title;

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