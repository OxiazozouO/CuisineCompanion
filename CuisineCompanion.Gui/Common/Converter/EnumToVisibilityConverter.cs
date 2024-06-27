using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Ct = System.Convert;

namespace CuisineCompanion.Common.Converter;

public class EnumToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            long l = Ct.ToInt64(value);
            long r = Ct.ToInt64(parameter);
            if ((l & r) == r)
            {
                return Visibility.Visible;
            }
        }
        catch
        {
            // ignored
        }

        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}