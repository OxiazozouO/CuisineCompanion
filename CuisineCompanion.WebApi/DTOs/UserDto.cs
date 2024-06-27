using System.ComponentModel.DataAnnotations;
using CuisineCompanion.Extensions;

namespace CuisineCompanion.WebApi.DTOs;

public class UserDto
{
    [Required(ErrorMessage = "用户id不能为空")]
    [MaxLength(int.MaxValue, ErrorMessage = "系统错误")]
    public int UserId { get; set; }

    [MinLength(1, ErrorMessage = "用户名长度不能小于{1}")]
    [MaxLength(20, ErrorMessage = "用户名长度不能大于{1}")]
    public string? Name { get; set; }

    [EmailAddress(ErrorMessage = "邮箱格式错误")]
    [RegularExpression(RegexHelper.Email, ErrorMessage = "邮箱格式错误")]
    public string? Email { get; set; }

    [StringLength(11, MinimumLength = 11, ErrorMessage = "手机号长度要等于{1}")]
    [RegularExpression(RegexHelper.MobileExact, ErrorMessage = "手机号格式错误")]
    public string? Phone { get; set; }
}