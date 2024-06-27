using System.IO;
using System.Windows;

namespace CuisineCompanion.Helper;

public static class FileViewHelper
{
    public static bool FileCheck(string filePath)
    {
        string? mes = null;
        if (!File.Exists(filePath))
        {
            mes = "文件不存在";
        }
        else if (new FileInfo(filePath).Length > 1024 * 1024 * 10)
        {
            mes = "文件大小超过10MB";
        }

        return !MsgBoxHelper.TryError(mes);
    }
}