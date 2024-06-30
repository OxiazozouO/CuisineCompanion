using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CuisineCompanion.Helper;
using CuisineCompanion.HttpClients;
using CuisineCompanion.Models;

namespace CuisineCompanion.ViewModels;

public partial class EditMyInfoViewModel : ObservableObject
{
    [ObservableProperty] private ChangePasswordModel _changePasswordModel = new();
    [ObservableProperty] private EditMyInfoFlags flag;
    [ObservableProperty] private LogoutModel logoutModel = new();
    [ObservableProperty] private MyInfoModel myInfo;
    [ObservableProperty] private MyInfoModel oldInfo;

    public EditMyInfoViewModel()
    {
        OldInfo = MainViewModel.Instance.MyInfo;
        Flag = EditMyInfoFlags.Info;

        MyInfo = new MyInfoModel
        {
            UserId = OldInfo.UserId,
            UserName = OldInfo.UserName,
            Gender = OldInfo.Gender,
            BirthDate = OldInfo.BirthDate,
            UserInfoList = UpdateUserInfoList(OldInfo.UserInfoList)
        };
        MyInfo.UserNowInfo ??= new UserInfoModel();
        MyInfo.Init();
    }

    public ObservableCollection<UserInfoModel> UpdateUserInfoList(ObservableCollection<UserInfoModel> userInfoList)
    {
        if (userInfoList is null)
        {
            return null;
        }
        return new ObservableCollection<UserInfoModel>(userInfoList.Select(Clone));
    }

    [RelayCommand]
    private void Save()
    {
        var flag = 0;
        if (MsgBoxHelper.TryError(MyInfo.Error + MyInfo.UserNowInfo.Error)) return;

        if (OldInfo.UserId != MyInfo.UserId ||
            OldInfo.UserName != MyInfo.UserName ||
            OldInfo.Gender != MyInfo.Gender ||
            OldInfo.BirthDate != MyInfo.BirthDate)
        {
            var req = ApiEndpoints.AccountChangeInfo(new
            {
                MainViewModel.UserToken,
                Name = MyInfo.UserName,
                MyInfo.Gender,
                MyInfo.BirthDate
            });
            if (req.Execute(out _))
            {
                OldInfo.UserName = MyInfo.UserName;
                OldInfo.Gender = MyInfo.Gender;
                OldInfo.BirthDate = MyInfo.BirthDate;
                flag |= 1;
            }
            else
            {
                flag |= 2;
            }
        }


        var model = MyInfo.UserNowInfo;
        var old = OldInfo.UserInfoList
            .FirstOrDefault(x => x.UpiId == model.UpiId);
        if (old is null) goto add;

        if (old.UpdateTime != model.UpdateTime
            || (int)(old.Weight * 100) != (int)(model.Weight * 100)
            || (int)(old.Height * 100) != (int)(model.Height * 100)
            || old.ActivityLevelStr != model.ActivityLevelStr ||
            (int)(old.ProteinRequirement * 10000) != (int)(model.ProteinRequirement * 10000)
            || (int)(old.FatPercentage * 10000) != (int)(model.FatPercentage * 10000))
        {
            if (MsgBoxHelper.OkCancel("是否修改此历史身材数据"))
            {
                var req2 = ApiEndpoints.UpdateMyInfo(new
                {
                    MainViewModel.UserToken,
                    model.UpiId,
                    model.Weight,
                    model.Height,
                    ActivityLevel = model.ActivityLevelStr,
                    model.ProteinRequirement,
                    model.FatPercentage
                });
                if (req2.Execute(out var res))
                {
                    old.Weight = model.Weight;
                    old.Height = model.Height;
                    old.ActivityLevelStr = model.ActivityLevelStr;
                    old.ProteinRequirement = model.ProteinRequirement;
                    old.FatPercentage = model.FatPercentage;
                    flag |= 4;
                }
                else
                {
                    flag |= 8;
                }

                goto ret;
            }

            goto add;
        }

        goto ret;
        add:
        if (MsgBoxHelper.OkCancel("添加为新的身材数据？"))
        {
            var req3 = ApiEndpoints.AddMyInfo(new
            {
                MainViewModel.UserToken,
                model.Weight,
                model.Height,
                ActivityLevel = model.ActivityLevelStr,
                model.ProteinRequirement,
                model.FatPercentage
            });
            if (req3.Execute(out var res))
            {
                var tmp = res.Data.ToEntity<UserInfoModel>();
                var newModel = Clone(model);
                newModel.UpdateTime = tmp.UpdateTime;
                newModel.UpiId = tmp.UpiId;

                MyInfo.UserInfoList.Add(newModel);
                OldInfo.UserInfoList.Add(Clone(newModel));
                flag |= 16;
            }
            else
            {
                flag |= 32;
            }
        }

        ret:
        OldInfo.Init();
        var sb1 = new StringBuilder();
        var sb2 = new StringBuilder();
        if ((flag & 1) > 0)
            sb1.AppendLine("用户信息修改成功");
        else if ((flag & 2) > 0)
            sb2.AppendLine("用户信息修改失败");

        if ((flag & 4) > 0)
            sb1.AppendLine("身材数据修改成功");
        else if ((flag & 8) > 0)
            sb2.AppendLine("身材数据修改失败");

        if ((flag & 16) > 0)
            sb1.AppendLine("身材数据添加成功");
        else if ((flag & 32) > 0)
            sb2.AppendLine("身材数据添加失败");

        if (!MsgBoxHelper.TryError(sb2.ToString()) && !string.IsNullOrEmpty(sb1.ToString()))
            MsgBoxHelper.Info(sb1.ToString());
    }

