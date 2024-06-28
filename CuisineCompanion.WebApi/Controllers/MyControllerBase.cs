using CuisineCompanion.WebApi.DataModel;
using Microsoft.AspNetCore.Mvc;

namespace CuisineCompanion.WebApi.Controllers;

public class MyControllerBase : ControllerBase
{
    protected readonly RecipeManagementSystemContext _db;

    public MyControllerBase(RecipeManagementSystemContext db)
    {
        _db = db;
    }
}