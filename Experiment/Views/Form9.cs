namespace Experiment.Views;

public partial class Form9 : Form
{
    public Form9()
    {
        InitializeComponent();
        Load += (_, _) =>
        {
            GroupBox box = new GroupBox
            {
                Text = "测试",
                Location = new Point(55, 55),
                FlatStyle = FlatStyle.System,
            };

            RadioButton radioButton1 = new RadioButton
            {
                Location = new Point(55, 30),
                Text = "测试1",
            };
            RadioButton radioButton2 = new RadioButton
            {
                Location = new Point(55, 60),
                Text = "测试2",
            };

            box.Controls.Add(radioButton1);
            box.Controls.Add(radioButton2);
            Controls.Add(box);
        };
    }
}