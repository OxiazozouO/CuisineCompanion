namespace Experiment.Views;

public partial class Form8 : Form
{
    public Form8()
    {
        InitializeComponent();
    }

    private void addButton_Click(object sender, EventArgs e)
    {
        if (textBox1.Text is "") return;
        comboBox1.Items.Add(textBox1.Text);
    }

    private void addGrandButton_Click(object sender, EventArgs e)
    {
        comboBox1.BeginUpdate();
        for (var i = 0; i < 100; i++)
            comboBox1.Items.Add("项目 " + i);
        comboBox1.EndUpdate();
    }

    private void findButton_Click(object sender, EventArgs e)
    {
        comboBox1.SelectedIndex = comboBox1.FindString(textBox1.Text);
    }

    private void showSelectedButton_Click(object sender, EventArgs e)
    {
        MessageBox.Show($"选择的是：{comboBox1.SelectedItem}\n索引为：{comboBox1.SelectedIndex}", "msg", MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }
}