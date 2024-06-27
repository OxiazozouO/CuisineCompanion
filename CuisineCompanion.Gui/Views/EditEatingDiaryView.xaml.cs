using System.Windows;
using CuisineCompanion.ViewModels;

namespace CuisineCompanion.Views;

public partial class EditEatingDiaryView
{
    public EditEatingDiaryView()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is not EditEatingDiaryViewModel ed) return;
        ListBox1.GetTop = ed.Top;
        ListBox1.GetBottom = ed.Bottom;
    }
}