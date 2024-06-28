using CuisineCompanion.WebApi.DataModel;
using CuisineCompanion.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CuisineCompanion.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ConfigController : MyControllerBase
{
    public ConfigController(RecipeManagementSystemContext db) : base(db)
    {
    }

    [HttpPost]
    public IActionResult GetConfigs(ConfigDto dto)
    {
        var arr = dto.Key.Split(',');
        var list = _db.Cfgs.Where(c => arr.Contains(c.CfgK)).ToList();
        var result = new Dictionary<string, string>();
        foreach (var cfg in list) result[cfg.CfgK] = cfg.CfgV;
        return Ok(new ApiResponses
        {
            Code = 1,
            Message = "success",
            Data = result
        });
    }
}