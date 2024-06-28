using System;
using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CuisineCompanion.Common.Converter;

public class NullOrEmptyToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
            return Visibility.Collapsed;
        if (value is "")
            return Visibility.Collapsed;

        if (value is IEnumerable i)
        {
            foreach (var o in i)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        if (value is < 0.00001m)
            return Visibility.Collapsed;

        if (value is < 0.00001)
            return Visibility.Collapsed;

        if (value is 0)
            return Visibility.Collapsed;

        return Visibility.Visible;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return Visibility.Collapsed;
    }
}