using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors;

namespace CuisineCompanion.Common.Behavior;

public class DragMoveBehavior : Behavior<Border>
{
    // @formatter:off
    protected override void OnAttached()
    {
       AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
       AssociatedObject.MouseLeftButtonUp   += AssociatedObject_MouseLeftButtonUp;
       AssociatedObject.MouseMove           += AssociatedObject_MouseMove;
    }
    
    protected override void OnDetaching()
    {
        AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
        AssociatedObject.MouseLeftButtonUp   -= AssociatedObject_MouseLeftButtonUp;
        AssociatedObject.MouseMove           -= AssociatedObject_MouseMove;
    }
    // @formatter:on
    private Canvas? _parentCanvas = null;
    private bool _isDragging = false;
    private Point _mouseCurrentPoint;

    private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
    {
        if (!_isDragging) return;
        // 相对于Canvas的坐标
        Point point = e.GetPosition(_parentCanvas);
        // 设置最新坐标
        AssociatedObject.SetValue(Canvas.TopProperty, point.Y - _mouseCurrentPoint.Y);
        AssociatedObject.SetValue(Canvas.LeftProperty, point.X - _mouseCurrentPoint.X);
    }

    private void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (!_isDragging) return;
        // 释放鼠标锁定
        Mouse.Capture(null);
        _isDragging = false;
    }

    private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var ui = (UIElement)sender;
        _isDragging = true;
        _parentCanvas ??= (Canvas?)VisualTreeHelper.GetParent(ui);
        // 当前鼠标坐标
        _mouseCurrentPoint = e.GetPosition(ui);
        Mouse.Capture(AssociatedObject);
    }
}