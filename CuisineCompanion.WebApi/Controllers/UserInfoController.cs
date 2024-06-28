using CuisineCompanion.WebApi.DataModel;
using CuisineCompanion.WebApi.DTOs;
using CuisineCompanion.WebApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace CuisineCompanion.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserInfoController : MyControllerBase
{
    public UserInfoController(RecipeManagementSystemContext db) : base(db)
    {
    }

    [HttpPost]
    public IActionResult MyAllInfo(AuthenticationDto dto)
    {
        try
        {
            if (!UserService.TryVerifyUserToken(_db, dto.UserToken, out var user, out var error)) return Ok(error);

            var data = _db.UserPhysicalInfos
                .Where(u => u.UId == user.UserId)
                .Select(u => new
                {
                    u.UpiId,
                    u.Height,
                    u.Weight,
                    ActivityLevelStr = u.ActivityLevel,
                    u.ProteinRequirement,
                    u.FatPercentage,
                    u.UpdateTime
                }).ToList();

            return Ok(new ApiResponses
            {
                Code = 1,
                Message = "获取用户信息成功",
                Data = new
                {
                    user.UserId,
                    UserName = user.UName,
                    user.Gender,
                    user.BirthDate,
                    UserInfoList = data
                }
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Ok(ApiResponses.ErrorResult);
    }

    [HttpPost]
    public IActionResult AddMyInfo(UserInfoAddDto dto)
    {
        try
        {
            if (!UserService.TryVerifyUserToken(_db, dto.UserToken, out var user, out var error)) return Ok(error);

            var info = new UserPhysicalInfo
            {
                UId = user.UserId,
                Height = dto.Height,
                Weight = dto.Weight,
                ActivityLevel = dto.ActivityLevel,
                ProteinRequirement = dto.ProteinRequirement,
                FatPercentage = dto.FatPercentage,
                UpdateTime = DateTime.Now
            };

            _db.UserPhysicalInfos.Add(info);

            if (_db.SaveChanges() == 1)
                return Ok(new ApiResponses
                {
                    Code = 1,
                    Message = "添加成功",
                    Data = new
                    {
                        info.UpiId,
                        info.UpdateTime
                    }
                });
            return Ok(new ApiResponses
            {
                Code = -1,
                Message = "添加失败"
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Ok(ApiResponses.ErrorResult);
    }

    [HttpPost]
    public IActionResult UpdateMyInfo(UserInfoUpdateDto dto)
    {
        try
        {
            if (!UserService.TryVerifyUserToken(_db, dto.UserToken, out var user, out var error)) return Ok(error);

            var info = _db.UserPhysicalInfos.FirstOrDefault(u => u.UpiId == dto.UpiId);
            if (info is not null)
            {
                info.Height = dto.Height;
                info.Weight = dto.Weight;
                info.ActivityLevel = dto.ActivityLevel;
                info.ProteinRequirement = dto.ProteinRequirement;
                info.FatPercentage = dto.FatPercentage;
                _db.UserPhysicalInfos.Update(info);
                if (_db.SaveChanges() == 1)
                    return Ok(new ApiResponses
                    {
                        Code = 1
                    });
                return Ok(new ApiResponses
                {
                    Code = -1,
                    Message = "更新失败"
                });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Ok(ApiResponses.ErrorResult);
    }
    
    [HttpPost]
    public IActionResult DeleteMyInfo(UserSelectionDto dto)
    {
        try
        {
            if (!UserService.TryVerifyUserToken(_db, dto.UserToken, out var user, out var error)) return Ok(error);

            var info = _db.UserPhysicalInfos.FirstOrDefault(u => u.UpiId == dto.Id);
            if (info is not null)
            {
                _db.UserPhysicalInfos.Remove(info);
                if (_db.SaveChanges() == 1)
                    return Ok(new ApiResponses
                    {
                        Code = 1
                    });
                return Ok(new ApiResponses
                {
                    Code = -1,
                    Message = "删除失败"
                });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Ok(ApiResponses.ErrorResult);
    }
}