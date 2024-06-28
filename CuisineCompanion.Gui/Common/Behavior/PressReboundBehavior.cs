using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using CuisineCompanion.Helper;
using Microsoft.Xaml.Behaviors;

namespace CuisineCompanion.Common.Behavior;

public class PressReboundBehavior : Behavior<FrameworkElement>
{
    private static readonly Storyboard PressRebound = XamlResourceHelper.Anime("PressRebound");

    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(
            nameof(Command),
            typeof(ICommand),
            typeof(PressReboundBehavior),
            new PropertyMetadata(null));

    public static readonly DependencyProperty CommandParameterProperty =
        DependencyProperty.Register(
            nameof(CommandParameter),
            typeof(object),
            typeof(PressReboundBehavior),
            new PropertyMetadata(null));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }


    protected override void OnAttached()
    {
        AssociatedObject.SetTransformGroup();
        AssociatedObject.HorizontalAlignment = HorizontalAlignment.Center;
        AssociatedObject.VerticalAlignment = VerticalAlignment.Center;
        AssociatedObject.RenderTransformOrigin = new Point(0.5, 0.5);

        AssociatedObject.MouseDown += AssociatedObject_MouseDown;
        if (AssociatedObject is Button b) b.Click += AssociatedObject_MouseDown;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.MouseDown -= AssociatedObject_MouseDown;
        if (AssociatedObject is Button b) b.Click -= AssociatedObject_MouseDown;
    }

    private void AssociatedObject_MouseDown(object sender, EventArgs e)
    {
        AssociatedObject.BeginStoryboard(PressRebound);
        if (Command is not null) Command.Execute(CommandParameter);
    }
}