using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;
using WS = System.Windows.WindowState;

namespace CuisineCompanion.Common.Behavior;

public class WindowStateManagementBehavior : Behavior<UIElement>
{
    public enum EventMod
    {
        Close,
        Minimize,
        Maximize,
        DragMoveResize,
        DoubleClickMaximize
    }

    public EventMod Mod { get; set; }


    private readonly Window _window = Application.Current.MainWindow;

    protected override void OnAttached()
    {
        if (_window is null) return;
        switch (AssociatedObject)
        {
            case Button button:
                switch (Mod)
                {
                    case EventMod.Close:
                        button.Click += CloseButtonOnClick;
                        break;
                    case EventMod.Minimize:
                        button.Click += MinimizeButtonOnClick;
                        break;
                    case EventMod.Maximize:
                        button.Click += MaximizeButtonOnClick;
                        break;
                }

                break;
            case Image image:
                switch (Mod)
                {
                    case EventMod.Close:
                        image.MouseDown += CloseButtonOnClick;
                        break;
                    case EventMod.Minimize:
                        image.MouseDown += MinimizeButtonOnClick;
                        break;
                    case EventMod.Maximize:
                        image.MouseDown += MaximizeButtonOnClick;
                        break;
                }

                break;
            default:
                switch (Mod)
                {
                    case EventMod.DragMoveResize:
                        AssociatedObject.MouseDown += DragMoveResizeUIElement_MouseDown;
                        break;
                    case EventMod.DoubleClickMaximize:
                        _window.MouseDoubleClick += Window_MouseDoubleClick;
                        break;
                }

                break;
        }
    }

    protected override void OnDetaching()
    {
        if (_window is null) return;
        switch (AssociatedObject)
        {
            case Button button:
                switch (Mod)
                {
                    case EventMod.Close:
                        button.Click -= CloseButtonOnClick;
                        break;
                    case EventMod.Minimize:
                        button.Click -= MinimizeButtonOnClick;
                        break;
                    case EventMod.Maximize:
                        button.Click -= MaximizeButtonOnClick;
                        break;
                }

                break;
            case Image image:
                switch (Mod)
                {
                    case EventMod.Close:
                        image.MouseDown -= CloseButtonOnClick;
                        break;
                    case EventMod.Minimize:
                        image.MouseDown -= MinimizeButtonOnClick;
                        break;
                    case EventMod.Maximize:
                        image.MouseDown -= MaximizeButtonOnClick;
                        break;
                }

                break;
            default:
                switch (Mod)
                {
                    case EventMod.DragMoveResize:
                        AssociatedObject.MouseDown -= DragMoveResizeUIElement_MouseDown;
                        break;
                    case EventMod.DoubleClickMaximize:
                        _window.MouseDoubleClick -= Window_MouseDoubleClick;
                        break;
                }

                break;
        }
    }

    private void CloseButtonOnClick(object sender, RoutedEventArgs e) =>
        _window.Close();

    private void MinimizeButtonOnClick(object sender, RoutedEventArgs e) =>
        _window.WindowState = WS.Minimized;

    private void MaximizeButtonOnClick(object sender, RoutedEventArgs e) =>
        _window.WindowState = _window.WindowState == WS.Maximized ? WS.Normal : WS.Maximized;

    private void DragMoveResizeUIElement_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton != MouseButton.Left) return;
        if (_window.WindowState == WS.Maximized)
        {
            _window.WindowState = WS.Normal;
        }

        try
        {
            _window.DragMove();
        }
        catch
        {
            // ignored
        }
    }

    private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton != MouseButton.Left) return;
        _window.WindowState = _window.WindowState == WS.Maximized ? WS.Normal : WS.Maximized;
    }
}