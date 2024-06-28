using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CuisineCompanion.Models;

public partial class FavoriteModel : ObservableValidator
{
    [Required(ErrorMessage = "收藏夹名称不能为空")]
    [StringLength(30, ErrorMessage = "简介不能超过{1}个字符")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    private string fName;

    [ObservableProperty] private int favoriteId;

    [ObservableProperty] private string fileUri;

    [Required] [Range(1, 254, ErrorMessage = "参数错误")] [NotifyDataErrorInfo] [ObservableProperty]
    private byte flag;

    [ObservableProperty] private int itemsCount;

    [StringLength(198, ErrorMessage = "收藏夹简介不能超过{1}个字符")] [NotifyDataErrorInfo] [ObservableProperty]
    private string refer;

    public string Error
    {
        get
        {
            ValidateAllProperties();
            return string.Join('\n', GetErrors());
        }
    }
}