    [RelayCommand]
    private void Delete()
    {
        if (MyInfo.UserInfoList is null || MyInfo.UserInfoList.Count == 0) return;
        if (MsgBoxHelper.OkCancel($"是否删除\"{MyInfo.UserNowInfo.UpdateTime:yyyy-MM-dd HH:mm:ss}\"时间的身材数据？"))
        {
            var req = ApiEndpoints.DeleteMyInfo(new
            {
                MainViewModel.UserToken,
                Id = MyInfo.UserNowInfo.UpiId
            });
            if (req.Execute(out _))
            {
                var old = OldInfo.UserInfoList
                    .FirstOrDefault(x => x.UpiId == MyInfo.UserNowInfo.UpiId);
                if (old is not null)
                    OldInfo.UserInfoList.Remove(old);
                MyInfo.UserInfoList.Remove(MyInfo.UserNowInfo);
                old = OldInfo.UserInfoList.Where(x => x.UpdateTime < DateTime.Now)
                    .MaxBy(x => x.UpdateTime);
                if (old is not null)
                    OldInfo.UserNowInfo = old;
                OldInfo.Init();
                MsgBoxHelper.Info("删除成功");
            }
            else
            {
                MsgBoxHelper.TryError("删除失败");
            }
        }
    }

    [RelayCommand]
    private void ToRNI()
    {
        MyInfo.UserNowInfo.ProteinPercentage = 0;
    }

    [RelayCommand]
    private void Logout()
    {
        if (!MsgBoxHelper.OkCancel("是否注销？ 此操作会使账户上所有数据资产不可用")) return;

        var req = ApiEndpoints.AccountLogout(new
        {
            MainViewModel.UserToken,
            LogoutModel.Password
        });
        if (req.Execute(out var res))
            MainViewModel.ReStart();
        else
            MsgBoxHelper.TryError("注销失败");
    }

    [RelayCommand]
    private void GoChange(EditMyInfoFlags f)
    {
        Flag = f;
        if (f == EditMyInfoFlags.Password)
        {
            var req = ApiEndpoints.AccountGetPhone(new { MainViewModel.UserToken });
            if (req.Execute(out var res) && res.Data is string s && !string.IsNullOrEmpty(s))
            {
                Flag = EditMyInfoFlags.Password;
                ChangePasswordModel.Password = "";
                ChangePasswordModel.NewPassword = "";
                ChangePasswordModel.PhoneString = s;
            }
            else
            {
                MsgBoxHelper.TryError("发生错误");
            }
        }
    }

    [RelayCommand]
    private void Back()
    {
        Flag = EditMyInfoFlags.Info;
    }

    [RelayCommand]
    private void ChangePassword()
    {
        if (MsgBoxHelper.TryError(ChangePasswordModel.Error)) return;

        var req = ApiEndpoints.AccountChangePassword(new
        {
            MainViewModel.UserToken,
            ChangePasswordModel.PhoneCut,
            ChangePasswordModel.Password,
            ChangePasswordModel.NewPassword
        });
        if (req.Execute(out var ret))
        {
            MsgBoxHelper.TryError("修改密码成功");
            Flag = EditMyInfoFlags.Info;
        }
        else
        {
            MsgBoxHelper.TryError(ret.Message);
        }
    }


    [RelayCommand]
    private static void SignOut()
    {
        if (!MsgBoxHelper.OkCancel("是否退出登录？")) return;
        MainViewModel.ReStart();
    }


    private static UserInfoModel Clone(UserInfoModel model)
    {
        return new UserInfoModel
        {
            UpiId = model.UpiId,
            Weight = model.Weight,
            Height = model.Height,
            ActivityLevelStr = model.ActivityLevelStr,
            UpdateTime = model.UpdateTime,
            FatPercentage = model.FatPercentage,
            CarbohydratePercentage = model.CarbohydratePercentage,
            ProteinRequirement = model.ProteinRequirement,
            ProteinPercentage = model.ProteinPercentage,
            CarbohydrateRequirement = model.CarbohydrateRequirement,
            FatRequirement = model.FatRequirement
        };
    }
}