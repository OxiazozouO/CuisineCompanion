using static RestSharp.Method;

namespace CuisineCompanion.HttpClients;

public static class ApiEndpoints
{

    #region 账户

    public static ApiRequest AccountRegister(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "Account/Register",
            Parameters = parameters
        };
    }

    public static ApiRequest AccountLogin(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "Account/Login",
            Parameters = parameters
        };
    }

    public static ApiRequest AccountLogout(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "Account/Logout",
            Parameters = parameters
        };
    }

    public static ApiRequest AccountChangeInfo(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "Account/ChangeInfo",
            Parameters = parameters
        };
    }

    public static ApiRequest AccountChangePassword(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "Account/ChangePassword",
            Parameters = parameters
        };
    }

    public static ApiRequest AccountGetPhone(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "Account/GetPhone",
            Parameters = parameters
        };
    }

    #endregion
    

    public static ApiRequest GetConfigs(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "Config/GetConfigs",
            Parameters = parameters
        };
    }

    #region 收藏

    public static ApiRequest AddFavorite(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "Favorite/AddFavorite",
            Parameters = parameters
        };
    }

    public static ApiRequest RemoveFavorite(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "Favorite/RemoveFavorite",
            Parameters = parameters
        };
    }

    public static ApiRequest EditFavorite(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "Favorite/EditFavorite",
            Parameters = parameters
        };
    }

    public static ApiRequest UserFavorites(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "Favorite/UserFavorites",
            Parameters = parameters
        };
    }

    public static ApiRequest FavoriteListItems(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "Favorite/FavoriteListItems",
            Parameters = parameters
        };
    }

    public static ApiRequest AddFavoriteItem(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "FavoriteItem/AddFavoriteItem",
            Parameters = parameters
        };
    }


    public static ApiRequest IsLike(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "FavoriteItem/IsLike",
            Parameters = parameters
        };
    }

    public static ApiRequest RemoveFavoriteItems(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "FavoriteItem/RemoveFavoriteItems",
            Parameters = parameters
        };
    }

    public static ApiRequest RemoveFavoriteItem(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "FavoriteItem/RemoveFavoriteItem",
            Parameters = parameters
        };
    }

    #endregion

    public static ApiRequest IndexSearch(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "Index/Search",
            Parameters = parameters
        };
    }

    public static ApiRequest IngredientCategory(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "IngredientInfo/IngredientCategory",
            Parameters = parameters
        };
    }

    public static ApiRequest IngredientAt(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "IngredientInfo/IngredientAt",
            Parameters = parameters
        };
    }

    public static ApiRequest RecipeAt(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "RecipeInfo/RecipeAt",
            Parameters = parameters
        };
    }

    #region 用户信息

    public static ApiRequest MyAllInfo(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "UserInfo/MyAllInfo",
            Parameters = parameters
        };
    }

    public static ApiRequest AddMyInfo(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "UserInfo/AddMyInfo",
            Parameters = parameters
        };
    }

    public static ApiRequest UpdateMyInfo(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "UserInfo/UpdateMyInfo",
            Parameters = parameters
        };
    }

    public static ApiRequest DeleteMyInfo(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "UserInfo/DeleteMyInfo",
            Parameters = parameters
        };
    }

    #endregion

    #region 饮食日记

    public static ApiRequest AddEatingDiary(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "UserEatingDiary/AddEatingDiary",
            Parameters = parameters
        };
    }

    public static ApiRequest DeleteEatingDiary(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "UserEatingDiary/DeleteEatingDiary",
            Parameters = parameters
        };
    }

    public static ApiRequest GetEatingDiaries(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "UserEatingDiary/GetEatingDiaries",
            Parameters = parameters
        };
    }

    public static ApiRequest GetEatingDiaryInfos(object parameters)
    {
        return new ApiRequest
        {
            Method = POST,
            Url = "UserEatingDiary/GetEatingDiaryInfos",
            Parameters = parameters
        };
    }

    #endregion
}