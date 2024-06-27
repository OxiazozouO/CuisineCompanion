using CuisineCompanion.Helper;
using Microsoft.AspNetCore.Mvc;
using static System.IO.File;

namespace CuisineCompanion.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class FileController : ControllerBase
{
    [HttpGet("files/{path}/{fileName}")]
    public IActionResult GetImageFile(string path, string fileName)
    {
        var filePath = Path.Combine(FileUrlHelper.OldFilePath, path, fileName);

        if (!Exists(filePath))
        {
            return NotFound();
        }

        var fileStream = OpenRead(filePath);
        var fileExtension = Path.GetExtension(fileName)[1..];

        string contentType;
        switch (fileExtension)
        {
            case "jpg":
            case "jpeg":
            case "png":
            case "gif":
            case "webp":
                contentType = $"image/{fileExtension}";
                break;
            default:
                return NotFound();
                break;
        }

        return File(fileStream, contentType);
    }

    [HttpPost]
    public Task<IActionResult> Upload(IFormFile file)
    {
        try
        {
            if (file == null)
            {
                return Task.FromResult<IActionResult>(Ok(new ApiResponses { Code = -1, Message = "文件为空" }));
            }

            if (file.Length > 1024 * 1024 * 10)
            {
                return Task.FromResult<IActionResult>(Ok(new ApiResponses { Code = -1, Message = "文件大小超过10MB" }));
            }

            if (file.ContentType.Contains("application/octet-stream") || file.ContentType.Contains("image"))
            {
                var fileName = Path.GetFileName(file.FileName);
                var ext = Path.GetExtension(fileName);
                var newFileName = StringHelper.GetRandomString() + ext;
                var newFilePath = Path.Combine(FileUrlHelper.OldFilePath, FileUrlHelper.Temps, newFileName);
                using var stream = new FileStream(newFilePath, FileMode.Create);
                file.CopyTo(stream);
                return Task.FromResult<IActionResult>(Ok(new ApiResponses
                    { Code = 0, Message = "上传成功", Data = newFileName }));
            }

            return Task.FromResult<IActionResult>(Ok(new ApiResponses { Code = -1, Message = "只允许上传图片" }));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Task.FromResult<IActionResult>(Ok(new ApiResponses { Code = -1, Message = "上传失败" }));
    }
}