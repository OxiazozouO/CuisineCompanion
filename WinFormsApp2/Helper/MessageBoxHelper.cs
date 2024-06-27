using System.Text;

namespace WinFormsApp2.Helper;

public static class MessageBoxHelper
{
    public static DialogResult ShowError(this Exception e) =>
        MessageBox.Show(e.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public static DialogResult ShowError(this string s) =>
        MessageBox.Show(s, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public static DialogResult ShowWarning(this string s) =>
        MessageBox.Show($"你确定要删除 {s} 吗？\n此后数据不可恢复！！",
        "删除警告", MessageBoxButtons.OKCancel,
        MessageBoxIcon.Warning);
}