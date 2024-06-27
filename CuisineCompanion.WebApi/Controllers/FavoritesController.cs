using CuisineCompanion.Models;
using CuisineCompanion.WebApi.Constant;
using CuisineCompanion.WebApi.DataModel;
using CuisineCompanion.WebApi.DTOs;
using CuisineCompanion.WebApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace CuisineCompanion.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class FavoritesController : MyControllerBase
{
    public FavoritesController(RecipeManagementSystemContext db) : base(db)
    {
    }

    #region Add

    [HttpPost]
    public IActionResult AddFavorite(FavoriteCreationDto dto)
    {
        if (!DbFlagsHelper.TryDeFlags(dto.Flag, out sbyte authority, out sbyte idCategory))
        {
            return Ok(new ApiResponses { Code = -1, Message = "参数错误" });
        }

        int userid = dto.UserToken.UserId;

        try
        {
            int count = _db.Favorites.Count(f => f.UserId == userid && f.IdCategory == idCategory);
            if (count >= 200)
            {
                return Ok(new ApiResponses
                {
                    Code = -1,
                    Message = $"{IdCategoryConstant.GetName(idCategory)}收藏夹数量已满"
                });
            }

            Favorite? favorite =
                _db.Favorites.FirstOrDefault(f => f.UserId == userid && f.CName == dto.CName);
            if (favorite is not null)
            {
                return Ok(new ApiResponses { Code = -1, Message = "此收藏夹已存在" });
            }

            favorite = new Favorite
            {
                UserId = userid,
                CName = dto.CName,
                Refer = dto.Refer,
                Authority = authority,
                IdCategory = idCategory
            };

            if (FileUrlHelper.TryMove(dto.FileUrl, FileUrlHelper.Favorites))
            {
                favorite.FileUri = dto.FileUrl;
            }
            else
            {
                goto bad;
            }

            _db.Favorites.Add(favorite);
            if (_db.SaveChanges() == 1)
            {
                return Ok(new ApiResponses
                {
                    Code = 1, Message = "添加收藏夹成功",
                    Data = new
                    {
                        favorite.FavoriteId,
                        FileUri = Url.GetFavoriteUrl(Request, favorite.FileUri)
                    }
                });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok(ApiResponses.ErrorResult);
        }

        bad:
        return Ok(new ApiResponses
        {
            Code = -1, Message = "添加失败"
        });
    }

    #endregion

    #region Remove

    [HttpPost]
    public IActionResult RemoveFavorite(FavoriteRemoveDto favoriteDto)
    {
        if (!DbFlagsHelper.TryDeFlags(favoriteDto.Flag, out sbyte authority, out sbyte idCategory))
        {
            return Ok(new ApiResponses { Code = -1, Message = "参数错误" });
        }

        int userid = favoriteDto.UserToken.UserId;

        try
        {
            var favorite = _db.Favorites.FirstOrDefault(f =>
                f.FavoriteId == favoriteDto.FavoriteId &&
                f.UserId == userid &&
                f.Authority == authority &&
                f.IdCategory == idCategory
            );
            if (favorite is null)
            {
                return Ok(new ApiResponses { Code = -1, Message = "收藏夹不存在" });
            }

            _db.FavoriteItems.RemoveRange(_db.FavoriteItems.Where(fi => fi.FavoriteId == favorite.FavoriteId));
            _db.Favorites.Remove(favorite);
            _db.SaveChanges();
            return Ok(new ApiResponses { Code = 1, Message = "删除收藏夹成功" });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok(ApiResponses.ErrorResult);
        }
    }

    #endregion

    #region Update

    [HttpPost]
    public IActionResult EditFavorite(FavoriteUpdateDto dto)
    {
        if (!DbFlagsHelper.TryDeFlags(dto.Flag, out sbyte authority, out sbyte idCategory))
        {
            return Ok(new ApiResponses { Code = -1, Message = "参数错误" });
        }
        
        int userid = dto.UserToken.UserId;

        try
        {
            var favorite = _db.Favorites.FirstOrDefault(f => f.FavoriteId == dto.FavoriteId && f.UserId == userid);
            if (favorite is null)
            {
                return Ok(new ApiResponses { Code = -1, Message = "收藏夹不存在" });
            }

            if (dto.FileName != "")
            {
                if (FileUrlHelper.TryMove(dto.FileName,
                        FileUrlHelper.Temps,
                        favorite.FileUri,
                        FileUrlHelper.Favorites,
                        FileUrlHelper.DeletedFavorites)
                   )
                {
                    favorite.FileUri = dto.FileName;
                }
                else
                {
                    goto ret;
                }
            }

            favorite.CName = dto.CName;
            favorite.Refer = dto.Refer;
            favorite.Authority = authority;
            favorite.IdCategory = idCategory;

            _db.Favorites.Update(favorite);

            if (_db.SaveChanges() == 1)
            {
                return Ok(new ApiResponses
                {
                    Code = 1,
                    Message = "更改成功",
                    Data = dto.FileName == "" ? null : Url.GetFavoriteUrl(Request, favorite.FileUri)
                });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok(ApiResponses.ErrorResult);
        }

        ret:
        return Ok(new ApiResponses
        {
            Code = -1, Message = "更改失败"
        });
    }
    
    #endregion

    #region Select

    [HttpPost]
    public IActionResult UserFavorites(UserToken dto)
    {
        try
        {
            if (!UserService.TryVerifyUserToken(_db, dto, out _, out var error))
            {
                return Ok(error);
            }
            
            var ret = _db.Favorites
                .Where(f => f.UserId == dto.UserId)
                .Select(f => new
                {
                    f.FavoriteId,
                    f.CName,
                    f.Refer,
                    FileUri = Url.GetFavoriteUrl(Request, f.FileUri),
                    Flag = DbFlagsHelper.GetFavoriteFlags((byte)f.Authority, (byte)f.IdCategory),
                    itemsCount = _db.FavoriteItems
                        .Count(fi => fi.FavoriteId == f.FavoriteId)
                })
                .ToList();

            return Ok(new ApiResponses
            {
                Code = 1,
                Message = "获取收藏夹成功",
                Data = ret
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok(ApiResponses.ErrorResult);
        }
    }

    [HttpPost]
    public IActionResult FavoriteListItems(FavoriteListItemDto dto)
    {
        if (!DbFlagsHelper.TryDeFlags(dto.Flag, out sbyte authority, out sbyte idCategory))
        {
            return Ok(new ApiResponses { Code = -1, Message = "参数错误" });
        }

        try
        {
            var ret = _db.Favorites.FirstOrDefault(f =>
                f.FavoriteId == dto.FavoriteId && f.Authority == authority && f.IdCategory == idCategory);
            if (ret is null)
            {
                goto bad;
            }

            object data = null;
            switch (ret.IdCategory)
            {
                case IdCategoryConstant.Ingredient:
                    data = (from fi in _db.FavoriteItems
                        join i in _db.Ingredients on fi.TId equals i.IngredientId
                        where fi.FavoriteId == ret.FavoriteId
                        select new
                        {
                            i.IngredientId,
                            i.IName,
                            i.Refer,
                            i.Unit,
                            i.Quantity,
                            Nutritional = i.NutritionalComposition,
                            i.Allergy,
                            i.Content,
                            FileUri = Url.GetIngredientUrl(Request, i.FileUri),
                            Dosage = 100,
                        }).ToList();
                    break;
                case IdCategoryConstant.Recipe:
                    switch (ret.Authority)
                    {
                        case FavoriteAuthorityConstant.Private:
                            data = (from fi in _db.FavoriteItems
                                join r in _db.Recipes on fi.TId equals r.RecipeId
                                where fi.FavoriteId == ret.FavoriteId
                                select new
                                {
                                    FileUri = Url.GetRecipeUrl(Request, r.FileUri),
                                    r.RecipeId,
                                    r.Title,
                                    r.Summary,
                                }).ToList();
                            break;
                        case FavoriteAuthorityConstant.Public:
                            data = (from fi in _db.FavoriteItems
                                join r in _db.Recipes on fi.TId equals r.RecipeId
                                where fi.FavoriteId == ret.FavoriteId
                                select new
                                {
                                    FileUri = Url.GetRecipeUrl(Request, r.FileUri),
                                    r.RecipeId,
                                    r.Title,
                                    r.Summary,
                                }).ToList();
                            break;
                    }
                    break;
                default:
                    goto bad;
            }

            return Ok(new ApiResponses
            {
                Code = 1,
                Message = "获取收藏夹成功",
                Data = data
            });
            bad:
            return Ok(new ApiResponses { Code = -1, Message = "收藏夹不存在" });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok(ApiResponses.ErrorResult);
        }
    }

    #endregion
}