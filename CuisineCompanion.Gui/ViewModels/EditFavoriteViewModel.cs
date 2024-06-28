using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CuisineCompanion.Helper;
using CuisineCompanion.Models;
using Microsoft.Win32;

namespace CuisineCompanion.ViewModels;

public partial class EditFavoriteViewModel : ObservableValidator
{
    [ObservableProperty] private ModelFlags authoritiesFlag;


    [ObservableProperty] private Visibility authorityVisibility = Visibility.Collapsed;

    [ObservableProperty] private ModelFlags flags;
    [ObservableProperty] private FavoriteModel fmodel;

    [ObservableProperty] private Visibility modelTypeVisibility = Visibility.Collapsed;

    [ObservableProperty] private FavoriteModel oldFavoriteModel;

    [ObservableProperty] private Func<ModelFlags, FavoriteModel, FavoriteModel, bool> saveFun;

    public EditFavoriteViewModel(FavoriteModel model)
    {
        Fmodel = new FavoriteModel
        {
            FName = model.FName,
            Refer = model.Refer,
            Flag = model.Flag,
            FileUri = model.FileUri,
            FavoriteId = model.FavoriteId,
            ItemsCount = model.ItemsCount
        };
        OldFavoriteModel = model;
    }

    public EditFavoriteViewModel()
    {
    }


    partial void OnFmodelChanged(FavoriteModel? oldValue, FavoriteModel newValue)
    {
        Flags = (ModelFlags)Fmodel.Flag;
        AuthorityVisibility = Flags.GetAuthority();
        AuthoritiesFlag = Flags & (ModelFlags.Private | ModelFlags.Public);
    }

    /***
     * 私有 公开为菜单 1 2
     * 食材 食谱 菜单 4 8 16
     * 食材 -> 私有
     * 食谱 -> 私有、公开为菜单
     * 菜单 -> 私有
     */
    partial void OnFlagsChanged(ModelFlags oldValue, ModelFlags newValue)
    {
        var f = Flags.CheckFlags();
        if (ModelTypeVisibility == Visibility.Visible)
            AuthorityVisibility = Flags.GetAuthority();
        Flags = f;
    }

    [RelayCommand]
    private void Save()
    {
        if (MsgBoxHelper.TryError(Fmodel.Error)) return;

        var f = (byte)Flags;
        f = (byte)(f - (f & (byte)ModelFlags.Public));
        f = (byte)(f - (f & (byte)ModelFlags.Private));
        var ff = (ModelFlags)f | AuthoritiesFlag;
        ff = ff.CheckFlags();

        SaveFun(ff, OldFavoriteModel, Fmodel);
    }

    [RelayCommand]
    private void SelectFile()
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "图片 |*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.webp"
        };
        if (openFileDialog.ShowDialog() == true)
        {
            if (!FileViewHelper.FileCheck(openFileDialog.FileName)) return;
            Fmodel.FileUri = openFileDialog.FileName;
        }
    }

    #region 辅助变量

    [ObservableProperty] private List<Tuple<string, ModelFlags>> authorities = new List<ModelFlags>
    {
        ModelFlags.Private,
        ModelFlags.Public
    }.Select(x => new Tuple<string, ModelFlags>(x.GetAuthorityName(), x)).ToList();

    [ObservableProperty] private List<Tuple<string, ModelFlags>> modelTypes = new List<ModelFlags>
    {
        ModelFlags.Ingredient,
        ModelFlags.Recipe,
        ModelFlags.Menu
    }.Select(x => new Tuple<string, ModelFlags>(x.GetName(), x)).ToList();

    #endregion
}