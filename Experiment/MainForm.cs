using Experiment.Views;
using Tmp.Helper;

namespace Experiment;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        Load += (_, _) =>
        {
            button1.Click += button_Click;
            button2.Click += button_Click;
            button3.Click += button_Click;
            button4.Click += button_Click;
            button5.Click += button_Click;
            button6.Click += button_Click;
            button7.Click += button_Click;
            button8.Click += button_Click;
            button9.Click += button_Click;
        };
    }

    private void button_Click(object? sender, EventArgs e)
    {
        try
        {
            Button button = (Button)sender;

            var type = Type.GetType("Experiment.Views.Form" + button.Name[^1]);
            Form form = (Form)Activator.CreateInstance(type);
            form.Show();
        }
        catch (Exception exception)
        {
            exception.ShowError();
        }
    }
}