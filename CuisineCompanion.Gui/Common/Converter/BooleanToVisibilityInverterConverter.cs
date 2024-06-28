using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace CuisineCompanion.Common.Converter;

public class BooleanToVisibilityInverterConverter : IValueConverter
{
    private readonly BooleanToVisibilityConverter help = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool b)
            return help.Convert(!b, targetType, parameter, culture);

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool b)
            return help.Convert(!b, targetType, parameter, culture);

        return value;
    }
}