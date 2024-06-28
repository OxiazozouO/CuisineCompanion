using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace CuisineCompanion.Common.Converter;

public class DecimalConverter : IValueConverter
{
    private static readonly Regex regex = new(@"[^0-9.]");
    public decimal MaxValue { set; get; } = 9999999.999999m;
    public decimal MinValue { set; get; } = 0m;
    public bool IsPositive { set; get; } = true;


    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return FormatString(value?.ToString() ?? null);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return FormatString(value as string);
    }


    public string FormatString(string? nowText)
    {
        if (string.IsNullOrEmpty(nowText)) nowText = "0";

        if (decimal.TryParse(nowText, out var d))
        {
            if (d > MaxValue) d = MaxValue;
            if (IsPositive && d < 0) d = 0;
            if (d < MinValue) d = MinValue;
            nowText = d.ToString();
        }

        return nowText;
    }

    public static string FilterString(string s)
    {
        if (s == "") return "0";

        var result = regex.Replace(s, "");
        var arr = result.Split('.');
        if (arr.Length > 1)
        {
            var ret = new StringBuilder().Append(arr[0]).Append('.');
            for (var i = 1; i < arr.Length; i++) ret.Append(arr[i]);

            result = ret.ToString();
        }


        if (result.Length > 0 && (result[0] == '.' || result[^1] == '.'))
        {
            if (result[0] == '.') result = '0' + result;

            if (result[^1] == '.') result += '0';
        }

        return result;
    }

    public bool FilterInput(string text, string oldText, out string result)
    {
        result = "";
        if (text == oldText) return false;
        if (text == "") return false;

        var ans = text.Sum(c => c == '.' ? 0 : c - '0');
        if (ans == 0) return false;

        var arr = text.Split('.');
        if (arr.Length > 2)
        {
            var ret = new StringBuilder().Append(arr[0]).Append('.');
            for (var i = 1; i < arr.Length; i++) ret.Append(arr[i]);

            result = ret.ToString();
            return true;
        }

        if (text[0] == '.' || text[^1] == '.') return false;


        result = FormatString(FilterString(text));
        return text != result;
    }
}