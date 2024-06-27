using CuisineCompanion.Models;
using CuisineCompanion.WebApi.Constant;
using CuisineCompanion.WebApi.DataModel;
using CuisineCompanion.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CuisineCompanion.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class FavoriteItemsController : MyControllerBase
{
    public FavoriteItemsController(RecipeManagementSystemContext db) : base(db)
    {
    }

    [HttpPost]
    public IActionResult AddFavoriteItems(FavoriteItemCreationDto dto)
    {
        if (!DbFlagsHelper.TryDeFlags(dto.Flag, out sbyte authority, out sbyte idCategory))
        {
            return Ok(new ApiResponses { Code = -1, Message = "参数错误" });
        }

        int userid = dto.UserToken.UserId;
        try
        {
            var favorite = _db.Favorites.FirstOrDefault(f =>
                f.FavoriteId == dto.FavoriteId && f.UserId == userid);
            if (favorite is null)
            {
                return Ok(new ApiResponses { Code = -1, Message = "收藏夹不存在" });
            }

            if (favorite.Authority != authority)
            {
                return Ok(new ApiResponses { Code = -1, Message = "收藏夹权限错误" });
            }

            if (favorite.IdCategory != idCategory)
            {
                return Ok(new ApiResponses { Code = -1, Message = "收藏夹类型不符" });
            }

            var favoriteItem =
                _db.FavoriteItems.FirstOrDefault(fi => fi.FavoriteId == dto.FavoriteId && fi.TId == dto.TId);
            if (favoriteItem is not null)
            {
                return Ok(new ApiResponses { Code = -1, Message = "已存在" });
            }

            _db.FavoriteItems.Add(new FavoriteItem { FavoriteId = dto.FavoriteId, TId = dto.TId });
            if (_db.SaveChanges() == 1)
            {
                return Ok(new ApiResponses { Code = 1, Message = "成功添加到收藏夹" });
            }
            else
            {
                return Ok(new ApiResponses { Code = -1, Message = "添加失败" });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok(ApiResponses.ErrorResult);
        }
    }


    [HttpPost]
    public IActionResult IsLike(FavoriteItemSeleteDto dto)
    {
        DbFlagsHelper.TryGetIdCategory(dto.Flag, out sbyte idCategory);
        if (idCategory == 0)
        {
            return Ok(new ApiResponses { Code = -1, Message = "参数错误" });
        }

        int userid = dto.UserToken.UserId;
        try
        {
            var l = (from fi in _db.FavoriteItems
                join f in _db.Favorites on fi.FavoriteId equals f.FavoriteId
                where f.UserId == userid && f.IdCategory == idCategory && fi.TId == dto.TId
                select fi).Count();

            return Ok(new ApiResponses { Code = 1, Message = "获取成功", Data = l > 0 });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok(ApiResponses.ErrorResult);
        }
    }

    [HttpPost]
    public IActionResult RemoveFavoriteItems(FavoriteItemSeleteDto dto)
    {
        DbFlagsHelper.TryGetIdCategory(dto.Flag, out sbyte idCategory);
        if (idCategory == 0)
        {
            return Ok(new ApiResponses { Code = -1, Message = "参数错误" });
        }

        int userid = dto.UserToken.UserId;
        try
        {
            var fis = (from fi in _db.FavoriteItems
                join f in _db.Favorites on fi.FavoriteId equals f.FavoriteId
                where f.UserId == userid && f.IdCategory == idCategory && fi.TId == dto.TId
                select fi).ToList();

            if (fis is null || fis.Count == 0)
            {
                return Ok(new ApiResponses { Code = -1, Message = "无收藏" });
            }

            _db.FavoriteItems.RemoveRange(fis);
            if (_db.SaveChanges() == fis.Count)
            {
                return Ok(new ApiResponses { Code = 1, Message = "已删除" });
            }
            else
            {
                return Ok(new ApiResponses { Code = -1, Message = "删除失败" });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok(ApiResponses.ErrorResult);
        }
    }

    [HttpPost]
    public IActionResult RemoveFavoriteItem(FavoriteItemCreationDto dto)
    {
        DbFlagsHelper.TryDeFlags(dto.Flag, out sbyte _, out sbyte idCategory);
        if (idCategory == 0)
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

            var favoriteItem =
                _db.FavoriteItems.FirstOrDefault(fi => fi.FavoriteId == dto.FavoriteId && fi.TId == dto.TId);

            if (favoriteItem is null)
            {
                return Ok(new ApiResponses { Code = -1, Message = "无此收藏" });
            }

            _db.FavoriteItems.Remove(favoriteItem);
            if (_db.SaveChanges() == 1)
            {
                return Ok(new ApiResponses { Code = 1, Message = "已删除" });
            }
            else
            {
                return Ok(new ApiResponses { Code = -1, Message = "删除失败" });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok(ApiResponses.ErrorResult);
        }
    }
}