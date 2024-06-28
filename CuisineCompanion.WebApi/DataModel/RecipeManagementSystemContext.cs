using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CuisineCompanion.WebApi.DataModel;

public partial class RecipeManagementSystemContext : DbContext
{
    public RecipeManagementSystemContext()
    {
    }

    public RecipeManagementSystemContext(DbContextOptions<RecipeManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CategoryItem> CategoryItems { get; set; }

    public virtual DbSet<Cfg> Cfgs { get; set; }

    public virtual DbSet<EatingDiary> EatingDiaries { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<FavoriteItem> FavoriteItems { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<PreparationStep> PreparationSteps { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeItem> RecipeItems { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPhysicalInfo> UserPhysicalInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("category", tb => tb.HasComment("分类"));

            entity.HasIndex(e => e.CName, "category_pk").IsUnique();

            entity.Property(e => e.CategoryId)
                .HasComment("分类id")
                .HasColumnName("category_id");
            entity.Property(e => e.CName)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasComment("名称")
                .HasColumnName("c_name");
        });

        modelBuilder.Entity<CategoryItem>(entity =>
        {
            entity.HasKey(e => e.CategoryItemId).HasName("PRIMARY");

            entity.ToTable("category_item", tb => tb.HasComment("分类项目"));

            entity.HasIndex(e => e.CategoryId, "category_item_category_category_id_fk");

            entity.HasIndex(e => new { e.CategoryId, e.TId, e.IdCategory }, "category_item_pk").IsUnique();

            entity.Property(e => e.CategoryItemId)
                .HasComment("分类项目id")
                .HasColumnName("category_item_id");
            entity.Property(e => e.CategoryId)
                .HasComment("分类id")
                .HasColumnName("category_id");
            entity.Property(e => e.IdCategory)
                .HasComment("目标类型 1：食材 2：食谱 3: 食谱计划")
                .HasColumnName("id_category");
            entity.Property(e => e.TId)
                .HasComment("目标id")
                .HasColumnName("t_id");
        });

        modelBuilder.Entity<Cfg>(entity =>
        {
            entity.HasKey(e => e.CfgId).HasName("PRIMARY");

            entity.ToTable("cfg", tb => tb.HasComment("可能会经常更新的配置"));

            entity.HasIndex(e => e.CfgK, "cfg_pk").IsUnique();

            entity.Property(e => e.CfgId)
                .HasComment("id")
                .HasColumnName("cfg_id");
            entity.Property(e => e.CfgK)
                .HasMaxLength(30)
                .HasComment("键")
                .HasColumnName("cfg_k");
            entity.Property(e => e.CfgV)
                .HasComment("值")
                .HasColumnType("json")
                .HasColumnName("cfg_v");
        });

        modelBuilder.Entity<EatingDiary>(entity =>
        {
            entity.HasKey(e => e.EdId).HasName("PRIMARY");

            entity.ToTable("eating_diary", tb => tb.HasComment("饮食日记"));

            entity.HasIndex(e => e.UserId, "eating_diary_user_user_id_fk");

            entity.Property(e => e.EdId)
                .HasComment("id")
                .HasColumnName("ed_id");
            entity.Property(e => e.Dosages)
                .HasComment("用量列表,（包含id和用量）")
                .HasColumnType("json")
                .HasColumnName("dosages");
            entity.Property(e => e.IdCategory)
                .HasComment("目标类型 0：食材  1：食谱")
                .HasColumnName("id_category");
            entity.Property(e => e.NutrientContent)
                .HasComment("营养元素缓存")
                .HasColumnType("json")
                .HasColumnName("nutrient_content");
            entity.Property(e => e.Tid)
                .HasComment("目标id")
                .HasColumnName("tid");
            entity.Property(e => e.UpdateTime)
                .HasComment("记录的时间")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.UserId)
                .HasComment("用户id")
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.EatingDiaries)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("eating_diary_user_user_id_fk");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.FavoriteId).HasName("PRIMARY");

            entity.ToTable("favorite", tb => tb.HasComment("收藏"));

            entity.HasIndex(e => e.UserId, "favorite_user_user_id_fk");

            entity.Property(e => e.FavoriteId)
                .HasComment("收藏id")
                .HasColumnName("favorite_id");
            entity.Property(e => e.Authority)
                .HasDefaultValueSql("'1'")
                .HasComment("权限 1：私有收藏夹 2：公开菜单收藏夹")
                .HasColumnName("authority");
            entity.Property(e => e.CName)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasComment("名称")
                .HasColumnName("c_name");
            entity.Property(e => e.FileUri)
                .HasMaxLength(60)
                .IsFixedLength()
                .HasComment("多媒体文件")
                .HasColumnName("file_uri");
            entity.Property(e => e.IdCategory)
                .HasDefaultValueSql("'2'")
                .HasComment("收藏类型 1：食材 2：食谱 3：菜单")
                .HasColumnName("id_category");
            entity.Property(e => e.Refer)
                .HasMaxLength(200)
                .IsFixedLength()
                .HasComment("描述")
                .HasColumnName("refer");
            entity.Property(e => e.UserId)
                .HasComment("用户id")
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("favorite_user_user_id_fk");
        });

