using System.ComponentModel.DataAnnotations;

namespace CuisineCompanion.WebApi.DTOs;

public record FavoriteBaseDto : AuthenticationDto
{
    [Required]
    [Range(1, 254, ErrorMessage = "请求错误")]
    public byte Flag { get; set; }
}

public record FavoriteListItemDto : FavoriteBaseDto
{
    [Required]
    [Range(1, int.MaxValue - 2, ErrorMessage = "请求错误")]
    public int FavoriteId { get; set; }
}

public record FavoriteRemoveDto : FavoriteBaseDto
{
    [Required]
    [Range(1, int.MaxValue - 2, ErrorMessage = "请求错误")]
    public int FavoriteId { get; set; }
}

public record FavoriteCreationDto : FavoriteBaseDto
{
    [StringLength(60, ErrorMessage = "上传错误")]
    public string FileUrl { get; set; }

    [Required(ErrorMessage = "收藏夹名称不能为空")]
    [StringLength(30, ErrorMessage = "简介不能超过{1}个字符")]
    public string FName { get; set; }

    [StringLength(198, ErrorMessage = "收藏夹简介不能超过{1}个字符")]
    public string Refer { get; set; }
}

public record FavoriteUpdateDto : FavoriteBaseDto
{
    [Required]
    [Range(1, int.MaxValue - 2, ErrorMessage = "请求错误")]
    public int FavoriteId { get; set; }

    [StringLength(60, ErrorMessage = "上传错误")]
    public string FileName { get; set; }

    [Required(ErrorMessage = "收藏夹名称不能为空")]
    [StringLength(100, ErrorMessage = "简介不能超过{1}个字符")]
    public string FName { get; set; }

    [StringLength(198, ErrorMessage = "收藏夹简介不能超过{1}个字符")]
    public string Refer { get; set; }
}

public record FavoriteItemCreationDto : FavoriteBaseDto
{
    [Required]
    [Range(1, int.MaxValue - 2, ErrorMessage = "请求错误")]
    public int FavoriteId { get; set; }

    [Required(ErrorMessage = "请求错误")]
    [Range(1, int.MaxValue - 2, ErrorMessage = "类型错误")]
    public int TId { get; set; }
}

public record FavoriteItemSeleteDto : FavoriteBaseDto
{
    [Required(ErrorMessage = "请求错误")]
    [Range(1, int.MaxValue - 2, ErrorMessage = "类型错误")]
    public int TId { get; set; }
}