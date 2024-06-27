using System.ComponentModel.DataAnnotations;

namespace CuisineCompanion.WebApi.DTOs;

public record ConfigDto : AuthenticationDto
{
    [Required(ErrorMessage = "配置名必填")] public string Key { get; set; }
}