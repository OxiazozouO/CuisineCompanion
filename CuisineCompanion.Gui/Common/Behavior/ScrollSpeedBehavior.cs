using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using CuisineCompanion.Helper;
using Microsoft.Xaml.Behaviors;

namespace CuisineCompanion.Common.Behavior;

public class ScrollSpeedBehavior : Behavior<ScrollViewer>
{
    private static readonly DispatcherTimer Timer = new DispatcherTimer()
    {
        Interval = TimeSpan.FromSeconds(0.005)
    };

    private static readonly DispatcherTimer Timer2 = new DispatcherTimer()
    {
        Interval = TimeSpan.FromSeconds(0.1)
    };

    static ScrollSpeedBehavior()
    {
        Timer.Start();
        Timer2.Start();
    }

// @formatter:off
    protected override void OnAttached()
    {
        AssociatedObject.SetTransformGroup();
        Timer.Tick                         += TimerOnTick;
        Timer2.Tick                        += TimerOnTick2;
        AssociatedObject.PreviewMouseWheel += AssociatedObjectOnPreviewMouseWheel;
    }

    protected override void OnDetaching()
    {
        Timer.Tick                         -= TimerOnTick;
        Timer2.Tick                        -= TimerOnTick2;
        AssociatedObject.PreviewMouseWheel -= AssociatedObjectOnPreviewMouseWheel;
    }
// @formatter:on
    private void AssociatedObjectOnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        ++n;
        double ff = e.Delta / double.Abs(e.Delta);
        _f = ff * n * 0.9;
        _x = e.Delta * double.Abs(_f);
        _v = ff * double.Sqrt(2 * _f * _x);
        e.Handled = true;
    }

    private double _f = 2;
    private double _v = 0;
    private double _x = 0;
    private int n = 0;

    private void TimerOnTick(object? sender, EventArgs e)
    {
        if (_v / double.Abs(_v) != _f / double.Abs(_f))
        {
            _v = 0;
            return;
        }

        AssociatedObject.ScrollToVerticalOffset(AssociatedObject.VerticalOffset - _v + _f / 2);
        _v -= _f / 2;
    }

    private void TimerOnTick2(object? sender, EventArgs e)
    {
        n = 0;
    }
}