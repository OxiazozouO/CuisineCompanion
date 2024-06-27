using static RestSharp.Method;

namespace CuisineCompanion.HttpClients;

public static class ApiEndpoints
{
    public static ApiRequest AccountRegister(object parameters) => new()
    {
        Method = POST,
        Url = "Account/Register",
        Parameters = parameters
    };

    public static ApiRequest AccountLogin(object parameters) => new()
    {
        Method = POST,
        Url = "Account/Login",
        Parameters = parameters
    };

    public static ApiRequest AccountChangeInfo(object parameters) => new()
    {
        Method = POST,
        Url = "Account/ChangeInfo",
        Parameters = parameters
    };

    public static ApiRequest AccountLogout(object parameters) => new()
    {
        Method = POST,
        Url = "Account/Logout",
        Parameters = parameters
    };

    public static ApiRequest AccountGetPhone(object parameters) => new()
    {
        Method = POST,
        Url = "Account/GetPhone",
        Parameters = parameters
    };

    public static ApiRequest AccountChangePassword(object parameters) => new()
    {
        Method = POST,
        Url = "Account/ChangePassword",
        Parameters = parameters
    };

    #region Favorite

    public static ApiRequest AddFavoriteItems(object parameters) => new()
    {
        Method = POST,
        Url = "FavoriteItems/AddFavoriteItems",
        Parameters = parameters
    };


    public static ApiRequest IsLike(object parameters) => new()
    {
        Method = POST,
        Url = "FavoriteItems/IsLike",
        Parameters = parameters
    };

    public static ApiRequest RemoveFavoriteItems(object parameters) => new()
    {
        Method = POST,
        Url = "FavoriteItems/RemoveFavoriteItems",
        Parameters = parameters
    };

    public static ApiRequest RemoveFavoriteItem(object parameters) => new()
    {
        Method = POST,
        Url = "FavoriteItems/RemoveFavoriteItem",
        Parameters = parameters
    };


    public static ApiRequest AddFavorite(object parameters) => new()
    {
        Method = POST,
        Url = "Favorites/AddFavorite",
        Parameters = parameters
    };

    public static ApiRequest RemoveFavorite(object parameters) => new()
    {
        Method = POST,
        Url = "Favorites/RemoveFavorite",
        Parameters = parameters
    };

    public static ApiRequest EditFavorite(object parameters) => new()
    {
        Method = POST,
        Url = "Favorites/EditFavorite",
        Parameters = parameters
    };

    public static ApiRequest UserFavorites(object parameters) => new()
    {
        Method = POST,
        Url = "Favorites/UserFavorites",
        Parameters = parameters
    };

    public static ApiRequest FavoriteListItems(object parameters) => new()
    {
        Method = POST,
        Url = "Favorites/FavoriteListItems",
        Parameters = parameters
    };

    #endregion

    public static ApiRequest IndexSearch(object parameters) => new()
    {
        Method = POST,
        Url = "Index/Search",
        Parameters = parameters
    };

    public static ApiRequest IngredientCategory(object parameters) => new()
    {
        Method = POST,
        Url = "IngredientInfo/IngredientCategory",
        Parameters = parameters
    };

    public static ApiRequest IngredientAt(object parameters) => new()
    {
        Method = POST,
        Url = "IngredientInfo/IngredientAt",
        Parameters = parameters
    };

    public static ApiRequest RecipeAt(object parameters) => new()
    {
        Method = POST,
        Url = "RecipeInfo/RecipeAt",
        Parameters = parameters
    };


    public static ApiRequest GetConfigs(object parameters) => new()
    {
        Method = POST,
        Url = "Config/GetConfigs",
        Parameters = parameters
    };

    public static ApiRequest MyAllInfo(object parameters) => new()
    {
        Method = POST,
        Url = "MyInfo/MyAllInfo",
        Parameters = parameters
    };

    public static ApiRequest AddInfo(object parameters) => new()
    {
        Method = POST,
        Url = "MyInfo/AddInfo",
        Parameters = parameters
    };

    public static ApiRequest UpdateInfo(object parameters) => new()
    {
        Method = POST,
        Url = "MyInfo/UpdateInfo",
        Parameters = parameters
    };

    public static ApiRequest DeleteInfo(object parameters) => new()
    {
        Method = POST,
        Url = "MyInfo/DeleteInfo",
        Parameters = parameters
    };

    public static ApiRequest AddEatingDiary(object parameters) => new()
    {
        Method = POST,
        Url = "MyEatingDiary/AddEatingDiary",
        Parameters = parameters
    };

    public static ApiRequest GetEatingDiaries(object parameters) => new()
    {
        Method = POST,
        Url = "MyEatingDiary/GetEatingDiaries",
        Parameters = parameters
    };

    public static ApiRequest GetGetEatingEatingDiaryInfos(object parameters) => new()
    {
        Method = POST,
        Url = "MyEatingDiary/GetEatingEatingDiaryInfos",
        Parameters = parameters
    }; 
    
    public static ApiRequest DeleteEatingDiary(object parameters) => new()
    {
        Method = POST,
        Url = "MyEatingDiary/DeleteEatingDiary",
        Parameters = parameters
    };
}