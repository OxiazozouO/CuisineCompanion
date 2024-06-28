using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CuisineCompanion.Common.Converter;

public class TimeSpanToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null) return Visibility.Collapsed;
        if (value is not TimeSpan time) return Visibility.Collapsed;
        if (time.Ticks < 10000000) return Visibility.Collapsed;
        return Visibility.Visible;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return Visibility.Collapsed;
    }
}