using System.Threading.Tasks;
using System.Windows;
using CuisineCompanion.ViewModels;
using CuisineCompanion.Views;

namespace CuisineCompanion;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        Task.Run(MainViewModel.InitConfig);
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new AuthenticationView());
    }
}