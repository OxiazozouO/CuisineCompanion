using System.ComponentModel.DataAnnotations;
using WinFormsApp2.Helper;

namespace WinFormsApp2.DatabaseModel;

public class PatientDTO
{
    public int PatientID { get; set; }

    [Required(ErrorMessage = "患者姓名是必填项")]
    [StringLength(100, ErrorMessage = "患者姓名长度不能超过{1}个字符")]
    public string PatientName { get; set; }

    [StringLength(10, ErrorMessage = "性别长度不能超过{1}个字符")]
    public string Gender { get; set; }

    [DataType(DataType.Date)] public DateTime DateOfBirth { get; set; } = DateTimePicker.MinDateTime;

    [StringLength(11, MinimumLength = 11, ErrorMessage = "手机号长度要等于{1}")]
    [RegularExpression(RegexHelper.MobileExact, ErrorMessage = "手机号格式错误")]
    public string ContactNumber { get; set; }

    [StringLength(100, ErrorMessage = "电子邮件长度不能超过{1}个字符")]
    [EmailAddress(ErrorMessage = "请输入有效的电子邮件地址")]
    [RegularExpression(RegexHelper.Email, ErrorMessage = "邮箱格式错误")]
    public string Email { get; set; }

    [Required(ErrorMessage = "注册日期是必填项")]
    [DataType(DataType.Date)]
    public DateTime RegDate { get; set; } = DateTime.Now;
}