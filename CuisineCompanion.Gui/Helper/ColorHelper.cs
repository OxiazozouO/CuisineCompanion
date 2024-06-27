using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace CuisineCompanion.Helper;

// @formatter:off
public static class ColorHelper
{
    
    static ColorHelper()
    {
        Blue      = Create("#47CBCF", "#63BCCC");
        Purple    = Create("#CEB4D7", "#E7B7C7");
        Orange    = Create("#FFBD89", "#ECBAB9");
        OrangeRed = Create("#fda085", "#ff9a9e");
        A         = Create("#7aff0c", "#c2ff0c");
        B         = Create("#c2ff0c", "#fff70c");
        C         = Create("#ffda0c", "#ffbe0c");
        D         = Create("#516BD7", "#6DAFD6");
        E         = Create("#84fab0", "#8fd3f4");
        F         = Create("#5ee7df", "#b490ca");
        G         = Create("#9890e3", "#b1f4cf");
        H         = Create("#a8e063", "#56ab2f");
        I         = Create("#2af598", "#009efd");
        J         = Create("#a1c4fd", "#c2e9fb");
        K         = Create("#48c6ef", "#6f86d6");
        L         = Create("#feada6", "#f5efef");
        N         = Create("#accbee", "#e7f0fd");
        O         = Create("#e9defa", "#fbfcdb");
        P         = Create("#c1dfc4", "#deecdd");
        Q         = Create("#0ba360", "#3cba92");
        S         = Create("#74ebd5", "#9face6");
        T         = Create("#6a85b6", "#bac8e0");
        U         = Create("#a3bded", "#6991c7");
        V         = Create("#9795f0", "#fbc8d4");
        W         = Create("#a7a6cb", "#8989ba");
        X         = Create("#d9afd9", "#97d9e1");
        Y         = Create("#93a5cf", "#e4efe9");
        Z         = Create("#92fe9d", "#00c9ff");
        M         = Create("#616161", "#5d4157");
        R         = Create("#8baaaa", "#ae8b9c");
        A1        = Create("#FFFEFF", "#D7FFFE");
        
        Mp = new List<LinearGradientBrush>
        {
            Blue, Purple, Orange, OrangeRed,
            A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z, A1,
            Create("#abecd6", "#fbed96"),
            Create("#ddd6f3", "#faaca8"),
            Create("#dcb0ed", "#99c99c"),
            Create("#f3e7e9", "#e3eeff"),
            Create("#96deda", "#50c9c3"),
            Create("#a8caba", "#5d4157"),
            Create("#00cdac", "#8ddad5"),
            Create("#f794a4", "#fdd6bd"),
            Create("#64b3f4", "#c2e59c"),
            Create("#0fd850", "#f9f047"),
            Create("#ee9ca7", "#ffdde1"),
            Create("#209cff", "#68e0cf"),
            Create("#bdc2e8", "#e6dee9"),
            Create("#e6b980", "#eacda3"),
            Create("#9be15d", "#00e3ae"),
            Create("#ed6ea0", "#ec8c69"),
            Create("#ffc3a0", "#ffafbd"),
            Create("#dfe9f3", "#ffffff"),
            Create("#50cc7f", "#f5d100"),
            Create("#0acffe", "#495aff"),
            Create("#616161", "#9bc5c3"),
            Create("#df89b5", "#bfd9fe"),
            Create("#ed6ea0", "#ec8c69"),
            Create("#c1c161", "#d4d4b1"),
            Create("#ec77ab", "#7873f5"),
            Create("#007adf", "#00ecbc"),
            Create("#20E2D7", "#F9FEA5"),
            Create("#B6CEE8", "#F578DC"),
            Create("#E3FDF5", "#FFE6FA"),
            Create("#7DE2FC", "#B9B6E5"),
            Create("#CBBACC", "#2580B3"),
            Create("#B7F8DB", "#50A7C2")
        };
    }

    public static LinearGradientBrush Create(Color l, Color r) =>
        new LinearGradientBrush()
        {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(1, 0),
            GradientStops = new GradientStopCollection { new(l, 0), new(r, 1) }
        };

    public static LinearGradientBrush Create(string l, string r) =>
        new LinearGradientBrush()
        {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(1, 0),
            GradientStops = new GradientStopCollection { new(l.ToColor(), 0), new(r.ToColor(), 1) }
        };

    private static readonly List<LinearGradientBrush> Mp;

    public static readonly LinearGradientBrush Blue,Purple,Orange,OrangeRed,
        A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z, A1;
// @formatter:on
    public static LinearGradientBrush RandomColor
    {
        get
        {
            int ind = new Random().Next(0, Mp.Count);
            return Mp[ind];
        }
    }

    private static Color ToColor(this string color)
    {
        return (Color)ColorConverter.ConvertFromString(color);
    }


    public static Brush GetColor(decimal value, decimal ear, decimal rni, decimal ul)
    {
        decimal a, b, c;
        a = b = c = 0;
        int flag = (ear > 0 ? 1 : 0) + (rni > 0 ? 2 : 0) + (ul > 0 ? 4 : 0);

        switch (flag)
        {
            case 0:
                return P;
            case 1:
                a = ear;
                b = ear * 1.2m;
                c = ear * 1.6m;
                break;
            case 2:
                a = rni * 0.8m;
                b = rni;
                c = rni * 1.4m;
                break;
            case 3:
                a = ear;
                b = rni;
                c = rni * 1.4m;
                break;
            case 4:
                a = ul * 0.4m;
                b = ul * 0.6m;
                c = ul;
                break;
            case 5:
                a = ear;
                b = Math.Min(ear * 1.2m, ul * 0.6m);
                c = ul;
                break;
            case 6:
                a = rni * 0.8m;
                b = rni;
                c = ul;
                break;
            case 7:
                a = ear;
                b = rni;
                c = ul;
                break;
        }

        if (value < a)
        {
            return Y;
        }

        if (value < b)
        {
            return A;
        }

        if (value < c)
        {
            decimal l = Math.Min(b * 1.2m, c * 0.6m);
            decimal r = Math.Max(b * 1.6m, c * 0.8m);
            if (value < l)
                return A;
            if (value < r)
                return C;
            return Q;
        }

        return OrangeRed;
    }
}