using System;

namespace CuisineCompanion.Models;

[Flags]
public enum ModelFlags : byte
{
    /// <summary>
    ///     未初始化
    /// </summary>
    None = 0,

    /// <summary>
    ///     私有
    /// </summary>
    Private = 1 << 0, // 1

    /// <summary>
    ///     公开为菜单
    /// </summary>
    Public = 1 << 1, // 2

    /// <summary>
    ///     食材
    /// </summary>
    Ingredient = 1 << 2, // 4

    /// <summary>
    ///     食谱
    /// </summary>
    Recipe = 1 << 3, // 8

    /// <summary>
    ///     菜单
    /// </summary>
    Menu = 1 << 4, // 16

    All = Ingredient | Recipe | Menu
}

public static class FavoriteFlagsHelper
{
    public static bool Exists(this ModelFlags model, ModelFlags flags)
    {
        return (model & flags) == flags;
    }

    public static bool Exists(this ModelFlags model, byte flags)
    {
        return model.Exists((ModelFlags)flags);
    }

    public static string GetName(byte model)
    {
        var ret = (ModelFlags)model;
        return GetName(ret);
    }

    public static string GetName(this ModelFlags model)
    {
        return model.Exists(ModelFlags.Ingredient)
            ? "食材"
            : model.Exists(ModelFlags.Recipe)
                ? "食谱"
                : model.Exists(ModelFlags.Menu)
                    ? "菜单"
                    : "";
    }

    public static string GetAuthorityName(this ModelFlags model)
    {
        return model.Exists(ModelFlags.Private)
            ? "私有"
            : model.Exists(ModelFlags.Public)
                ? "公开为菜单"
                : "";
    }
}