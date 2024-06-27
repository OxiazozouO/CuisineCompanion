using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using CuisineCompanion.Helper;
using CuisineCompanion.Resources;

namespace CuisineCompanion.Views;

public partial class Login
{

    public Login()
    {
        InitializeComponent();
        Loaded += (_, _) => { NameBnt.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent)); };
    }

    private void Longin_OnClick(object sender, RoutedEventArgs e)
    {
        Button clickedButton = (Button)sender;

        string str;
        string icon;
        if (clickedButton == NameBnt)
        {
            str = Strings.Str_Name;
            icon = "IconName";
        }
        else if (clickedButton == EmailBnt)
        {
            str = Strings.Str_Email;
            icon = "IconEmail";
        }
        else if (clickedButton == PhoneBnt)
        {
            str = Strings.Str_Phone;
            icon = "IconPhone";
        }
        else
        {
            str = "";
            icon = "IconName";
        }

        LoginTip.Text = string.Format(Strings.LoginTip, str);
        IdentifierInp.TipBoxText = str;
        IdentifierInp.IconSource = XamlResourceHelper.Icon(icon);

        // 先解锁所有按钮
        NameBnt.IsEnabled = true;
        EmailBnt.IsEnabled = true;
        PhoneBnt.IsEnabled = true;

        // 锁定被点击的按钮
        clickedButton.IsEnabled = false;
    }
}