using System.ComponentModel.DataAnnotations;

namespace CuisineCompanion.WebApi.DTOs;

public record UserInfoUpdateDto : UserInfoAddDto
{
    [Required(ErrorMessage = "参数错误")]
    [Range(1, int.MaxValue - 2, ErrorMessage = "参数错误")]
    public int UpiId { get; set; }
}

public record UserInfoAddDto : AuthenticationDto
{
    [Required(ErrorMessage = "参数错误")]
    [Range(120, 255, ErrorMessage = "身高只能在{1}cm~{2}cm之间")]
    public double Height { get; set; }

    [Required(ErrorMessage = "参数错误")]
    [Range(30, 255, ErrorMessage = "体重只能在{1}kg~{2}kg之间")]
    public double Weight { get; set; }

    [Required(ErrorMessage = "参数错误")]
    [StringLength(20, ErrorMessage = "参数错误")]
    public string ActivityLevel { get; set; }

    [Required(ErrorMessage = "参数错误")]
    [Range(1, 255, ErrorMessage = "蛋白质摄入量必须在{1}%~{2}%之间")]
    public double ProteinRequirement { get; set; }

    [Required(ErrorMessage = "参数错误")]
    [Range(0.2, 0.3, ErrorMessage = "脂肪摄入量必须在{1}%~{2}%之间")]
    public double FatPercentage { get; set; }
}