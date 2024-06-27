using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CuisineCompanion.Common.Behavior;

public class PasswordBindingEx
{
    public static string GetPassword(DependencyObject obj)
    {
        return (string)obj.GetValue(PasswordExProperty);
    }

    public static void SetPassword(DependencyObject obj, string value)
    {
        obj.SetValue(PasswordExProperty, value);
    }

    public static readonly DependencyProperty PasswordExProperty =
        DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordBindingEx),
            new PropertyMetadata(string.Empty, OnPasswordPropertyChanged));

    private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not PasswordBox passwordBox) return;
        string password = (string)e.NewValue;
        if (password != null && passwordBox.Password != password)
        {
            passwordBox.Password = password;
        }
    }
}