using System.ComponentModel;

namespace Experiment.Views;

partial class Form5
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
        linkLabel1 = new LinkLabel();
        SuspendLayout();
        // 
        // linkLabel1
        // 
        linkLabel1.AutoSize = true;
        linkLabel1.Font = new Font("Microsoft YaHei UI", 26F, FontStyle.Regular, GraphicsUnit.Point);
        linkLabel1.Location = new Point(114, 98);
        linkLabel1.Name = "linkLabel1";
        linkLabel1.Size = new Size(341, 67);
        linkLabel1.TabIndex = 0;
        linkLabel1.TabStop = true;
        linkLabel1.Text = "打开一个网站";
        linkLabel1.LinkClicked += linkLabel1_LinkClicked;
        // 
        // Form5
        // 
        AutoScaleDimensions = new SizeF(11F, 24F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(linkLabel1);
        Name = "Form5";
        Text = "Form5";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private LinkLabel linkLabel1;
}