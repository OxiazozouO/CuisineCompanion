using System.ComponentModel.DataAnnotations;

namespace WinFormsApp2.DatabaseModel;

public class DoctorDTO
{
    public int DoctorID { get; set; }

    [Required(ErrorMessage = "医生姓名是必填项")]
    [StringLength(50, ErrorMessage = "医生姓名长度不能超过{1}个字符")]
    public string DoctorName { get; set; }

    [Required(ErrorMessage = "专科是必填项")]
    [StringLength(100, ErrorMessage = "专科长度不能超过{1}个字符")]
    public string Specialty { get; set; }

    [StringLength(11, ErrorMessage = "联系电话长度不能超过{1}个字符")]
    public string ContactNumber { get; set; }

    [StringLength(100, ErrorMessage = "电子邮件长度不能超过{1}个字符")]
    [EmailAddress(ErrorMessage = "请输入有效的电子邮件地址")]
    public string Email { get; set; }

    [Required(ErrorMessage = "入职日期是必填项")]
    [DataType(DataType.Date)]
    public DateTime HireDate { get; set; } = DateTime.Now;

    [StringLength(50, ErrorMessage = "学位长度不能超过{1}个字符")]
    public string Degree { get; set; }
}