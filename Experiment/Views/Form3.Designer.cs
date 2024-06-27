using System.ComponentModel;

namespace Experiment.Views;

partial class Form3
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
        label1 = new Label();
        label2 = new Label();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Font = new Font("Microsoft YaHei UI", 26F, FontStyle.Regular, GraphicsUnit.Point);
        label1.Location = new Point(60, 99);
        label1.Name = "label1";
        label1.Size = new Size(186, 67);
        label1.TabIndex = 0;
        label1.Text = "¡°&Print¡±";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Font = new Font("Microsoft YaHei UI", 26F, FontStyle.Regular, GraphicsUnit.Point);
        label2.Location = new Point(359, 99);
        label2.Name = "label2";
        label2.Size = new Size(407, 67);
        label2.TabIndex = 1;
        label2.Text = "¡°&Copy && Paste¡±";
        // 
        // Form3
        // 
        AutoScaleDimensions = new SizeF(11F, 24F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(label2);
        Controls.Add(label1);
        Name = "Form3";
        Text = "Form3";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private Label label2;
}