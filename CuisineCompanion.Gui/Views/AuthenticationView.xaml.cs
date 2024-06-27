using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using CuisineCompanion.Helper;
using CuisineCompanion.Resources;
using CuisineCompanion.ViewModels;

namespace CuisineCompanion.Views;

public partial class AuthenticationView
{
    public AuthenticationView()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        RunTip.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        PlayAnimation(AnimeGrid.Children, (int)ActualHeight);
        
        Regster r = null;
        if (Grid1.Children[0] is Regster r1) r = r1;
        else if (Grid1.Children[1] is Regster r2) r = r2;
        if(r is null) return;
        
        var viewModel = (RegisterViewModel)r.DataContext;
        
        viewModel.CommandCompleted += ViewModel_CommandCompleted;
    }

    private void ViewModel_CommandCompleted(object? sender, EventArgs e)
    {
        var rv = (RegisterViewModel)sender;
        RunTip.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        Login l = null;
        if (Grid1.Children[0] is Login l1) l = l1;
        else if (Grid1.Children[1] is Login l2) l = l2;
        if(l is null) return;
        if (l.DataContext is not LoginViewModel lv) return;
        lv.Identifier = rv.Email;
        lv.Password = rv.Password;
    }

    private bool _isLogin = false;

    public bool _anime = false;

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (_anime) return;
        _anime = true;
        var cardSlideOut = XamlResourceHelper.Anime("CardSlideOut");
        var cardZoomAndFadeIn = XamlResourceHelper.Anime("CardZoomAndFadeIn");

        FrameworkElement l = (FrameworkElement)Grid1.Children[0];
        FrameworkElement r = (FrameworkElement)Grid1.Children[1];
        //排后面去
        Grid1.Children.RemoveAt(0);
        Grid1.Children.Add(l);

        if (l is Login)
        {
            Title.Text = Strings.Str_Login;
            Tip.Text = Strings.Login_Instruction;
            RunTip.Content = Strings.Str_Login;
        }
        else
        {
            Title.Text = Strings.Str_Regster;
            Tip.Text = Strings.Regster_Instruction;
            RunTip.Content = Strings.Str_Regster;
        }

        cardSlideOut.Completed += (_, _) =>
        {
            _anime = false;
            l.Visibility = Visibility.Collapsed;
            r.Visibility = Visibility.Visible;
        };

        l.BeginStoryboard(cardSlideOut);
        r.BeginStoryboard(cardZoomAndFadeIn);
        r.Visibility = Visibility.Visible;
    }

    public static void PlayAnimation(UIElementCollection children, int height)
    {
        var s = XamlResourceHelper.Pattern("ShapeTemplate").UnPackCanvas<Shape>();
        Polygon p = (Polygon)s[0];
        Ellipse e = (Ellipse)s[1];
        var shapes = new List<Shape>();
        var random = new Random();

        //生成三角形
        int sum = 40;
        int ma = (int)(sum * 0.75) + random.Next(-(int)(sum * 0.1), (int)(sum * 0.1));

        for (int i = 0; i < ma; ++i)
        {
            double[] arr = new double[6];
            for (int j = 0; j < 6; j++)
            {
                arr[j] = random.Next(random.Next(-40, 20), random.Next(230, 270)) / 1d;
            }

            var x0 = Math.Max(Math.Max(arr[0], arr[2]), arr[4]) - Math.Min(Math.Min(arr[0], arr[2]), arr[4]);
            var y0 = Math.Max(Math.Max(arr[1], arr[3]), arr[5]) - Math.Min(Math.Min(arr[1], arr[3]), arr[5]);
            double area = Area(arr);

            //只保留长得匀称的
            if (double.IsNaN(x0) || double.IsNaN(y0) || double.IsNaN(area) || Math.Abs(x0 - y0) > 10 ||
                area < 390 || area / (x0 * y0) < 0.31415926)
            {
                i--;
                continue;
            }

            var tmp = p.Clone();
            tmp.Points = new PointCollection(new[]
            {
                new Point(arr[0], arr[1]),
                new Point(arr[2], arr[3]),
                new Point(arr[4], arr[5])
            });
            tmp.RenderTransformOrigin = new Point(0.5, 0.5);
            shapes.Add(tmp);
        }

        //生成大小不等的小圆形
        ma = sum - ma;
        for (int i = 0; i < ma; i++)
        {
            var tmp = e.Clone();
            tmp.Width = tmp.Height = random.Next(30, 180) / 3d;
            shapes.Add(tmp);
        }

        foreach (var shape in shapes)
        {
            children.Add(shape);
        }

        var anime = XamlResourceHelper.Anime("TransitionRotationMovement");

        var animations = new List<Storyboard>();
        foreach (var shape in shapes)
        {
            var tmpAnime = anime.Clone();
            var movement = anime.Children[0] as DoubleAnimation;
            var rotation = anime.Children[1] as DoubleAnimation;
            var transition = anime.Children[2] as DoubleAnimationUsingKeyFrames;
            double time = random.Next(2700, 8000) / 100.0d;
            rotation.From = random.Next(0, 30);
            rotation.To = random.Next(360, 500);

            //是否反相旋转
            if (random.Next(0, 2) == 0) rotation.To = -rotation.To;
            movement.Duration = new Duration(TimeSpan.FromSeconds(time));
            rotation.Duration = new Duration(TimeSpan.FromSeconds(time));
            transition.KeyFrames[1].KeyTime = TimeSpan.FromSeconds(3);
            transition.KeyFrames[2].KeyTime = TimeSpan.FromSeconds(time - 3);
            transition.KeyFrames[3].KeyTime = TimeSpan.FromSeconds(time);
            animations.Add(tmpAnime);

            Run();
            tmpAnime.Completed += (_, _) => { Run(); };
            continue;

            void Run()
            {
                shape.Margin = new Thickness(random.Next(-3000 * sum, 3000 * sum) / 100d,
                    random.Next(-90, (int)height - 90),
                    0, 0);
            }
        }

        for (var i = 0; i < shapes.Count; i++)
            shapes[i].BeginStoryboard(animations[i]);
    }


    /// <summary>
    /// 计算三角形面积
    /// </summary>
    /// <param name="arr">三角形顶点坐标</param>
    /// <returns>面积</returns>
    private static double Area(double[] arr)
    {
        double[][] ind =
        {
            new[] { arr[0] - arr[2], arr[1] - arr[3] },
            new[] { arr[0] - arr[4], arr[1] - arr[5] },
            new[] { arr[2] - arr[4], arr[3] - arr[5] },
        };

        double[] side = new double[3];
        for (int i = 0; i < ind.Length; i++)
        {
            side[i] = Math.Sqrt(ind[i][0] * ind[i][0] + ind[i][1] * ind[i][1]);
        }

        // 计算半周长
        double x = (side[0] + side[1] + side[2]) / 2;

        return Math.Sqrt(x * (x - side[0]) * (x - side[1]) * (x - side[2]));
    }
}