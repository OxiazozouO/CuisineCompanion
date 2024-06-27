using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CuisineCompanion.Models;

public partial class MealPlanModel : ObservableObject
{
    [ObservableProperty] private int mealPlanId;
    [ObservableProperty] private string mpName;
    [ObservableProperty] private string fileUri;
    [ObservableProperty] private string refer;
    [ObservableProperty] private DateTime mpiTime;
}