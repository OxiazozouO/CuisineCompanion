// namespace CuisineCompanion.Helpers;
//
// using System;
//
// public struct DecimalWrapper
// {
//     public decimal MaxValue = decimal.MaxValue;
//     public decimal MinValue = decimal.MinValue;
//     public bool IsPositive = false;
//     private decimal _value;
//
//     public decimal Value
//     {
//         get => _value;
//         set
//         {
//             if (value > MaxValue)
//                 value = MaxValue;
//             else if (value < MinValue)
//                 value = MinValue;
//             _value = value;
//         }
//     }
//
//     public DecimalWrapper(decimal value)
//     {
//         Value = value;
//     }
//
//     public DecimalWrapper(decimal value, bool isPositive, decimal maxValue, decimal minValue)
//     {
//         IsPositive = isPositive;
//         MaxValue = maxValue;
//         MinValue = minValue;
//         Value = value;
//     }
//
//     public DecimalWrapper() => Value = 0m;
//
//     public static implicit operator DecimalWrapper(decimal d) => new DecimalWrapper(d);
//
//     public static implicit operator decimal(DecimalWrapper wrapper) => wrapper.Value;
//
//     public static DecimalWrapper operator +(DecimalWrapper d1, DecimalWrapper d2) =>
//         new(d1.Value + d2.Value, d1.IsPositive, d1.MinValue, d1.MaxValue);
//
//     public static DecimalWrapper operator -(DecimalWrapper d1, DecimalWrapper d2) =>
//         new(d1.Value - d2.Value, d1.IsPositive, d1.MinValue, d1.MaxValue);
//
//     public static DecimalWrapper operator *(DecimalWrapper d1, DecimalWrapper d2) =>
//         new(d1.Value * d2.Value, d1.IsPositive, d1.MinValue, d1.MaxValue);
//
//     public static DecimalWrapper operator /(DecimalWrapper d1, DecimalWrapper d2) =>
//         new(d1.Value / d2.Value, d1.IsPositive, d1.MinValue, d1.MaxValue);
//
//     public static bool operator ==(DecimalWrapper d1, DecimalWrapper d2) => d1.Value == d2.Value;
//
//     public static bool operator !=(DecimalWrapper d1, DecimalWrapper d2) => d1.Value != d2.Value;
//
//     public static bool operator >(DecimalWrapper d1, DecimalWrapper d2) => d1.Value > d2.Value;
//
//     public static bool operator <(DecimalWrapper d1, DecimalWrapper d2) => d1.Value < d2.Value;
//
//     public static bool operator >=(DecimalWrapper d1, DecimalWrapper d2) => d1.Value >= d2.Value;
//
//     public static bool operator <=(DecimalWrapper d1, DecimalWrapper d2) => d1.Value <= d2.Value;
//
//     public override bool Equals(object? obj) => Value.Equals(obj);
//
//     public override int GetHashCode() => Value.GetHashCode();
//
//     public override string ToString() => Value.ToString();
// }