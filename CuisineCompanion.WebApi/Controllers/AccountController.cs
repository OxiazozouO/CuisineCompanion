using CuisineCompanion.Helper;
using CuisineCompanion.WebApi.DataModel;
using CuisineCompanion.WebApi.DTOs;
using CuisineCompanion.WebApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace CuisineCompanion.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountController : MyControllerBase
{
    public AccountController(RecipeManagementSystemContext db) : base(db)
    {
    }

    [HttpPost]
    public IActionResult Register(RegisterDto dto)
    {
        try
        {
            if (dto.Email is null || dto.Phone is null || dto.Name is null || dto.Password is null)
                return Ok(new ApiResponses { Code = -1, Message = "注册信息不为空" });

            var user = _db.Users.FirstOrDefault(u =>
                u.IsLogout == false
                && (u.Email == dto.Email || u.Phone == dto.Phone || u.UName == dto.Name));

            if (user is not null) return Ok(new ApiResponses { Code = -2, Message = "账号已存在" });

            var newUser = new User
            {
                UName = dto.Name,
                Password = dto.Password,
                Salt = dto.Password,
                Email = dto.Email,
                Phone = dto.Phone
            };

            _db.Users.Add(newUser);

            if (_db.SaveChanges() == 1)
                return Ok(new ApiResponses
                {
                    Code = 1,
                    Message = "注册成功",
                    Data = new UserDto
                    {
                        UserId = newUser.UserId,
                        Name = newUser.UName,
                        Email = newUser.Email,
                        Phone = newUser.Phone
                    }
                });

            return Ok(new ApiResponses { Code = -1, Message = "注册失败" });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Ok(ApiResponses.ErrorResult);
    }

    /// <summary>
    ///     登陆
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Login(LoginDto dto)
    {
        try
        {
            var user = _db.Users.FirstOrDefault(u => u.IsLogout == false && (
                u.UName == dto.Identifier || u.Email == dto.Identifier || u.Phone == dto.Identifier));
            if (user == null) return Ok(new ApiResponses { Code = -1, Message = "用户不存在" });

            BackendEncryptionHelper.HasPassword(dto.Password, user.Salt, out var hashpswd);

            if (hashpswd != user.Password) return Ok(new ApiResponses { Code = -2, Message = "密码错误" });

            return Ok(new ApiResponses
            {
                Code = 1,
                Message = "登陆成功",
                Data = new UserToken
                {
                    UserId = user.UserId,
                    UserName = user.UName,
                    Token = DateTime.UtcNow.AddDays(1).ToString()
                }
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok(ApiResponses.ErrorResult);
        }
    }

    [HttpPost]
    public IActionResult Logout(LogoutDto dto)
    {
        try
        {
            if (!UserService.TryVerifyUserToken(_db, dto.UserToken, out var user, out var error)) return Ok(error);

            BackendEncryptionHelper.HasPassword(dto.Password, user.Salt, out var hashpswd);

            if (hashpswd != user.Password) return Ok(new ApiResponses { Code = -1, Message = "密码错误" });

            user.IsLogout = true;
            _db.Update(user);
            if (_db.SaveChanges() == 1)
                return Ok(new ApiResponses
                {
                    Code = 1,
                    Message = "注销成功"
                });

            return Ok(new ApiResponses
            {
                Code = -1,
                Message = "注销失败"
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Ok(ApiResponses.ErrorResult);
    }

    [HttpPost]
    public IActionResult ChangeInfo(UserChangeInfoDto dto)
    {
        try
        {
            if (!UserService.TryVerifyUserToken(_db, dto.UserToken, out var user, out var error)) return Ok(error);

            if (dto.Name != null)
            {
                user.UName = dto.Name;
                user.Gender = dto.Gender;
                user.BirthDate = dto.BirthDate;
                if (_db.SaveChanges() == 1)
                    return Ok(new ApiResponses
                    {
                        Code = 1,
                        Message = "修改成功"
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
    public IActionResult ChangePassword(UserChangePasswordDto dto)
    {
        try
        {
            if (string.IsNullOrEmpty(dto.Password))
                return Ok(new ApiResponses
                {
                    Code = -1,
                    Message = "密码不能为空"
                });
            if (!UserService.TryVerifyUserToken(_db, dto.UserToken, out var user, out var error)) return Ok(error);

            if (user.Phone[^4..] != dto.PhoneCut.ToString())
                return Ok(new ApiResponses
                {
                    Code = -1,
                    Message = "手机号错误"
                });

            BackendEncryptionHelper.HasPassword(dto.Password, user.Salt, out var hashpswd);

            if (hashpswd != user.Password)
                return Ok(new ApiResponses
                {
                    Code = -1,
                    Message = "密码错误"
                });

            BackendEncryptionHelper.HasPassword(dto.NewPassword, user.Salt, out var newhashpswd);
            if (user.Password == newhashpswd)
                return Ok(new ApiResponses
                {
                    Code = -1,
                    Message = "新密码不能与旧密码相同"
                });

            user.Password = newhashpswd;
            _db.Users.Update(user);
            if (_db.SaveChanges() == 1)
                return Ok(new ApiResponses
                {
                    Code = 1,
                    Message = "修改成功"
                });

            return Ok(new ApiResponses
            {
                Code = -1,
                Message = "修改失败"
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Ok(ApiResponses.ErrorResult);
    }

    [HttpPost]
    public IActionResult GetPhone(AuthenticationDto dto)
    {
        try
        {
            if (!UserService.TryVerifyUserToken(_db, dto.UserToken, out var user, out var error)) return Ok(error);

            return Ok(new ApiResponses
            {
                Code = 1,
                Message = "success",
                Data = StringHelper.PhoneDesensitization(user.Phone)
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Ok(ApiResponses.ErrorResult);
    }
}