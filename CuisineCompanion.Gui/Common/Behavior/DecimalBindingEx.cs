using System.Windows;
using System.Windows.Controls;

namespace CuisineCompanion.Common.Behavior;

public class DecimalBindingEx
{
    public static string GetText(DependencyObject obj)
    {
        return (string)obj.GetValue(TextExProperty);
    }

    public static void SetText(DependencyObject obj, string value)
    {
        obj.SetValue(TextExProperty, value);
    }

    public static readonly DependencyProperty TextExProperty =
        DependencyProperty.RegisterAttached("Text", typeof(string), typeof(DecimalBindingEx),
            new PropertyMetadata(string.Empty, OnTextPropertyChanged));

    private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not TextBox tb) return;
        string newValue = (string)e.NewValue; //Text转换decimal后

        if (newValue != null && tb.Text != newValue)
        {
            tb.Text = newValue;
        }
    }
}