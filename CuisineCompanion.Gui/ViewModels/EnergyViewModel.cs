using System;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CuisineCompanion.ViewModels;

public partial class EnergyViewModel : ObservableValidator
{
    [Required(ErrorMessage = "请选择日期")] [NotifyDataErrorInfo] [ObservableProperty]
    private DateTime date;

    [ObservableProperty] private decimal energy;
    [ObservableProperty] private byte flag;

    public string Error
    {
        get
        {
            ValidateAllProperties();
            return string.Join('\n', GetErrors());
        }
    }
}