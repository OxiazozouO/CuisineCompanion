using System.ComponentModel.DataAnnotations;

namespace WinFormsApp2.DTO;

public class AppointmentDTO
{
    public int DID { get; set; }
    public int PID { get; set; }
    public int AID { get; set; }

    public string DoctorName { get; set; }

    public string Specialty { get; set; }

    public string DoctorPhone { get; set; }

    public string PatientName { get; set; }

    public string Gender { get; set; }

    public DateTime PatientBirthday { get; set; }

    public string PatientPhone { get; set; }

    [Required(ErrorMessage = "预约日期是必填项")]
    [DataType(DataType.Date)]
    public DateTime AppointmentDate { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "预约时间是必填项")]
    [DataType(DataType.Time)]
    public TimeSpan AppointmentTime { get; set; }

    [Required(ErrorMessage = "预约状态是必填项")]
    public int Status { get; set; }

    [StringLength(100, ErrorMessage = "备注长度不能超过{1}个字符")]
    public string Notes { get; set; }

    public static readonly Dictionary<string, int> StatusDict = new()
    {
        { "已预约", 1 },
        { "已取消", 2 },
        { "已完成", 3 },
    };

    public string StringStatus
    {
        get { return StatusDict.FirstOrDefault(e => e.Value == Status).Key; }
    }
}