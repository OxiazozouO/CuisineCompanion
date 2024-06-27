using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using CuisineCompanion.Common;
using CuisineCompanion.Models;
using CuisineCompanion.ViewModels;

namespace CuisineCompanion.Helper;

public static class NutritionalHelper
{
    /// <summary>
    /// 静息代谢
    /// </summary>
    /// <param name="weight">体重</param>
    /// <param name="height">身高</param>
    /// <param name="birthday">生日</param>
    /// <param name="sex">性别</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static double GetMifflinStJeorREE(double weight, double height, DateTime birthday, bool sex)
    {
        double age = birthday.GetAge();
        var ret = sex switch
        {
            true => 9.99 * weight + 6.25 * height - 4.92 * age + 5,
            false => 9.99 * weight + 6.25 * height - 4.92 * age - 161
        };
        return Math.Max(0, ret);
    }

    /// <summary>
    /// 计算BMI  来自中国肥胖预防和控制蓝皮书
    /// </summary>
    /// <param name="weight">体重 kg</param>
    /// <param name="height">身高 cm</param>
    /// <returns></returns>
    public static double GetBmi(double weight, double height)
    {
        if (weight == 0 || height == 0) return 0;
        return weight * 10000 / (height * height);
    }

    /// <summary>
    /// 中国肥胖预防和控制蓝皮书
    /// </summary>
    public static string GetBmiString(double bmp)
    {
        return bmp switch
        {
            < 0.000001 => "",
            < 18.5 => "消瘦",
            <= 23.9 => "标准",
            <= 27.9 => "超重",
            _ => "肥胖",
        };
    }

    /// <summary>
    /// 基础代谢
    /// </summary>
    /// <param name="weight"></param>
    /// <param name="height"></param>
    /// <param name="birthday"></param>
    /// <param name="sex"></param>
    /// <param name="activityFactor"></param>
    /// <returns></returns>
    public static double TDEE(double weight, double height, DateTime birthday, bool sex, double activityFactor)
    {
        return GetMifflinStJeorREE(weight, height, birthday, sex) * activityFactor;
    }

    public static double TDEE(double ree, double activityFactor)
    {
        return ree * activityFactor;
    }

