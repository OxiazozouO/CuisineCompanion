using CommunityToolkit.Mvvm.Input;

namespace CuisineCompanion.Common;

public partial class DateTimeDialog
{
    public object ReturnValue { get; private set; }

    public DateTimeDialog()
    {
        InitializeComponent();
    }


    [RelayCommand]
    private void Ok()
    {
        ReturnValue = DatePicker.SelectedDate;
        if (ReturnValue is null) return;
        DialogResult = true;
        Close();
    }

    [RelayCommand]
    private void Cancel()
    {
        DialogResult = false;
        Close();
    }
}