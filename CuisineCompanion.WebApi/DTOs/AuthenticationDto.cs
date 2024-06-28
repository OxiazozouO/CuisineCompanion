using System.ComponentModel.DataAnnotations;

namespace CuisineCompanion.WebApi.DTOs;

public record AuthenticationDto
{
    [Required(ErrorMessage = "未登录")] public UserToken UserToken { get; set; }
}

public record UserSelectionDto : AuthenticationDto
{
    [Range(1, int.MaxValue - 2, ErrorMessage = "请求错误")]
    public int Id { get; set; }
}

public class RegisterDto
{
    [MinLength(1, ErrorMessage = "用户名长度不能小于{1}")]
    [MaxLength(20, ErrorMessage = "用户名长度不能大于{1}")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "密码不能为空")]
    [StringLength(44, MinimumLength = 44, ErrorMessage = "客户端错误，加密后密码长度为{1}")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "客户端错误")]
    [StringLength(49, MinimumLength = 49, ErrorMessage = "客户端错误，密码盐长度为{1}")]
    public string? Salt { get; set; }

    [EmailAddress(ErrorMessage = "邮箱格式错误")]
    public string? Email { get; set; }

    [StringLength(11, MinimumLength = 11, ErrorMessage = "手机号长度要等于{1}")]
    public string? Phone { get; set; }
}

public record LoginDto
{
    [Required(ErrorMessage = "用户名/邮箱/手机号不能为空")]
    [MinLength(1, ErrorMessage = "最小长度为 {1} 字符")]
    [MaxLength(20, ErrorMessage = "最大长度为 {1} 字符")]
    public string Identifier { get; set; }

    [Required(ErrorMessage = "密码不能为空")]
    [MinLength(6, ErrorMessage = "密码长度不能小于{1}")]
    [MaxLength(20, ErrorMessage = "密码长度不能大于{1}")]
    public string Password { get; set; }
}

public record LogoutDto : AuthenticationDto
{
    [Required(ErrorMessage = "密码不能为空")]
    [MinLength(6, ErrorMessage = "密码长度不能小于{1}")]
    [MaxLength(20, ErrorMessage = "密码长度不能大于{1}")]
    public string Password { get; set; }
}

public record UserChangeInfoDto : AuthenticationDto
{
    [Required(ErrorMessage = "请输入姓名")]
    [StringLength(20, ErrorMessage = "姓名长度不能超过{1}")]
    public string Name { get; set; }

    [Required(ErrorMessage = "请输入性别")] public bool Gender { get; set; }

    [Required(ErrorMessage = "请输入出生日期")] public DateTime BirthDate { get; set; }
}

public record UserChangePasswordDto : AuthenticationDto
{
    [Required(ErrorMessage = "密码不能为空")]
    [MinLength(6, ErrorMessage = "密码长度不能小于{1}")]
    [MaxLength(20, ErrorMessage = "密码长度不能大于{1}")]
    public string Password { get; set; }

    [Required(ErrorMessage = "密码不能为空")]
    [MinLength(6, ErrorMessage = "密码长度不能小于{1}")]
    [MaxLength(20, ErrorMessage = "密码长度不能大于{1}")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "必填项")]
    [Range(1000, 9999, ErrorMessage = "请输入4个数字")]
    public int PhoneCut { get; set; }
}