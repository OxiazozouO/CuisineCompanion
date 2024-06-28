using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace CuisineCompanion.WebApi;

public static class FileUrlHelper
{
    public const string OldFilePath = "File";
    public static string Recipes => TryCreate();
    public static string Favorites => TryCreate();
    public static string DeletedFavorites => TryCreate();
    public static string Ingredients => TryCreate();

    public static string Temps => TryCreate();

    //变量名变成字符串
    private static string TryCreate([CallerMemberName] string name = "")
    {
        if (name == "") return "";
        var dir = Path.Combine(OldFilePath, name);
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

        return name;
    }

    public static string? GetRecipeUrl(this IUrlHelper urlHelper, HttpRequest request, string? fileName)
    {
        return urlHelper.Action("GetImageFile", "File", new { path = Recipes, fileName }, request.Scheme,
            request.Host.Value);
    }

    public static string? GetFavoriteUrl(this IUrlHelper urlHelper, HttpRequest request, string? fileName)
    {
        return urlHelper.Action("GetImageFile", "File", new { path = Favorites, fileName }, request.Scheme,
            request.Host.Value);
    }

    public static string? GetIngredientUrl(this IUrlHelper urlHelper, HttpRequest request, string? fileName)
    {
        return urlHelper.Action("GetImageFile", "File", new { path = Ingredients, fileName }, request.Scheme,
            request.Host.Value);
    }


    public static bool MoveTo(string path, string fileName, string filePathName)
    {
        try
        {
            if (!ExistsForm(path, fileName, out var fileUrl)) return false;

            var filePath = Path.Combine(OldFilePath, filePathName, fileName);

            File.Move(fileUrl, filePath);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public static bool TryMove(string inputFileName, string inputPath, string? oldFileName, string oldPath,
        string backPath)
    {
        var sb = new StringBuilder();
        try
        {
            if (ExistsForm(inputPath, inputFileName, out var inputFilePath))
            {
                var toFilePath = Path.Combine(OldFilePath, oldPath, inputFileName);
                try
                {
                    File.Move(inputFilePath, toFilePath);
                    if (ExistsForm(oldPath, oldFileName, out var oldFilePath))
                    {
                        var backToFilePath = Path.Combine(OldFilePath, backPath, oldFileName);
                        try
                        {
                            File.Move(oldFilePath, backToFilePath);
                            return true;
                        }
                        catch (Exception a)
                        {
                            sb.Append("新的文件移动进来，但是旧文件移动失败");
                            try
                            {
                                File.Move(toFilePath, inputFilePath);
                            }
                            catch (Exception b)
                            {
                                sb.Append(" 新的文件右恢复不了");
                            }
                        }
                    }
                    else
                    {
                        // 旧文件不存在
                        return true;
                    }
                }
                catch (Exception c)
                {
                    sb.Append("新的文件移动失败");
                }
            }
        }
        catch (Exception d)
        {
            sb.Append("新的文件不存在");
        }

        Console.WriteLine(sb.ToString());
        return false;
    }

    public static bool TryMove(string inputFileName, string toPath)
    {
        var sb = new StringBuilder();
        try
        {
            if (ExistsForm(Temps, inputFileName, out var inputFilePath))
            {
                var toFilePath = Path.Combine(OldFilePath, toPath, inputFileName);
                if (File.Exists(toFilePath)) //文件重名或者
                    return false;

                try
                {
                    try
                    {
                        File.Move(inputFilePath, toFilePath);
                    }
                    catch (Exception e)
                    {
                        sb.Append("新的文件撤销移动失败");
                        return false;
                    }

                    return true;
                }
                catch (Exception c)
                {
                    sb.Append("新的文件移动失败");
                }
            }
        }
        catch (Exception d)
        {
            sb.Append("新的文件不存在");
        }

        Console.WriteLine(sb.ToString());
        return false;
    }

    public static bool DeleteTemp(string fileName)
    {
        try
        {
            if (ExistsForm(Temps, fileName, out var fileUrl)) File.Delete(fileUrl);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }

        return true;
    }

    public static bool ExistsForm(string path, string? fileName, out string retPath)
    {
        retPath = "";
        if (string.IsNullOrEmpty(fileName)) return false;
        retPath = Path.Combine(OldFilePath, path, fileName);
        return File.Exists(retPath);
    }
}