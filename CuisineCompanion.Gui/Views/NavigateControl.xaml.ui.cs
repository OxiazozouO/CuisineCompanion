using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Animation;
using CommunityToolkit.Mvvm.Input;
using CuisineCompanion.HttpClients;
using CuisineCompanion.Models;
using CuisineCompanion.ViewModels;

namespace CuisineCompanion.Views;

public partial class NavigateControl
{
    private readonly DoubleAnimation _height;


    private readonly Stack<(object, string, bool)> _lStack = new();
    private readonly Stack<(object, string, bool)> _rStack = new();
    private readonly Storyboard _storyboard;
    private readonly DoubleAnimation _width;

    private bool _isOnlyNavigate;

    public NavigateControl()
    {
        InitializeComponent();

        _storyboard = new Storyboard();

        // 创建宽度的 DoubleAnimation
        _width = new DoubleAnimation
        {
            Duration = TimeSpan.FromSeconds(0.2)
        };

        // 创建高度的 DoubleAnimation
        _height = new DoubleAnimation
        {
            Duration = TimeSpan.FromSeconds(0.2)
        };
        Run();
    }

    private void Run()
    {
        Storyboard.SetTargetProperty(_width, new PropertyPath("Width"));

        Storyboard.SetTargetProperty(_height, new PropertyPath("Height"));
        // 将动画添加到 Storyboard 中
        _storyboard.Children.Add(_width);
        _storyboard.Children.Add(_height);
        var switch1 = false;
        _storyboard.Completed += (_, _) =>
        {
            if (switch1)
            {
                Panel1.Visibility = Visibility.Collapsed;
                Grid1.Visibility = Visibility.Collapsed;
            }

            switch1 = !switch1;
        };

        Menu1.SelectionChanged += CloseMenu;
        Panel1.MouseDown += CloseMenu;
        Grid1.MouseDown += CloseMenu;
        MenuToggleButton.Click += OpenMenu;
    }

    private void OpenMenu(object sender, EventArgs e)
    {
        _width.From = 0;
        _width.To = Width * 0.3;
        _height.From = 0;
        _height.To = Height;

        Panel1.Visibility = Visibility.Visible;
        Grid1.Visibility = Visibility.Visible;
        Panel1.BeginStoryboard(_storyboard);
    }

    private void CloseMenu(object sender, EventArgs e)
    {
        _width.From = Width * 0.3;
        _width.To = 0;
        _height.From = Height;
        _height.To = 0;
        Panel1.BeginStoryboard(_storyboard);
    }

    public void ReNavigate(string text, object view, bool isOnlyNavigate = false)
    {
        _lStack.Clear();
        Navigate(text, view, isOnlyNavigate);
    }

    public void Navigate(string text, object view, bool isOnlyNavigate = false)
    {
        if (HomeContentControl.Content is not null)
        {
            if (_lStack.Count > 0)
            {
                var (a, _, _) = _lStack.Peek();
                if (a.GetType() == view.GetType())
                    _lStack.Pop();
            }

            _lStack.Push((HomeContentControl.Content, TitleTextBlock.Text, isOnlyNavigate));
        }

        (HomeContentControl.Content, TitleTextBlock.Text, _isOnlyNavigate) = (view, text, isOnlyNavigate);
        _rStack.Clear();
    }

    [RelayCommand]
    public void GoBack()
    {
        if (_lStack.Count <= 0) return;


        if (_isOnlyNavigate)
            _rStack.Clear();
        else
            _rStack.Push((HomeContentControl.Content, TitleTextBlock.Text, false));

        (HomeContentControl.Content, TitleTextBlock.Text, _isOnlyNavigate) = _lStack.Pop();
    }

    [RelayCommand]
    private void GoForward()
    {
        if (_rStack.Count <= 0) return;


        var (content, text, isOnlyNavigate) = _rStack.Pop();
        if (!_isOnlyNavigate)
        {
            _lStack.Push((HomeContentControl.Content, TitleTextBlock.Text, _isOnlyNavigate));
            (HomeContentControl.Content, TitleTextBlock.Text, _isOnlyNavigate) = (content, text, isOnlyNavigate);
        }
    }

    [RelayCommand]
    public void GoLogin() => MainViewModel.ReStart();

    [RelayCommand]
    public void GoHome()
    {
        GoHome(null, null);
    }
    
    public void GoHome(SearchFlags? flags, string? searchText)
    {
        flags ??= SearchFlags.Recipe;
        searchText ??= "";
        
        var home = HomeIndexViewModel._instance;
        _rStack.Clear();
        _lStack.Clear();
        MainViewModel.MainFrame.Navigate(MainViewModel.Navigate);

        var e = ApiService.GetEatingDiaries();

        home.EatingDiary = new EatingDiaryViewModel { EatingDiaries = e };
        home.SearchText = searchText;
        home.InitOption((SearchFlags)flags);
        ReNavigate($"你好，{MainViewModel.UserToken.UserName}！欢迎来到 食谱与健康管理平台！",
            new HomeIndexView
            {
                DataContext = home
            });
    }
}