using System.ComponentModel.DataAnnotations;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CuisineCompanion.Helper;
using CuisineCompanion.HttpClients;
using CuisineCompanion.Views;

namespace CuisineCompanion.ViewModels;

public partial class LoginViewModel : ObservableValidator
{
    [Required(ErrorMessage = "用户名/邮箱/手机号不能为空")]
    [MinLength(1, ErrorMessage = "最小长度为 {1} 字符")]
    [MaxLength(20, ErrorMessage = "最大长度为 {1} 字符")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    private string identifier;

    [Required(ErrorMessage = "密码不能为空")]
    [MinLength(6, ErrorMessage = "密码长度不能小于{1}")]
    [MaxLength(20, ErrorMessage = "密码长度不能大于{1}")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    private string password;

    public LoginViewModel()
    {
        Identifier = "管理员小李";
        Password = "qwq1714050472";
    }


    [RelayCommand]
    private void Login()
    {
        if (MsgBoxHelper.TryError(Error)) return;

        var req = ApiEndpoints.AccountLogin(new { Identifier, Password });

        if (req.Execute(out var res))
        {
            MainViewModel.Init(res.Data);
            MainViewModel.Navigate.GoHome();
        }
        else
        {
            MsgBoxHelper.TryError(res.Message);
        }
    }

    public string Error
    {
        get
        {
            ValidateAllProperties();
            return string.Join('\n', GetErrors());
        }
    }
}