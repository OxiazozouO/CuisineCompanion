using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace CuisineCompanion.Common;

public class DynamicBoundsVerticalScrollListBox : ListBox
{
    public int Pro { get; set; } = 0;
    public int Nex { get; set; } = 1;

    public bool IsUpdate
    {
        get => (bool)GetValue(IsUpdateProperty);
        set => SetValue(IsUpdateProperty, value);
    }

    public static readonly DependencyProperty IsUpdateProperty =
        DependencyProperty.Register(nameof(IsUpdate), typeof(bool), typeof(DynamicBoundsVerticalScrollListBox),
            new PropertyMetadata(false, OnIsUpdatePropertyChanged));
    
    public int ProMaxCount
    {
        get => (int)GetValue(ProMaxCountProperty);
        set => SetValue(ProMaxCountProperty, value);
    }

    public static readonly DependencyProperty ProMaxCountProperty =
        DependencyProperty.Register(nameof(ProMaxCount), typeof(int), typeof(DynamicBoundsVerticalScrollListBox),
            new PropertyMetadata(100));
    
    public int NextMaxCount
    {
        get => (int)GetValue(NextMaxCountProperty);
        set => SetValue(NextMaxCountProperty, value);
    }

    public static readonly DependencyProperty NextMaxCountProperty =
        DependencyProperty.Register(nameof(NextMaxCount), typeof(int), typeof(DynamicBoundsVerticalScrollListBox),
            new PropertyMetadata(100));

    private static void OnIsUpdatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not DynamicBoundsVerticalScrollListBox box) return;
        box.Pro = 1;
        box.Nex = 1;
    }


    public DynamicBoundsVerticalScrollListBox()
    {
        AddHandler(ScrollViewer.ScrollChangedEvent, new ScrollChangedEventHandler(OnScrollChanged));
    }

    public Func<int, int> GetTop { get; set; } = i => i;

    public Func<int, int> GetBottom { get; set; } = i => i;

    private void OnScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        if (!IsUpdate) return;
        if (Items.Count == 0)
            return;

        if (e.OriginalSource is not ScrollViewer scroll)
            return;

        if (scroll.VerticalOffset == 0 && -Pro <= ProMaxCount)
        {
            object? pos = Items[0];
            Pro = GetTop(Pro);
            UpdateLayout();
            ScrollIntoView(pos);
        }
        else if (scroll.VerticalOffset + scroll.ViewportHeight >= scroll.ExtentHeight && Nex <= NextMaxCount)
        {
            Nex = GetBottom(Nex);
            UpdateLayout();
        }
    }
}