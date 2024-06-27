using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CuisineCompanion.Models;

/***
 * 私有 公开为菜单
 * 食材 食谱 收藏夹
 * 食材 -> 私有
 * 食谱 -> 私有、公开为菜单
 * 菜单 -> 私有为食谱
 */

public static class FavoriteFlagsViewHelper
{
    public static ModelFlags CheckFlags(this ModelFlags model)
    {
        if (model.Exists(ModelFlags.Private | ModelFlags.Public))
        {
            model -= (model & ModelFlags.Public);
            model |= (model & ModelFlags.Private);
        }

        if (model.Exists(ModelFlags.Ingredient))
        {
            return ModelFlags.Ingredient | ModelFlags.Private;
        }

        if (model.Exists(ModelFlags.Recipe | ModelFlags.Public))
        {
            return ModelFlags.Menu | ModelFlags.Public;
        }

        if (model.Exists(ModelFlags.Menu | ModelFlags.Private))
        {
            return ModelFlags.Recipe | ModelFlags.Private;
        }

        return model;
    }

    public static Visibility GetAuthority(this ModelFlags model)
    {
        model = CheckFlags(model);

        if (model.Exists(ModelFlags.Ingredient))
        {
            return Visibility.Collapsed;
        }

        if (model.Exists(ModelFlags.Menu))
        {
            return Visibility.Visible;
        }

        if (model.Exists(ModelFlags.Recipe))
        {
            return Visibility.Visible;
        }

        return Visibility.Collapsed;
    }
}