using System.ComponentModel.DataAnnotations;

namespace CuisineCompanion.WebApi.DTOs;

public record SearchDto
{
    public UserToken UserToken { get; set; }
    public byte Flag { get; set; }

    [MaxLength(100)] public string Text { get; set; }
}