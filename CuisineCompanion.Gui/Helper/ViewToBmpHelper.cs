using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace CuisineCompanion.Helper;

public static class ViewToBmpHelper
{
    private static bool SaveToBmp(this FrameworkElement element, string path)
    {
        try
        {
            var bmp = new RenderTargetBitmap(
                (int)element.ActualWidth,
                (int)element.ActualHeight,
                96,
                96,
                PixelFormats.Pbgra32);
            bmp.Render(element);

            var encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            using var fs = new FileStream(path, FileMode.Create);

            encoder.Save(fs);
        }
        catch (Exception e)
        {
           MsgBoxHelper.TryError(e.Message);
            return false;
        }

        return true;
    }

    /// <summary>
    /// 选择导出路径并保存
    /// </summary>
    /// <param name="element">需要导出的界面元素</param>
    public static void ShowSaveToBmp(this FrameworkElement element, string filename)
    {
        var dialog = new SaveFileDialog
        {
            Filter = "png|*.png",
            FileName = $"{filename}.png"
        };
        if (dialog.ShowDialog() == false) return;
        if (!element.SaveToBmp(dialog.FileName)) return;
        if (MsgBoxHelper.Confirmation("保存成功, 是否打开？"))
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = dialog.FileName,
                UseShellExecute = true,
                Verb = "open"
            });
        }
    }
}