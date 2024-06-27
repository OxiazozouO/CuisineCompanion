using System;
using System.Globalization;
using System.Windows.Data;
using CuisineCompanion.ViewModels;

namespace CuisineCompanion.Common.Converter;

public class DateTimeFormatConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime date)
        {
            return date.ToString(parameter.ToString(), MainViewModel.I18N);
        }

        return value; // 如果输入不是DateTime类型，则原样返回
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}