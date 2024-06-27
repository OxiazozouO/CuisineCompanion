using System.ComponentModel.DataAnnotations;
using CuisineCompanion.Models;

namespace CuisineCompanion.WebApi.DTOs;


public record EatingDiaryAddDto : AuthenticationDto
{
    [Required]
    [Range(1, 254, ErrorMessage = "请求错误")]
    public byte Flag { get; set; }

    [Required] public Dictionary<int, decimal> Dosages { get; set; }

    [Required]
    [MinLength(1, ErrorMessage = "营养素列表不能为空")]
    public Dictionary<string, decimal> Nutrients { get; set; }

    [Required] public DateTime UpdateTime { get; set; }

    [Required] public int? TId { get; set; }
}


public record EatingDiaryDeleteDto : AuthenticationDto
{
    [Required]
    [Range(1, 254, ErrorMessage = "请求错误")]
    public byte Flag { get; set; }

    [Required]
    [Range(1, 254, ErrorMessage = "请求错误")]
    public int EdId { get; set; }
}

public record EatingDiaryInfoDTO
{
    [Required] public HashSet<KeyValuePair<byte, int>> IdFlags { get; set; }
}

public record EatingDiaryRetInfoDTO
{
    public ModelFlags Flag { get; set; }
    public int TId { get; set; }
    public string? Name { get; set; }
    public string? FileUrl { get; set; }
}