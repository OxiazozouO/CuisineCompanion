using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WinFormsApp2.Helper;

public static class Helpers
{
    public static string GetError(object ret)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(ret, null, null);
        Validator.TryValidateObject(ret, context, results, true);
        if (results.Count > 0)
        {
            var sb = new StringBuilder().Append("存在以下错误：\n");
            foreach (var validationResult in results)
                sb.Append(validationResult.ErrorMessage).Append('\n');
            return sb.ToString();
        }

        return "";
    }

    public static void GetSelectedIndex<T>(this DataGridView dataGridView, out List<T> list, out HashSet<int> set)
    {
        set = new HashSet<int>();
        list = dataGridView.DataSource as List<T>;
        if (list is null) return;

        foreach (DataGridViewCell cell in dataGridView.SelectedCells)
            set.Add(dataGridView.Rows[cell.RowIndex].Index);
    }

    public static string Age(this DateTime time)
    {
        return ((DateTime.Now - time).Days / 365).ToString();
    }

    public static void QueueOpen<T>(this List<T> list) where T : Form
    {
        for (var i = 0; i < list.Count - 1; i++)
        {
            var sta = list[i];
            var end = list[i + 1];
            sta.Closed += (_, _) => { end.Show(); };
        }

        if (list.Count > 0)
            list[0].Show();
    }
}