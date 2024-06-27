using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using CuisineCompanion.Common.Behavior;
using Microsoft.Xaml.Behaviors;

namespace CuisineCompanion.Common;

public partial class TipTextBox
{
    public static readonly DependencyProperty TipProperty =
        DependencyProperty.Register(nameof(TipBoxText), typeof(string), typeof(TipTextBox));

    public static readonly DependencyProperty ErrorVisibilityProperty =
        DependencyProperty.Register(nameof(ErrorVisibility), typeof(Visibility), typeof(TipTextBox),
            new PropertyMetadata(Visibility.Visible));

    public static readonly DependencyProperty IconSourceProperty =
        DependencyProperty.Register(nameof(IconSource), typeof(ImageSource), typeof(TipTextBox));

    public static readonly DependencyProperty InputViewProperty =
        DependencyProperty.Register(nameof(InputView), typeof(Control), typeof(TipTextBox));

    public static readonly DependencyProperty IsErrorProperty =
        DependencyProperty.Register(nameof(IsError), typeof(bool), typeof(TipTextBox));

    public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register(nameof(BorderBrush),
        typeof(Brush), typeof(TipTextBox));

    public static readonly DependencyProperty BorderThicknessProperty = DependencyProperty.Register(
        nameof(BorderThickness), typeof(Thickness), typeof(TipTextBox));

    public Brush BorderBrush
    {
        get => (Brush)GetValue(BorderBrushProperty);
        set => SetValue(BorderBrushProperty, value);
    }

    public Thickness BorderThickness
    {
        get => (Thickness)GetValue(BorderThicknessProperty);
        set => SetValue(BorderThicknessProperty, value);
    }

    public string TipBoxText
    {
        get => (string)GetValue(TipProperty);
        set => SetValue(TipProperty, value);
    }

    public Visibility ErrorVisibility
    {
        get => (Visibility)GetValue(ErrorVisibilityProperty);
        set => SetValue(ErrorVisibilityProperty, value);
    }

    public ImageSource IconSource
    {
        get => (ImageSource)GetValue(IconSourceProperty);
        set => SetValue(IconSourceProperty, value);
    }


    public Control InputView
    {
        get => (Control)GetValue(InputViewProperty);
        set => SetValue(InputViewProperty, value);
    }

    public bool IsError
    {
        get => (bool)GetValue(IsErrorProperty);
        set => SetValue(IsErrorProperty, value);
    }

    public TipTextBox()
    {
        InitializeComponent();
        Loaded += (sender, args) =>
        {
            if (InputView is null)
            {
                DataContext = this;
                return;
            }

            if (!Input.Children.Contains(InputView))
            {
                Input.DataContext = DataContext;
                DataContext = this;
                Input.Children.Add(InputView);
                AddBinding();
            }
        };
    }

    private void AddBinding()
    {
        ErrorBox.SetBinding(TextBox.TextProperty, new Binding()
            {
                Source = InputView,
                Path = new PropertyPath("(Validation.Errors)[0].ErrorContent"),
            }
        );

        SetBinding(IsErrorProperty, new Binding
        {
            Source = InputView,
            Path = new PropertyPath("(Validation.HasError)"),
        });


        Interaction.GetBehaviors(InputView).Add(new ContentChangedBehavior((o, args, content) =>
        {
            Changed((string?)content);
        }));
    }

    private void Changed(string? s)
    {
        bool b = !string.IsNullOrEmpty(s) && s.Length > 0;
        ClearImg.Visibility = b
            ? Visibility.Visible
            : Visibility.Collapsed;
        Tip.Visibility = b
            ? Visibility.Collapsed
            : Visibility.Visible;
    }
}