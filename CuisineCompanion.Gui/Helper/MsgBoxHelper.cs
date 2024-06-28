using System.Windows;
using static System.Windows.MessageBox;

namespace CuisineCompanion.Helper;

public static class MsgBoxHelper
{
    public static void Info(string message, string caption = "信息")
    {
        Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public static void Warning(string message, string caption = "警告")
    {
        Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    public static bool TryError(string? message, string caption = "错误")
    {
        if (string.IsNullOrEmpty(message)) return false;
        Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        return true;
    }

    public static bool Confirmation(string message, string caption = "确认")
    {
        var result = Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
        return result == MessageBoxResult.Yes;
    }

    public static MessageBoxResult Custom(string message, string caption, MessageBoxButton buttons,
        MessageBoxImage icon)
    {
        return Show(message, caption, buttons, icon);
    }

    public static bool OkCancel(string message, string caption = "提示")
    {
        var result = Show(message, caption, MessageBoxButton.OKCancel, MessageBoxImage.Question);
        return result == MessageBoxResult.OK;
    }
}