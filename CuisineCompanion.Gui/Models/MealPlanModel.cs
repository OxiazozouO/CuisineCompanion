using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CuisineCompanion.Models;

public partial class MealPlanModel : ObservableObject
{
    [ObservableProperty] private string fileUri;
    [ObservableProperty] private int mealPlanId;
    [ObservableProperty] private DateTime mpiTime;
    [ObservableProperty] private string mpName;
    [ObservableProperty] private string refer;
}