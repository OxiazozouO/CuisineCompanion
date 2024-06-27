using System;
using System.Windows;
using System.Windows.Media.Animation;
using CuisineCompanion.Helper;
using Microsoft.Xaml.Behaviors;

namespace CuisineCompanion.Common.Behavior;

public class FocusAmplificationBehavior : Behavior<FrameworkElement>
{
    private static readonly Storyboard Amplification = XamlResourceHelper.Anime("Amplification");

    public double XFrom { get; set; }
    public double YFrom { get; set; }
    public double XTo { get; set; }
    public double YTo { get; set; }
    public double Duration { get; set; }

    public enum Mods
    {
        Weak,
        Natural,
        Ordinary,
        Exaggerate
    }

    public Mods Mod
    {
        set
        {
            switch (value)
            {
                case Mods.Weak:
                    XFrom = 1;
                    YFrom = 1;
                    XTo = 1.05;
                    YTo = 1.05;
                    Duration = 0.2;
                    break;
                case Mods.Natural:
                    XFrom = 1;
                    YFrom = 1;
                    XTo = 1.15;
                    YTo = 1.15;
                    Duration = 0.2;
                    break;
                case Mods.Ordinary:
                    XFrom = 1;
                    YFrom = 1;
                    XTo = 1.25;
                    YTo = 1.25;
                    Duration = 0.2;
                    break;
                case Mods.Exaggerate:
                    XFrom = 1;
                    YFrom = 1;
                    XTo = 1.85;
                    YTo = 1.85;
                    Duration = 0.2;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }
    }

    public FocusAmplificationBehavior()
    {
        Mod = Mods.Natural;
    }

    private readonly Storyboard _amplification = new Storyboard();
    private readonly Storyboard _amplificationBack = new Storyboard();


    protected override void OnAttached()
    {
        Create();
        AssociatedObject.MouseEnter += AssociatedObject_MouseEnter;
        AssociatedObject.MouseLeave += AssociatedObject_MouseLeave;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.MouseEnter -= AssociatedObject_MouseEnter;
        AssociatedObject.MouseLeave -= AssociatedObject_MouseLeave;
    }

    private void AssociatedObject_MouseEnter(object sender, EventArgs e)
    {
        AssociatedObject.BeginStoryboard(_amplification);
    }

    private void AssociatedObject_MouseLeave(object sender, EventArgs e)
    {
        AssociatedObject.BeginStoryboard(_amplificationBack);
    }

    private void Create()
    {
        AssociatedObject.SetTransformGroup();
        AssociatedObject.HorizontalAlignment = HorizontalAlignment.Center;
        AssociatedObject.VerticalAlignment = VerticalAlignment.Center;
        AssociatedObject.RenderTransformOrigin = new Point(0.5, 0.5);

        if (Amplification.Children[0] is DoubleAnimation dx)
        {
            var x = dx.Clone();
            var x2 = dx.Clone();
            x2.To = x.From = XFrom;
            x2.From = x.To = XTo;
            x2.Duration = x.Duration = TimeSpan.FromSeconds(Duration);
            _amplification.Children.Add(x);
            _amplificationBack.Children.Add(x2);
        }

        if (Amplification.Children[1] is DoubleAnimation dy)
        {
            var y = dy.Clone();
            var y2 = dy.Clone();
            y2.To = y.From = YFrom;
            y2.From = y.To = YTo;
            y2.Duration = y.Duration = TimeSpan.FromSeconds(Duration);
            _amplification.Children.Add(y);
            _amplificationBack.Children.Add(y2);
        }
    }
}