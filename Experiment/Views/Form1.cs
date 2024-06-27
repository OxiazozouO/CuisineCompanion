namespace Experiment.Views;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void AddButtonClick(object sender, EventArgs e)
    {
        if (textBox1.Text is "" || textBox2.Text is "")
            MessageBox.Show(this, "请输入数字", "msg", MessageBoxButtons.OK, MessageBoxIcon.Error);

        try
        {
            var d1 = decimal.Parse(textBox1.Text);
            var d2 = decimal.Parse(textBox2.Text);
            textBox3.Text = (d1 + d2).ToString();
        }
        catch (Exception exception)
        {
            MessageBox.Show(this, exception.ToString(), "msg", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Console.WriteLine(exception);
        }
    }

    private void ExitButtonClick(object sender, EventArgs e) => Close();
}