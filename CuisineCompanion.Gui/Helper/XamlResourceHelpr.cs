﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace CuisineCompanion.Helper;

public static class XamlResourceHelper
{
    private static readonly Dictionary<string, ResourceDictionary> _mp = new()
    {
        ["icon"] = new()
        {
            Source = new Uri(@"..\Resources\Icons.xaml", UriKind.Relative)
        },
        ["anime"] = new()
        {
            Source = new Uri(@"..\Resources\Anime.xaml", UriKind.Relative)
        },
        ["pattern"] = new()
        {
            Source = new Uri(@"..\Resources\Patterns.xaml", UriKind.Relative)
        }
    };

    public static ImageSource Icon(string w) => (ImageSource)_mp["icon"][w];

    public static Storyboard Anime(string w) => (Storyboard)_mp["anime"][w];

    public static Canvas Pattern(string w) => (Canvas)_mp["pattern"][w];

    public static void SetTransformGroup(this FrameworkElement fe)
    {
        if(fe.RenderTransform is TransformGroup) return;
        fe.RenderTransform = ((TransformGroup)_mp["anime"]["CenterTransform"]).Clone();
    }

    public static T Clone<T>(this T data) where T : UIElement =>
        (T)XamlReader.Parse(XamlWriter.Save(data));

    public static List<T> UnPackCanvas<T>(this Canvas data) where T : UIElement => 
        (from T child in data.Children select child.Clone()).ToList();
    
}