    /// <summary>
    /// 获得对营养素含量的评价
    /// </summary>
    /// <param name="allNutritional">每100克可食部的营养素含量</param>
    /// <param name="ans">可食部量</param>
    /// <returns></returns>
    public static List<Tuple<EvaluateTag, string>> GetEvaluateTag(Dictionary<string, decimal> allNutritional,
        decimal ans)
    {
        //先按比例计算出营养素
        var result = new List<Tuple<EvaluateTag, string>>();
        var dic = new Dictionary<string, Tuple<decimal, string>>();
        var nrv = new Dictionary<string, decimal>();
        {
            foreach (var (key, value) in allNutritional)
            {
                if (!MainViewModel.TryGetNutritional(key, out var unit)) continue;
                //   *100/ans  每100g可食部中营养素的含量
                dic[key] = new(value * 100 / ans, unit.Item1);
                // 食品中某营养素的含量/该营养素的营养素参考值 *100%
                if (unit.Item2 != 0)
                    nrv[key] = dic[key].Item1 * 100 / unit.Item2;
            }
        }

        if (dic.TryGetValue("热量 kcal", out var e))
        {
            decimal eKcal = e.Item1;
            decimal eKj = UnitHelper.ConvertUnit(e.Item1, e.Item2, "kJ");
            // 低能量 ≤ 170 kJ/100g 固体  ≤ 80 kJ/100ml 液体
            if (eKj <= 170) result.Add(new(EvaluateTag.Positive, "低能量"));

            // 无或零能量 ≤ 17 kJ/100g（固体）或100ml（液体
            if (eKj <= 17) result.Add(new(EvaluateTag.Positive, "无能量"));

            //（中国居民膳食指南） 高能量 400kcal/100g 以上
            if (eKcal >= 400) result.Add(new(EvaluateTag.Negative, "高能量"));

            if (dic.TryGetValue("蛋白质", out var d))
            {
                //来自蛋白质的能量 kcal
                decimal dd = d.Item1 * 4m;
                // 低蛋白 来自蛋白质的能量 ≤总能量的5 %
                if (dd <= eKcal * 0.05m)
                {
                    result.Add(new(EvaluateTag.Negative, "低蛋白"));
                }
            }

            if (nrv.TryGetValue("蛋白质", out var ddd))
            {
                switch (ddd)
                {
                    //蛋白质来源或含有蛋白质或提供蛋白质 每100 g的含量≥10 % NRV 每100 ml的含量 ≥5 % NRV 或者 每420 kJ的含量 ≥5 % NRV
                    case >= 10m:
                        result.Add(new(EvaluateTag.Positive, "富含蛋白质"));
                        break;
                    //高或富含蛋白质或蛋白质丰富“来源”的两倍以上
                    case >= 5m:
                        result.Add(new(EvaluateTag.Positive, "提供蛋白质"));
                        break;
                }
            }
        }

        if (dic.TryGetValue("脂肪", out var z))
        {
            switch (z.Item1)
            {
                //零，无或不含脂肪 ≤0.5 g/100g（固体）或100ml（液体） 
                case <= 0.5m:
                    result.Add(new(EvaluateTag.Positive, "零脂肪"));
                    break;
                //低脂肪 ≤3 g/100g固体；≤1.5 g/100ml液体
                case <= 3m:
                    result.Add(new(EvaluateTag.Positive, "低脂肪"));
                    break;
                case >= 20m:
                    result.Add(new(EvaluateTag.Negative, "高脂肪"));
                    break;
            }
        }

        decimal ans2 = dic.Where(d => d.Key.Contains("饱和脂肪") && d.Key.Contains("脂肪酸"))
            .Sum(d => UnitHelper.ConvertToBaseUnit(d.Value.Item1, d.Value.Item2));
        switch (ans2)
        {
            //零，无或不含饱和脂肪 ≤0.1 g/ 100g（固体）或100ml（液体）
            case <= 0.1m:
                result.Add(new(EvaluateTag.Positive, "不含饱和脂肪"));
                break;
            //低饱和脂肪 ≤1.5 g/100g固体 ≤0.75 g /100mL液体
            case <= 1.5m:
                result.Add(new(EvaluateTag.Positive, "低饱和脂肪"));
                break;
            case >= 5m:
                result.Add(new(EvaluateTag.Negative, "高饱和脂肪"));
                break;
        }

        if (dic.TryGetValue("胆固醇", out var d2))
        {
            switch (UnitHelper.ConvertUnit(d2.Item1, d2.Item2, "mg"))
            {
                //无、或不含、零胆固醇 ≤0.005 g/100g（固体）或100ml（液体）
                case <= 0.005m:
                    result.Add(new(EvaluateTag.Positive, "零胆固醇"));
                    break;
                //低胆固醇 ≤20m g /100g固体； ≤10mg /100ml液体。
                case <= 20m:
                    result.Add(new(EvaluateTag.Positive, "低胆固醇"));
                    break;
                case >= 100m:
                    result.Add(new(EvaluateTag.Negative, "高胆固醇"));
                    break;
            }
        }

        if (dic.TryGetValue("碳水化合物", out var d3))
        {
            switch (UnitHelper.ConvertUnit(d3.Item1, d3.Item2, "g"))
            {
                //无或不含碳水化合物 ≤ 0.5 g /100g（固体）或100ml（液体）
                case <= 0.5m:
                    result.Add(new(EvaluateTag.Positive, "不含碳水化合物"));
                    break;
                //低碳水化合物 ≤ 5 g /100g（固体）或100ml（液体）
                case <= 5m:
                    result.Add(new(EvaluateTag.Positive, "低碳水化合物"));
                    break;
                ////（中国居民膳食指南） 高糖 400kcal/100g 以上
            }
        }

        if (dic.TryGetValue("钠", out var d4))
        {
            switch (UnitHelper.ConvertUnit(d4.Item1, d4.Item2, "mg"))
            {
                //无、或不含、零钠 ≤5 mg /100g（固体）或100ml（液体）
                case <= 5m:
                    result.Add(new(EvaluateTag.Positive, "零钠"));
                    break;
                //极低钠 ≤40 mg /100g或100ml
                case <= 40m:
                    result.Add(new(EvaluateTag.Positive, "极低钠"));
                    break;
                //低钠 ≤120 mg /100g或100ml
                case <= 120m:
                    result.Add(new(EvaluateTag.Positive, "低钠"));
                    break;
                ////（中国居民膳食指南） 高盐 800/100g 以上
                case >= 800m:
                    result.Add(new(EvaluateTag.Negative, "高钠"));
                    break;
            }
        }


        var list = new List<string>() { "钙", "磷", "镁", "铁", "锌", "铜", "锰", "碘", "硒", "钼", "铬", "氟" };
        var list2 = nrv.Where(d => list.Contains(d.Key) && d.Value >= 15m)
            .ToList();
        foreach (var (key, value) in list2)
        {
            switch (value)
            {
                //高或富含xx或xx 的良好来源 “来源”的两倍以上 
                case >= 30m:
                    result.Add(new(EvaluateTag.Positive, $"富含 {key}"));
                    break;
                // 矿物质 xx来源 或含有xx 或提供xx 每100 g中 ≥15% NRV 每100 ml中 ≥7.5% NRV  或者 每420 kJ中 ≥5% NRV
                case >= 15m:
                    result.Add(new(EvaluateTag.Positive, $"含有 {key}"));
                    break;
            }
        }

        var list3 = nrv.Where(d => d.Key.Contains("维生素") && d.Value >= 15m)
            .ToList();
        foreach (var (key, value) in list3)
        {
            switch (value)
            {
                case >= 30m:
                    result.Add(new(EvaluateTag.Positive, $"富含 {key}"));
                    break;
                case >= 15m:
                    result.Add(new(EvaluateTag.Positive, $"含有 {key}"));
                    break;
            }
        }

        //多维 添加3种以上的维生素 含量符合上述相应来源的含量要求
        if (list3.Count > 3)
        {
            result.Add(new(EvaluateTag.Positive, "多维"));
        }

        //膳食纤维来源或含有膳食纤维   ≥3 g/ 100g，  ≥1.5 g/ 100ml 
        //高或富含膳食纤维或良好来源 “来源”的两倍以上
        if (dic.TryGetValue("膳食纤维", out var b))
        {
            decimal value = UnitHelper.ConvertToBaseUnit(b.Item1, b.Item2);
            switch (value)
            {
                case >= 6m:
                    result.Add(new(EvaluateTag.Positive, "富含膳食纤维"));
                    break;
                case >= 3m:
                    result.Add(new(EvaluateTag.Positive, "含有膳食纤维"));
                    break;
            }
        }

        if (e is not null)
        {
            dic.TryGetValue("碳水化合物", out var aaa);
            dic.TryGetValue("脂肪", out var bbb);
            dic.TryGetValue("蛋白质", out var ccc);

            decimal ans4 = UnitHelper.ConvertUnit(aaa?.Item1 ?? 0m, aaa?.Item2 ?? "g", "g") +
                           UnitHelper.ConvertUnit(bbb?.Item1 ?? 0m, bbb?.Item2 ?? "g", "g") +
                           UnitHelper.ConvertUnit(ccc?.Item1 ?? 0m, ccc?.Item2 ?? "g", "g");
            if (ans4 <= 1)
            {
                result.Add(new(EvaluateTag.Negative, "纯热量食物"));
            }
        }

        return result;
    }

