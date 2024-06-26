﻿using System;
using System.Collections.Generic;

namespace CuisineCompanion.WebApi.DataModel;

/// <summary>
/// 用户
/// </summary>
public partial class User
{
    /// <summary>
    /// 用户id
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string UName { get; set; } = null!;

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// 手机
    /// </summary>
    public string Phone { get; set; } = null!;

    /// <summary>
    /// 盐
    /// </summary>
    public string Salt { get; set; } = null!;

    /// <summary>
    /// 性别 1：男生、0：女生
    /// </summary>
    public bool Gender { get; set; }

    /// <summary>
    /// 生日
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// 是否注销
    /// </summary>
    public bool IsLogout { get; set; }

    public virtual ICollection<EatingDiary> EatingDiaries { get; set; } = new List<EatingDiary>();

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    public virtual ICollection<UserPhysicalInfo> UserPhysicalInfos { get; set; } = new List<UserPhysicalInfo>();
}
