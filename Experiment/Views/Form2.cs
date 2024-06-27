namespace Experiment.Views;

public partial class Form2 : Form
{
    public Form2()
    {
        InitializeComponent();
    }

    private void AddButtonClick(object sender, EventArgs e)
    {
        Opacity += 0.1;
    }

    private void SubButtonClick(object sender, EventArgs e)
    {
        if (Opacity <= 0.11)
        {
            Close();
            return;
        }

        Opacity -= 0.1;
    }
}