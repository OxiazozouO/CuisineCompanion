using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace CuisineCompanion.Common.Behavior;

public class PasswordBindingBehavior : Behavior<PasswordBox>
{
    protected override void OnAttached() =>
        AssociatedObject.PasswordChanged += AssociatedObject_PasswordChanged;

    protected override void OnDetaching() =>
        AssociatedObject.PasswordChanged -= AssociatedObject_PasswordChanged;

    private void AssociatedObject_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (sender is not PasswordBox pb) return;
        if (pb.Password == PasswordBindingEx.GetPassword(pb)) return;
        
        PasswordBindingEx.SetPassword(pb, pb.Password);
    }
}