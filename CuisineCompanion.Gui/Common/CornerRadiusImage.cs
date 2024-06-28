using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CuisineCompanion.Common;

public class CornerRadiusImage : Image
{
    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius),
            typeof(CornerRadius),
            typeof(CornerRadiusImage),
            new PropertyMetadata(new CornerRadius(0)));

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }


    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
        ApplyCornerRadiusClip();
    }

// @formatter:off
    private void ApplyCornerRadiusClip()
    {
        if (CornerRadius == null) return;
        var radiusX = CornerRadius.TopLeft;
        var radiusY = CornerRadius.TopRight;
        var width   = ActualWidth  > 0 ? ActualWidth  : Width ;
        var height  = ActualHeight > 0 ? ActualHeight : Height;
        
        Clip = new RectangleGeometry(new Rect(0, 0, width, height), radiusX, radiusY);
    }
// @formatter:on
    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        base.OnRenderSizeChanged(sizeInfo);
        ApplyCornerRadiusClip();
    }
}