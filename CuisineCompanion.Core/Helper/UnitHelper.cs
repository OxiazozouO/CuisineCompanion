using System;
using static CuisineCompanion.ViewModels.MainViewModel;

namespace CuisineCompanion.Helper;

public static class UnitHelper
{
    public static decimal ConvertUnit(decimal value, string input, string output)
    {
        if (input == output) return value;
        if (Unit.TryGetValue(input, out var i) && Unit.TryGetValue(output, out var o)) return value * i / o;

        throw new ArgumentException();
    }

    public static decimal ConvertBaseUnitTo(decimal value, string input)
    {
        return ConvertUnit(value, GetBaseUnit(input), input);
    }

    public static decimal ConvertToBaseUnit(decimal value, string input)
    {
        return ConvertUnit(value, input, GetBaseUnit(input));
    }

    public static void ConvertToClosestUnit(decimal value, string input, out decimal result, out string output)
    {
        if (Unit.TryGetValue(input, out var i))
        {
            if (UnitNext.TryGetValue(input, out var a) && a is not null &&
                Unit.TryGetValue(a, out var ma) &&
                input != a && Math.Abs(value) >= Math.Abs(ma / i))
            {
                value = value * i / ma;
                input = a;
                ConvertToClosestUnit(value, input, out result, out output);
                return;
            }

            if (UnitPre.TryGetValue(input, out var b) && b is not null &&
                Unit.TryGetValue(b, out var mi) && input != b && Math.Abs(value) < 1)
            {
                value = value * i / mi;
                input = b;
                ConvertToClosestUnit(value, input, out result, out output);
                return;
            }
        }

        result = value;
        output = input;
    }

    public static string GetBaseUnit(string input)
    {
        if (BaseUnit.TryGetValue(input, out var ret)) return ret ?? input;

        throw new ArgumentException();
    }

    public static string GetLocalName(string input, string local = "zh")
    {
        if (Local[local].TryGetValue(input, out var ret)) return ret;

        throw new ArgumentException();
    }
}