using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CuisineCompanion.Common;

public class PiePanel : Canvas
{
    public static readonly DependencyProperty ItemsProperty =
        DependencyProperty.Register(nameof(Items), typeof(List<PieSegmentModel>), typeof(PiePanel),
            new PropertyMetadata(null, OnItemsPropertyChanged));

    static PiePanel()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(PiePanel), new FrameworkPropertyMetadata(typeof(PiePanel)));
    }

    public List<PieSegmentModel> Items
    {
        get => (List<PieSegmentModel>)GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    private static void OnItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PiePanel piePanel) piePanel.InitView();
    }


    private void InitView()
    {
        Children.Clear();
        if (Width == 0 || Height == 0 || Items is null || Items.Count == 0) return;
        var array = Items;

        array.Sort((a, b) => b.Value.CompareTo(a.Value));


        var ans = array.Sum(w => w.Value);
        if (ans == 0) return;
        double angleNum = 0;
        var r = Math.Min(Width, Height) / 2;
        foreach (var model in array)
        {
            var sector = new Sector
            {
                Center = new Point(r, r),
                Fill = model.Color,
                OuterRadius = r,
                InnerRadius = r * 0.6,
                StartAngle = angleNum,
                Angle = (double)(model.Value / ans) * 360,
                IsStroked = true
            };
            Children.Add(sector);
            angleNum += sector.Angle;
        }
    }
}

public partial class PieSegmentModel : ObservableObject
{
    [ObservableProperty] private Brush color;

    [ObservableProperty] private int id;

    [ObservableProperty] private object item;
    [ObservableProperty] private decimal value;
}