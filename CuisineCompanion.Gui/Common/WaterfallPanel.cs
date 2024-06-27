using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CuisineCompanion.Common;

public class WaterfallPanel : Panel
{
    public int Columns
    {
        get => (int)GetValue(ColumnsProperty);
        set => SetValue(ColumnsProperty, value);
    }

    public static readonly DependencyProperty ColumnsProperty =
        DependencyProperty.Register(nameof(Columns), typeof(int), typeof(WaterfallPanel), new PropertyMetadata(3));

    public double Spacing
    {
        get => (double)GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }

    public static readonly DependencyProperty SpacingProperty =
        DependencyProperty.Register(nameof(Spacing), typeof(double), typeof(WaterfallPanel), new PropertyMetadata(5.0));

    static WaterfallPanel()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(WaterfallPanel),
            new FrameworkPropertyMetadata(typeof(WaterfallPanel)));
    }

    private List<double> columnHeights;

    protected override Size MeasureOverride(Size availableSize)
    {
        base.MeasureOverride(availableSize);
        // 创建面板期望大小并初始化为零
        var panelDesiredSize = new Size(0, 0);
        // 将列高度列表初始化为长度为 Columns 的 double 数组，并转换为列表
        columnHeights = new double[Columns].ToList();
        // 初始化当前 X 坐标
        double currentX = 0;
        // 计算每列的宽度
        var width = (availableSize.Width - (Columns - 1 * Spacing)) / Columns;
        // 遍历面板内的每个子元素
        for (int i = 0; i < InternalChildren.Count; i++)
        {
            // 如果当前子元素不是 FrameworkElement 类型，则继续下一个循环
            if (InternalChildren[i] is not FrameworkElement child) continue;
            // 测量当前子元素
            child.Measure(availableSize);
            // 设置子元素的宽度
            child.Width = width;

            int columnIndex = 0;
            //找最小的高度
            if (i / Columns == 0)
            {
                columnIndex = i % Columns;
            }
            else
            {
                for (var j = 0; j < Columns; j++)
                {
                    if (columnHeights[columnIndex] > columnHeights[j])
                    {
                        columnIndex = j;
                    }
                }
            }

            // 计算子元素的 X 坐标
            double x = (width + Spacing) * columnIndex;

            // 获取当前列的高度
            double y = columnHeights[columnIndex];
            // 如果不是第一行，则加上行间距
            if (i / Columns > 0)
                y += Spacing;


            // 创建子元素的尺寸
            var size = new Size(width, child.DesiredSize.Height);
            // 安排子元素在面板中的位置
            child.Arrange(new Rect(new Point(x, y), size));
            // 更新面板期望的宽度和高度
            panelDesiredSize.Width = Math.Max(panelDesiredSize.Width, x + child.DesiredSize.Width);
            panelDesiredSize.Height = Math.Max(panelDesiredSize.Height, y + child.DesiredSize.Height);

            // 更新当前列的高度
            columnHeights[columnIndex] += child.DesiredSize.Height + (i >= Columns ? Spacing : 0);
        }

        // 返回计算后的面板期望大小
        return panelDesiredSize;
    }
}