        modelBuilder.Entity<FavoriteItem>(entity =>
        {
            entity.HasKey(e => e.FavoriteItemId).HasName("PRIMARY");

            entity.ToTable("favorite_item", tb => tb.HasComment("收藏项目"));

            entity.HasIndex(e => new { e.FavoriteId, e.TId }, "favorite_item_pk").IsUnique();

            entity.Property(e => e.FavoriteItemId)
                .HasComment("收藏项目id")
                .HasColumnName("favorite_item_id");
            entity.Property(e => e.FavoriteId)
                .HasComment("收藏id")
                .HasColumnName("favorite_id");
            entity.Property(e => e.TId)
                .HasComment("目标id")
                .HasColumnName("t_id");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.IngredientId).HasName("PRIMARY");

            entity.ToTable("ingredient", tb => tb.HasComment("食材"));

            entity.HasIndex(e => e.UserId, "ingredient_user_user_id_fk");

            entity.Property(e => e.IngredientId)
                .HasComment("食材id")
                .HasColumnName("Ingredient_id");
            entity.Property(e => e.Allergy)
                .HasMaxLength(200)
                .IsFixedLength()
                .HasComment("过敏信息")
                .HasColumnName("allergy");
            entity.Property(e => e.Content)
                .HasDefaultValueSql("'1'")
                .HasComment("净含量")
                .HasColumnName("content");
            entity.Property(e => e.FileUri)
                .HasMaxLength(60)
                .IsFixedLength()
                .HasComment("多媒体文件")
                .HasColumnName("file_uri");
            entity.Property(e => e.IName)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasComment("名称")
                .HasColumnName("i_name");
            entity.Property(e => e.NutritionalComposition)
                .HasComment("营养成分 默认净含量100g来计算")
                .HasColumnType("json")
                .HasColumnName("nutritional_composition");
            entity.Property(e => e.Quantity)
                .HasComment("量")
                .HasColumnType("json")
                .HasColumnName("quantity");
            entity.Property(e => e.Refer)
                .HasMaxLength(200)
                .IsFixedLength()
                .HasComment("描述")
                .HasColumnName("refer");
            entity.Property(e => e.Unit)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasComment("单位")
                .HasColumnName("unit");
            entity.Property(e => e.UserId)
                .HasComment("创建用户id")
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Ingredients)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ingredient_user_user_id_fk");
        });

        modelBuilder.Entity<PreparationStep>(entity =>
        {
            entity.HasKey(e => e.PreparationStepId).HasName("PRIMARY");

            entity.ToTable("preparation_step", tb => tb.HasComment("制作步骤"));

            entity.HasIndex(e => e.RecipeId, "preparation_step_recipe_recipe_id_fk");

            entity.Property(e => e.PreparationStepId)
                .HasComment("id")
                .HasColumnName("preparation_step_id");
            entity.Property(e => e.FileUri)
                .HasMaxLength(60)
                .IsFixedLength()
                .HasComment("图片路径")
                .HasColumnName("file_uri");
            entity.Property(e => e.RecipeId)
                .HasComment("食谱id")
                .HasColumnName("recipe_id");
            entity.Property(e => e.Refer)
                .HasMaxLength(200)
                .IsFixedLength()
                .HasComment("描述")
                .HasColumnName("refer");
            entity.Property(e => e.RequiredIngredient)
                .HasComment("所需食材  食材1.食材2.食材3.....|+(+表示加上食材1.食材2.食材3.....).食材a.食材b....|食材制作时间的比例1.....(数量需跟第二个一致)")
                .HasColumnType("text")
                .HasColumnName("required_ingredient");
            entity.Property(e => e.RequiredTime)
                .HasComment("所需时间")
                .HasColumnType("time")
                .HasColumnName("required_time");
            entity.Property(e => e.SequenceNumber)
                .HasComment("顺序编号")
                .HasColumnName("sequence_number");
            entity.Property(e => e.Summary)
                .HasMaxLength(200)
                .IsFixedLength()
                .HasComment("总结、技巧说明")
                .HasColumnName("summary");
            entity.Property(e => e.Title)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasComment("标题")
                .HasColumnName("title");

            entity.HasOne(d => d.Recipe).WithMany(p => p.PreparationSteps)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("preparation_step_recipe_recipe_id_fk");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PRIMARY");

            entity.ToTable("recipe", tb => tb.HasComment("食谱"));

            entity.HasIndex(e => e.UserId, "recipe____fk");

            entity.Property(e => e.RecipeId)
                .HasComment("id")
                .HasColumnName("recipe_id");
            entity.Property(e => e.FileUri)
                .HasMaxLength(60)
                .IsFixedLength()
                .HasComment("多媒体文件")
                .HasColumnName("file_uri");
            entity.Property(e => e.RName)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasComment("名称")
                .HasColumnName("r_name");
            entity.Property(e => e.Summary)
                .HasMaxLength(200)
                .IsFixedLength()
                .HasComment("总结")
                .HasColumnName("summary");
            entity.Property(e => e.Title)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasComment("标题")
                .HasColumnName("title");
            entity.Property(e => e.UserId)
                .HasComment("创建用户id")
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe____fk");
        });

        modelBuilder.Entity<RecipeItem>(entity =>
        {
            entity.HasKey(e => e.RecipeItemId).HasName("PRIMARY");

            entity.ToTable("recipe_item", tb => tb.HasComment("食谱项目"));

            entity.HasIndex(e => e.IngredientId, "recipe_item_ingredient_Ingredient_id_fk");

            entity.HasIndex(e => e.RecipeId, "recipe_item_recipe_recipe_id_fk");

            entity.Property(e => e.RecipeItemId)
                .HasComment("食谱项目id")
                .HasColumnName("recipe_item_id");
            entity.Property(e => e.Dosage)
                .HasPrecision(10)
                .HasComment("用量")
                .HasColumnName("dosage");
            entity.Property(e => e.IngredientId)
                .HasComment("食材id")
                .HasColumnName("ingredient_id");
            entity.Property(e => e.RecipeId)
                .HasComment("食谱id")
                .HasColumnName("recipe_id");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.RecipeItems)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_item_ingredient_Ingredient_id_fk");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeItems)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_item_recipe_recipe_id_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user", tb => tb.HasComment("用户"));

            entity.HasIndex(e => e.UName, "user_pk").IsUnique();

            entity.Property(e => e.UserId)
                .HasComment("用户id")
                .HasColumnName("user_id");
            entity.Property(e => e.BirthDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("生日")
                .HasColumnType("datetime")
                .HasColumnName("birth_date");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasComment("邮箱")
                .HasColumnName("email");
            entity.Property(e => e.Gender)
                .HasComment("性别 1：男生、0：女生")
                .HasColumnName("gender");
            entity.Property(e => e.IsLogout)
                .HasComment("是否注销")
                .HasColumnName("is_logout");
            entity.Property(e => e.Password)
                .HasMaxLength(44)
                .IsFixedLength()
                .HasComment("密码")
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasComment("手机")
                .HasColumnName("phone");
            entity.Property(e => e.Salt)
                .HasMaxLength(49)
                .IsFixedLength()
                .HasComment("盐");
            entity.Property(e => e.UName)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasComment("名称")
                .HasColumnName("u_name");
        });

        modelBuilder.Entity<UserPhysicalInfo>(entity =>
        {
            entity.HasKey(e => e.UpiId).HasName("PRIMARY");

            entity.ToTable("user_physical_info", tb => tb.HasComment("用户身体数据"));

            entity.HasIndex(e => e.UId, "user_physical_info_user_user_id_fk");

            entity.Property(e => e.UpiId)
                .HasComment("id")
                .HasColumnName("upi_id");
            entity.Property(e => e.ActivityLevel)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasComment("运动习惯")
                .HasColumnName("activity_level");
            entity.Property(e => e.FatPercentage)
                .HasComment("脂肪供能占比")
                .HasColumnName("fat_percentage");
            entity.Property(e => e.Height)
                .HasDefaultValueSql("'120'")
                .HasComment("身高")
                .HasColumnName("height");
            entity.Property(e => e.ProteinRequirement)
                .HasComment("蛋白质摄入量")
                .HasColumnName("protein_requirement");
            entity.Property(e => e.UId)
                .HasComment("用户id")
                .HasColumnName("u_id");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime")
                .HasColumnName("update_time");
            entity.Property(e => e.Weight)
                .HasDefaultValueSql("'30'")
                .HasComment("体重(kg)")
                .HasColumnName("weight");

            entity.HasOne(d => d.UIdNavigation).WithMany(p => p.UserPhysicalInfos)
                .HasForeignKey(d => d.UId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_physical_info_user_user_id_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
