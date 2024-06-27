using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace CuisineCompanion.Common.Converter;

public class DecimalConverter : IValueConverter
{
    public decimal MaxValue { set; get; } = 9999999.999999m;
    public decimal MinValue { set; get; } = 0m;
    public bool IsPositive { set; get; } = true;


    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => 
        FormatString(value?.ToString() ?? null);

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        FormatString(value as string);


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


    private static readonly Regex regex = new Regex(@"[^0-9.]");

    public static string FilterString(string s)
    {
        if (s == "") return "0";

        string result = regex.Replace(s, "");
        var arr = result.Split('.');
        if (arr.Length > 1)
        {
            StringBuilder ret = new StringBuilder().Append(arr[0]).Append('.');
            for (int i = 1; i < arr.Length; i++)
            {
                ret.Append(arr[i]);
            }

            result = ret.ToString();
        }


        if (result.Length > 0 && (result[0] == '.' || result[^1] == '.'))
        {
            if (result[0] == '.')
            {
                result = '0' + result;
            }

            if (result[^1] == '.')
            {
                result += '0';
            }
        }

        return result;
    }

    public bool FilterInput(string text, string oldText, out string result)
    {
        result = "";
        if (text == oldText) return false;
        if (text == "")
        {
            return false;
        }

        int ans = text.Sum(c => c == '.' ? 0 : (c - '0'));
        if (ans == 0) return false;

        var arr = text.Split('.');
        if (arr.Length > 2)
        {
            StringBuilder ret = new StringBuilder().Append(arr[0]).Append('.');
            for (int i = 1; i < arr.Length; i++)
            {
                ret.Append(arr[i]);
            }

            result = ret.ToString();
            return true;
        }

        if (text[0] == '.' || text[^1] == '.')
        {
            return false;
        }


        result = FormatString(FilterString(text));
        return text != result;
    }
}