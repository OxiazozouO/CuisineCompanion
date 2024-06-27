using CuisineCompanion.WebApi.DataModel;
using CuisineCompanion.WebApi.DTOs;

namespace CuisineCompanion.WebApi.Service;

public static class UserService
{
    public static bool TryVerifyUserToken(RecipeManagementSystemContext db, UserToken token, out User? user,
        out ApiResponses? error)
    {
        error = null;
        user = db.Users.FirstOrDefault(u => u.UserId == token.UserId && u.IsLogout == false);
        if (user is not null) return true;
        
        error = new ApiResponses
        {
            Code = -1,
            Message = "用户不存在"
        };
        
        return false;
    }
}