using System.ComponentModel;

namespace Experiment.Views;

partial class Form2
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        addButton = new Button();
        subButton = new Button();
        SuspendLayout();
        // 
        // button1
        // 
        addButton.Location = new Point(135, 165);
        addButton.Name = "addButton";
        addButton.Size = new Size(177, 86);
        addButton.TabIndex = 0;
        addButton.Text = "增加透明度";
        addButton.UseVisualStyleBackColor = true;
        addButton.Click += AddButtonClick;
        // 
        // button2
        // 
        subButton.Location = new Point(467, 165);
        subButton.Name = "subButton";
        subButton.Size = new Size(177, 86);
        subButton.TabIndex = 1;
        subButton.Text = "降低透明度";
        subButton.UseVisualStyleBackColor = true;
        subButton.Click += SubButtonClick;
        // 
        // FormOpacity
        // 
        AutoScaleDimensions = new SizeF(11F, 24F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(subButton);
        Controls.Add(addButton);
        Name = "Form2";
        Text = "可调节透明度的窗体";
        ResumeLayout(false);
    }

    #endregion

    private Button addButton;
    private Button subButton;
}