    public static Dictionary<EvaluateTag, List<string>> ToDictionary(List<Tuple<EvaluateTag, string>> list)
    {
        var ret = new Dictionary<EvaluateTag, List<string>>();
        foreach (var (item1, item2) in list)
        {
            if (!ret.ContainsKey(item1))
            {
                ret[item1] = new List<string>();
            }

            ret[item1].Add(item2);
        }

        return ret;
    }


    /// <summary>
    /// 生成模型
    /// </summary>
    /// <param name="dic">所有营养素</param>
    /// <param name="list1">百分比饼图模型 其他元素</param>
    /// <param name="list2">百分比饼图模型 三大元素</param>
    /// <param name="energy">千焦 千卡</param>
    /// <param name="otherNutrient">其他营养素</param>
    /// <param name="proteinContent">三大营养素</param>
    public static void InitAllNutritional(Dictionary<string, decimal> dic,
        out List<PieSegmentModel> list1, out List<PieSegmentModel> list2,
        out List<Tuple<string, decimal>> energy,
        out List<NutrientContentModel> otherNutrient,
        out List<NutrientContentViewModel> proteinContent)
    {
        InitAllNutritional(dic, out list1, out energy, out otherNutrient, out proteinContent);
        list2 = list1.Where(pieSegmentModel => pieSegmentModel.Id == 1).ToList();
        foreach (var pieSegmentModel in list2)
        {
            list1.Remove(pieSegmentModel);
        }
    }

