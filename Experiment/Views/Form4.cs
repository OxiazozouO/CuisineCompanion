namespace Experiment.Views;

public partial class Form4 : Form
{
    public Form4()
    {
        InitializeComponent();
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        new MainForm().Show();
        linkLabel1.LinkVisited = true;
    }
}