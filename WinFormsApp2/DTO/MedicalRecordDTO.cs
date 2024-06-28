using System.ComponentModel.DataAnnotations;

namespace WinFormsApp2.DTO;

public class MedicalRecordDTO
{
    public int MID { get; set; }

    public int DID { get; set; }

    public int PID { get; set; }

    public string DoctorName { get; set; }

    public string Specialty { get; set; }

    public string DoctorPhone { get; set; }

    public string PatientName { get; set; }

    public string Gender { get; set; }

    public DateTime PatientBirthday { get; set; }

    public string PatientPhone { get; set; }

    [Required(ErrorMessage = "诊断是必填项")] public string Diagnosis { get; set; }

    [Required(ErrorMessage = "治疗方案是必填项")] public string Treatment { get; set; }

    [Required(ErrorMessage = "记录日期是必填项")]
    [DataType(DataType.Date)]
    public DateTime RecordDate { get; set; } = DateTime.Now;

    [StringLength(100, ErrorMessage = "随访情况长度不能超过{1}个字符")]
    public string FollowUp { get; set; }
}