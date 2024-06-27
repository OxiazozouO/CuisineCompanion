using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace CuisineCompanion.Common.Behavior;

public class DraggableNumericBehavior : Behavior<TextBox>
{
    private bool isDown;
    private bool isMove;
    private Point startPoint;
    private decimal staTextPos;
// @formatter:off
    protected override void OnAttached()
    {
        AssociatedObject.PreviewMouseLeftButtonUp   += AssociatedObjectOnPreviewMouseLeftButtonUp;
        AssociatedObject.PreviewMouseMove           += AssociatedObjectOnPreviewMouseMove;
        AssociatedObject.PreviewMouseLeftButtonDown += AssociatedObjectOnPreviewMouseLeftButtonDown;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.PreviewMouseLeftButtonUp   -= AssociatedObjectOnPreviewMouseLeftButtonUp;
        AssociatedObject.PreviewMouseMove           -= AssociatedObjectOnPreviewMouseMove;
        AssociatedObject.PreviewMouseLeftButtonDown -= AssociatedObjectOnPreviewMouseLeftButtonDown;
    }
// @formatter:on
    private void AssociatedObjectOnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        // 鼠标左键按下时开始监测鼠标移动
        if (e.LeftButton != MouseButtonState.Pressed) return;
        isDown = true;
        AssociatedObject.IsReadOnly = true;

        startPoint = e.GetPosition(AssociatedObject);
        staTextPos = decimal.Parse(AssociatedObject.Text == "" ? "0" : AssociatedObject.Text);
        AssociatedObject.CaptureMouse();
    }

    private void AssociatedObjectOnPreviewMouseMove(object sender, MouseEventArgs e)
    {
        if (!isDown || e.LeftButton != MouseButtonState.Pressed) return;
        var pos = e.GetPosition(AssociatedObject) - startPoint;
        isMove |= Math.Abs(pos.X) > 0.1;
        if (!isMove) return;

        Point currentPoint = e.GetPosition(AssociatedObject);
        decimal deltaX = (decimal)(currentPoint.X - startPoint.X);
        deltaX += staTextPos;
        if (deltaX < 0) deltaX = 0;
        AssociatedObject.Text = $"{deltaX:0.###}";
    }

    private void AssociatedObjectOnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        // 释放鼠标时，根据是否有移动动作来决定是否进入键入模式
        AssociatedObject.ReleaseMouseCapture();

        if (!isMove)
        {
            AssociatedObject.IsReadOnly = false;
            AssociatedObject.Focus(); // 进入键入模式
        }
        else
        {
            //退出键入模式
            AssociatedObject.RaiseEvent(new RoutedEventArgs(TextBox.LostFocusEvent));
        }

        isDown = false;
        isMove = false;
    }
}