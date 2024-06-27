using System;
using System.Globalization;
using System.Windows.Data;

namespace CuisineCompanion.Common.Converter;

public class TimeSpanConverter : IValueConverter
{
    private string? _fmt;
    private string? _day;
    private string? _h;
    private string? _min;
    private string? _s;

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (_fmt == null) InitFmt(parameter);
        string ret = "";
        // @formatter:off
        if (value is TimeSpan timeSpan)
            if (timeSpan.Days != 0)
                ret = $"{timeSpan.Days} {_day} {timeSpan.Hours} {_h} {timeSpan.Minutes} {_min} {timeSpan.Seconds} {_s}";
            else if (timeSpan.Hours != 0)
                ret =                        $"{timeSpan.Hours} {_h} {timeSpan.Minutes} {_min} {timeSpan.Seconds} {_s}";
            else if (timeSpan.Minutes != 0)
                ret =                                              $"{timeSpan.Minutes} {_min} {timeSpan.Seconds} {_s}";
            else if (timeSpan.Seconds != 0)
                ret =                                                                        $"{timeSpan.Seconds} {_s}";
        return ret;
        // @formatter:on
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not TimeSpan timeSpan) return null;
        return timeSpan.ToString();
    }

    private void InitFmt(object? parameter)
    {
        _fmt ??= parameter as string ?? "天:小时:分:秒";
        var arr = _fmt.Split(':');
        (_day, _h, _min, _s) = arr.Length != 4 ? ("天", "小时", "分", "秒") : (arr[0], arr[1], arr[2], arr[3]);
    }
}