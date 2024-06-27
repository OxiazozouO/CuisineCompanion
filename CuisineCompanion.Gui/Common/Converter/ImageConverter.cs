using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace CuisineCompanion.Common.Converter;

/// <summary>
/// 不占用图片资源
/// </summary>
public class ImageConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        string? s = value?.ToString();
        if (s == null) return null;
        var url = new Uri(s);
        BitmapImage bitmap = new BitmapImage();
        bitmap.BeginInit();
        bitmap.CacheOption = BitmapCacheOption.OnLoad;
        bitmap.UriSource = url;
        bitmap.EndInit();
        return bitmap.Clone();
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }
}