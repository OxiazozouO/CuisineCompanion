using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CuisineCompanion.Helper;
using CuisineCompanion.ViewModels;

namespace CuisineCompanion.Views;

public partial class RecipeView
{
    public RecipeView() => InitializeComponent();

    private bool isDragging = false;
    private double initialValue;
    private Point initialMousePosition;


    private void NumericTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is not TextBox t) return;
        if (isDragging)
            return;

        isDragging = true;
        initialMousePosition = e.GetPosition(this);

        if (double.TryParse(t.Text, out initialValue))
        {
            Mouse.Capture(t);
        }
        else
        {
            initialValue = 0;
        }

        t.PreviewMouseLeftButtonUp += NumericTextBox_PreviewMouseLeftButtonUp;
    }

    private void NumericTextBox_MouseMove(object sender, MouseEventArgs e)
    {
        if (sender is not TextBox t) return;
        if (!isDragging || e.LeftButton != MouseButtonState.Pressed) return;

        Point currentMousePosition = e.GetPosition(this);
        double delta = currentMousePosition.X - initialMousePosition.X;
        double newValue = initialValue + delta;

        t.Text = newValue.ToString();
    }

    private void NumericTextBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (sender is not TextBox t) return;
        isDragging = false;
        Mouse.Capture(null);
        t.PreviewMouseLeftButtonUp -= NumericTextBox_PreviewMouseLeftButtonUp;
    }

    private void NumericTextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        if (sender is not TextBox t) return;

        if (!double.TryParse(t.Text, out _))
        {
            t.Text = initialValue.ToString();
        }
    }


    [RelayCommand]
    private void Share()
    {
        if (DataContext is not RecipeViewModel r) return;
        r.IsSaveVisible = Visibility.Collapsed;
        MainPanel.ShowSaveToBmp(r.Recipe.RName + " 食谱");
        r.IsSaveVisible = Visibility.Visible;
    }
}