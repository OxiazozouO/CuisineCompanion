using CuisineCompanion.WebApi.Constant;

namespace CuisineCompanion.Models;

/***
 * 私有 公开为菜单 1 2
 * 食材 食谱 收藏夹 4 8 16
 * 食材 -> 私有
 * 食谱 -> 私有、公开为菜单
 * 菜单 -> 私有
 */

public static class DbFlagsHelper
{
    public static ModelFlags DeFavoriteFlags(this ModelFlags model)
    {
        if (model.Exists(ModelFlags.Private | ModelFlags.Public))
        {
            model -= model & ModelFlags.Public;
            model |= model & ModelFlags.Private;
        }

        if (model.Exists(ModelFlags.Ingredient)) return ModelFlags.Ingredient | ModelFlags.Private;

        if (model.Exists(ModelFlags.Menu)) return ModelFlags.Menu | ModelFlags.Public;

        if (model.Exists(ModelFlags.Recipe))
            //方便扩展
            return model;

        return ModelFlags.None;
    }

    public static bool TryDeFlags(byte flags, out sbyte authority, out sbyte idCategory)
    {
        authority = 0;
        idCategory = 0;
        var ret = (ModelFlags)flags;
        if (ret != ret.DeFavoriteFlags()) return false;

        if (ret.Exists(ModelFlags.Ingredient | ModelFlags.Private))
        {
            idCategory = IdCategoryConstant.Ingredient;
            authority = FavoriteAuthorityConstant.Private;
            return true;
        }

        if (ret.Exists(ModelFlags.Recipe | ModelFlags.Private))
        {
            idCategory = IdCategoryConstant.Recipe;
            authority = FavoriteAuthorityConstant.Private;
            return true;
        }

        if (ret.Exists(ModelFlags.Menu | ModelFlags.Public))
        {
            idCategory = IdCategoryConstant.Recipe;
            authority = FavoriteAuthorityConstant.Public;
            return true;
        }

        return false;
    }

    public static bool TryGetIdCategory(byte flags, out sbyte idCategory)
    {
        idCategory = 0;
        var ret = (ModelFlags)flags;

        switch (ret)
        {
            case ModelFlags.Ingredient:
                idCategory = IdCategoryConstant.Ingredient;
                return true;
            case ModelFlags.Recipe:
                idCategory = IdCategoryConstant.Recipe;
                return true;
            default:
                return false;
        }
    }

    public static byte GetFavoriteFlags(byte authority, byte idCategory)
    {
        TryGetFavoriteFlags(authority, idCategory, out var ret);
        return ret;
    }

    public static bool TryGetFavoriteFlags(byte authority, byte idCategory, out byte ret)
    {
        var flags = ModelFlags.None;

        if (idCategory == IdCategoryConstant.Ingredient)
        {
            flags = ModelFlags.Ingredient | ModelFlags.Private;
        }
        else if (idCategory == IdCategoryConstant.Recipe)
        {
            if (authority == FavoriteAuthorityConstant.Public)
                flags = ModelFlags.Menu | ModelFlags.Public;
            else if (authority == FavoriteAuthorityConstant.Private) flags = ModelFlags.Recipe | ModelFlags.Private;
        }

        ret = (byte)flags;
        return flags == flags.DeFavoriteFlags();
    }

    public static byte GetFlags(byte idCategory)
    {
        TryGetFlags(idCategory, out var ret);
        return ret;
    }

    public static bool TryGetFlags(byte idCategory, out byte ret)
    {
        ModelFlags flags;

        if (idCategory == IdCategoryConstant.Ingredient)
        {
            flags = ModelFlags.Ingredient;
        }
        else if (idCategory == IdCategoryConstant.Recipe)
        {
            flags = ModelFlags.Recipe;
        }
        else
        {
            ret = 0;
            return false;
        }

        ret = (byte)flags;
        return true;
    }
}