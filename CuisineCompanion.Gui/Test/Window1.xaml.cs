using System.Windows;
using System.Windows.Media;
using CuisineCompanion.Common;

namespace CuisineCompanion.Test;

public partial class Window1
{
    public Window1()
    {
        InitializeComponent();
        var s = new Sector
        {
            Center = new Point(100, 100),
            OuterRadius = 100,
            InnerRadius = 50,
            StartAngle = 0,
            Angle = 90,
            Fill = Brushes.LightBlue,
            IsStroked = true
        };
        DataContext = s;
        canvas.Child = s;
    }
}