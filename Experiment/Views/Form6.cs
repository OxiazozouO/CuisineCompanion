namespace Experiment.Views;

public partial class Form6 : Form
{
    public Form6()
    {
        InitializeComponent();
    }

    private void Form6_Load(object sender, EventArgs e)
    {
        TextBox textBox1 = new TextBox
        {
            Location = new Point(80, 55),
            Multiline = true,
            ScrollBars = ScrollBars.Vertical,
            AcceptsReturn = true,
            AcceptsTab = true,
            WordWrap = true,
            Text = "欢迎！",
            Size = new Size(200, 100)
        };
        Controls.Add(textBox1);
    }
}