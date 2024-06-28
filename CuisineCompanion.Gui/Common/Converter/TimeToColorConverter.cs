using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using CuisineCompanion.Helper;

namespace CuisineCompanion.Common.Converter;

public class TimeToColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not TimeSpan spendTime) return Brushes.Black; // 默认颜色
        // 将TimeSpan转换为分钟
        double minutes = spendTime.Minutes + spendTime.Hours * 60;
        double days = spendTime.Days;

        // 计算渐变色的起始和结束颜色
        Color startColor;
        Color endColor;

        switch (minutes)
        {
            case < 30:
                startColor = Colors.Green;
                endColor = Colors.Yellow;
                break;
            case >= 30 and < 60:
                startColor = Colors.Green;
                endColor = Colors.Yellow;
                break;
            case >= 60 and < 90:
                startColor = Colors.Yellow;
                endColor = Colors.Orange;
                break;
            case >= 90 and < 120:
                startColor = Colors.Orange;
                endColor = Colors.OrangeRed;
                break;
            default:
                startColor = Colors.OrangeRed;
                endColor = Colors.Purple;
                break;
        }

        if (days > 0)
        {
            startColor = Colors.OrangeRed;
            endColor = Colors.Purple;
        }

        return ColorHelper.Create(startColor, endColor);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Brushes.Black;
    }
}