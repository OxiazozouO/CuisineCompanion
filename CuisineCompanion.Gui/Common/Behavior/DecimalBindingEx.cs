using System.Windows;
using System.Windows.Controls;

namespace CuisineCompanion.Common.Behavior;

public class DecimalBindingEx
{
    public static readonly DependencyProperty TextExProperty =
        DependencyProperty.RegisterAttached("Text", typeof(string), typeof(DecimalBindingEx),
            new PropertyMetadata(string.Empty, OnTextPropertyChanged));

    public static string GetText(DependencyObject obj)
    {
        return (string)obj.GetValue(TextExProperty);
    }

    public static void SetText(DependencyObject obj, string value)
    {
        obj.SetValue(TextExProperty, value);
    }

    private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not TextBox tb) return;
        var newValue = (string)e.NewValue; //Text转换decimal后

        if (newValue != null && tb.Text != newValue) tb.Text = newValue;
    }
}