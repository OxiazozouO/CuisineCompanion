using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CuisineCompanion.Helper;
using CuisineCompanion.HttpClients;
using CuisineCompanion.Models;
using CuisineCompanion.Views;

namespace CuisineCompanion.ViewModels;

public partial class FavoriteViewModel : ObservableObject
{
    [ObservableProperty] private ILike data;
    [ObservableProperty] private MyFavoritesViewModel myFavorites;


    private bool tag;

    [RelayCommand]
    private void AddFavorite(FavoriteAtModel at)
    {
        tag = true;
        var view = new EditFavoriteView
        {
            DataContext = new EditFavoriteViewModel(new FavoriteModel { Flag = at.Flag })
            {
                SaveFun = Add
            }
        };

        MainViewModel.Navigate.Navigate("添加收藏夹", view, true);
    }

    private bool Add(ModelFlags f, FavoriteModel _, FavoriteModel model)
    {
        if (!HttpRestClient.FileUpload(model.FileUri, out var fileName)) return false;
        var req = ApiEndpoints.AddFavorite(new
        {
            MainViewModel.UserToken,
            Flag = f,
            FileUrl = fileName,
            model.CName,
            model.Refer
        });

        if (req.Execute(out var ret) && ret.Code == 1)
        {
            var obj = ret.Data.ToEntity<FavoriteModel>();
            model.Flag = (byte)f;
            model.FileUri = obj.FileUri;
            model.FavoriteId = obj.FavoriteId;

            MyFavorites.Favorite.Add(model);

            MsgBoxHelper.Info("添加成功");
            MainViewModel.Navigate.GoBack();
            return true;
        }

        MsgBoxHelper.TryError(ret.Message);
        return false;
    }

    [RelayCommand]
    private void EditFavorite(FavoriteModel model)
    {
        tag = true;
        var view = new EditFavoriteView
        {
            DataContext = new EditFavoriteViewModel(model) { SaveFun = Edit }
        };
        MainViewModel.Navigate.Navigate("编辑收藏夹", view, true);
    }

    private bool Edit(ModelFlags f, FavoriteModel oldModel, FavoriteModel model)
    {
        var filePath = model.FileUri;
        var fileName = "";
        if (!filePath.Contains("http") && !HttpRestClient.FileUpload(filePath, out fileName)) return false;

        model.FavoriteId = model.FavoriteId;

        var req = ApiEndpoints.EditFavorite(new
        {
            MainViewModel.UserToken,
            Flag = (byte)f,
            model.FavoriteId,
            FileName = fileName,
            model.CName,
            model.Refer
        });

        if (req.Execute(out var ret))
        {
            MyFavorites.Favorite.Remove(oldModel);

            oldModel.CName = model.CName;
            oldModel.Refer = model.Refer;
            oldModel.Flag = (byte)f;
            if (ret.Data is not null)
                oldModel.FileUri = ret.Data.ToString();
            oldModel.FileUri = model.FileUri;
            oldModel.FavoriteId = model.FavoriteId;
            oldModel.ItemsCount = model.ItemsCount;

            MyFavorites.Favorite.Add(oldModel);


            MsgBoxHelper.Info("修改成功");
            MainViewModel.Navigate.GoBack();

            return true;
        }

        MsgBoxHelper.TryError(ret.Message);
        return false;
    }


    [RelayCommand]
    private void RemoveFavorite(FavoriteModel model)
    {
        tag = true;
        if (MsgBoxHelper.OkCancel($"是否删除\"{model.CName}\"收藏夹？ 此操作将删除该收藏夹下的所有{model.ItemsCount}个收藏。"))
        {
            var req = ApiEndpoints.RemoveFavorite(new
            {
                MainViewModel.UserToken,
                model.Flag,
                model.FavoriteId
            });
            if (req.Execute(out var ret))
                MyFavorites.Favorite.Remove(model);
            else
                MsgBoxHelper.TryError(ret.Message);
        }
    }

    [RelayCommand]
    private void SelectedFavorite(FavoriteModel model)
    {
        if (tag)
        {
            tag = false;
            return;
        }

        var flag = (ModelFlags)model.Flag;

        var tid = -1;
        var name = "";
        if (flag.Exists(ModelFlags.Ingredient) && Data is IngredientViewModel i)
        {
            (tid, name) = (i.IngredientModel.IngredientId, i.IngredientModel.IName);
        }
        else if ((flag.Exists(ModelFlags.Recipe) || flag.Exists(ModelFlags.Menu)) && Data is RecipeModel r)
        {
            (tid, name) = (r.RecipeId, r.RName);
        }
        else if (Data is null)
        {
            OpenView(model);
            return;
        }
        else
        {
            return;
        }

        AddTo(model, name, tid);
    }

    private void AddTo(FavoriteModel model, string name, int tid)
    {
        if (!MsgBoxHelper.OkCancel($"是否把\"{name}\"加入\"{model.CName}\"收藏夹？")) return;

        var req = ApiEndpoints.AddFavoriteItem(new
        {
            MainViewModel.UserToken,
            model.Flag,
            model.FavoriteId,
            TId = tid
        });

        if (req.Execute(out var ret))
        {
            Data.Like();
            ++model.ItemsCount;
            MsgBoxHelper.Info(ret.Message);
        }
        else
        {
            MsgBoxHelper.TryError(ret.Message);
        }
    }

    private void OpenView(FavoriteModel model)
    {
        MainViewModel.Navigate.Navigate($"{model.CName} 收藏夹详情",
            new FavoriteItemView
            {
                DataContext = new FavoriteItemViewModel
                {
                    Root = model
                }
            }, true);
    }
}