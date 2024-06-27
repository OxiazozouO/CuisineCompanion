namespace Tmp.Helper;

public static class MessageBoxHelper
{
    public static void ShowError(this Exception e) => 
        MessageBox.Show(e.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
}