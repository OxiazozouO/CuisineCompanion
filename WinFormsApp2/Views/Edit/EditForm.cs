using WinFormsApp2.Helper;

namespace WinFormsApp2.Views.Edit;

public abstract class EditForm<T, R> : Form where T : new()
{
    protected T _model;

    private Action<T> _running;

    protected EditForm(T model, string text)
    {
        _model = model;
        StartPosition = FormStartPosition.CenterScreen;
        Text = text;
    }

    public Action<T> Running
    {
        set => _running = value;
    }

    public abstract R BuildRunning(Action<T> running);

    private void TestValue(object obj)
    {
        var s = Helpers.GetError(obj);
        if (s != "")
        {
            s.ShowError();
            return;
        }

        try
        {
            _running((T)obj);
            Close();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }

    /// <summary>
    ///     model赋值到view
    /// </summary>
    public abstract void SetEnt();

    /// <summary>
    ///     view赋值到model
    /// </summary>
    protected abstract T GetEnt();

    protected void Save_Click(object sender, EventArgs e)
    {
        var ret = GetEnt();
        TestValue(ret);
    }

    protected void Close_Click(object sender, EventArgs e)
    {
        Close();
    }
}