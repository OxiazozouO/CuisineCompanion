using System.Diagnostics;
using System.Security.Policy;

namespace Experiment.Views;

public partial class Form5 : Form
{
    public Form5()
    {
        InitializeComponent();
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        linkLabel1.LinkVisited = true;
        Process.Start(new ProcessStartInfo
        {
            FileName = "https://github.com/OxiazozouO/CuisineCompanion",
            UseShellExecute = true
        });
    }
}