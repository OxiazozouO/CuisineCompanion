using System.Diagnostics;

namespace Experiment.Views;

public partial class Form7 : Form
{
    public Form7()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        ListBox listBox1 = new ListBox
        {
            Size = new Size(600, 200),
            Location = new Point(80, 95),
            MultiColumn = true,
            SelectionMode = SelectionMode.MultiExtended
        };
        Controls.Add(listBox1);
        listBox1.BeginUpdate();
        for (int i = 1; i <= 50; i++)
            listBox1.Items.Add("标签 " + i);

        listBox1.EndUpdate();
        
        listBox1.SetSelected(1, true);
        listBox1.SetSelected(2, true);
        listBox1.SetSelected(3, true);
        
        Debug.WriteLine(listBox1.SelectedItems[1]);
        Debug.WriteLine(listBox1.SelectedItems[0]);
        
        
    }
}