    /// <summary>
    /// 生成模型
    /// </summary>
    /// <param name="dic">所有营养素</param>
    /// <param name="list">百分比饼图模型</param>
    /// <param name="energy">千焦 千卡</param>
    /// <param name="otherNutrient">其他营养素</param>
    /// <param name="proteinContent">三大营养素</param>
    public static void InitAllNutritional(Dictionary<string, decimal> dic,
        out List<PieSegmentModel> list,
        out List<Tuple<string, decimal>> energy,
        out List<NutrientContentModel> otherNutrient,
        out List<NutrientContentViewModel> proteinContent)
    {
        list = new List<PieSegmentModel>();
        energy = new List<Tuple<string, decimal>>();
        otherNutrient = new List<NutrientContentModel>();
        proteinContent = null;


        foreach (var (key, value) in dic)
        {
            //dic 单位为g或者ml
            if (key is "热量 kcal")
            {
                energy.Add(new(key, value));
                energy.Add(new("热量 kJ", UnitHelper.ConvertUnit(value, "kcal", "kJ")));
                continue;
            }

            if (!MainViewModel.TryGetNutritional(key, out var unit)) continue;
            if (key.Contains("脂肪酸") || key.Contains("饱和脂肪"))
            {
                UnitHelper.ConvertToClosestUnit(value, unit.Item1, out var value2, out var unit2);
                otherNutrient.Add(new NutrientContentModel
                {
                    Name = key,
                    Value = value2,
                    Unit = unit2,
                    Color = Brushes.Gainsboro
                });
                continue;
            }


            var item = new PieSegmentModel
            {
                Value = UnitHelper.ConvertToBaseUnit(value, unit.Item1),
                Item = key,
                Color = key switch
                {
                    "蛋白质" => ColorHelper.Blue,
                    "碳水化合物" => ColorHelper.Purple,
                    "脂肪" => ColorHelper.Orange,
                    _ => ColorHelper.RandomColor
                }
            };

            if (key is "蛋白质" or "碳水化合物" or "脂肪")
            {
                item.Id = 1;
            }
            else
            {
                UnitHelper.ConvertToClosestUnit(value, unit.Item1, out var value2, out var unit2);
                otherNutrient.Add(new NutrientContentModel
                {
                    Name = key,
                    Value = value2,
                    Unit = unit2,
                    Color = item.Color
                });
            }

            list.Add(item);
        }

        energy.Sort((l, r) => string.Compare(r.Item1, l.Item1, StringComparison.Ordinal));
        otherNutrient.Sort((l, r) =>
            UnitHelper.ConvertToBaseUnit(r.Value, r.Unit).CompareTo(UnitHelper.ConvertToBaseUnit(l.Value, l.Unit)));

        var f = list.Sum(w => w.Value);
        dic.TryGetValue("碳水化合物", out var a);
        dic.TryGetValue("蛋白质", out var b);
        dic.TryGetValue("脂肪", out var c);

        if (a == 0 && b == 0 && c == 0) return;
        UnitHelper.ConvertToClosestUnit(a, "g", out var a1, out var au);
        UnitHelper.ConvertToClosestUnit(b, "g", out var b1, out var bu);
        UnitHelper.ConvertToClosestUnit(c, "g", out var c1, out var cu);

        proteinContent = new List<NutrientContentViewModel>()
        {
            new()
            {
                Value = (double)(a / f * 100),
                Icon = XamlResourceHelper.Icon("IconCarbohydrate"),
                Str = $"碳水 {a1:0.##} {au}",
                GradientBrush = ColorHelper.Blue
            },
            new()
            {
                Value = (double)(b / f * 100),
                Icon = XamlResourceHelper.Icon("IconProtein"),
                Str = $"蛋白质 {b1:0.##} {bu}",
                GradientBrush = ColorHelper.Purple
            },
            new()
            {
                Value = (double)(c / f * 100),
                Icon = XamlResourceHelper.Icon("IconFat"),
                Str = $"脂肪 {c1:0.##} {cu}",
                GradientBrush = ColorHelper.Orange
            }
        };
    }
}