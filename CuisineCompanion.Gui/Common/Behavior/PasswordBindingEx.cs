using System.Windows;
using System.Windows.Controls;

namespace CuisineCompanion.Common.Behavior;

public class PasswordBindingEx
{
    public static readonly DependencyProperty PasswordExProperty =
        DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordBindingEx),
            new PropertyMetadata(string.Empty, OnPasswordPropertyChanged));

    public static string GetPassword(DependencyObject obj)
    {
        return (string)obj.GetValue(PasswordExProperty);
    }

    public static void SetPassword(DependencyObject obj, string value)
    {
        obj.SetValue(PasswordExProperty, value);
    }

    private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not PasswordBox passwordBox) return;
        var password = (string)e.NewValue;
        if (password != null && passwordBox.Password != password) passwordBox.Password = password;
    }
}