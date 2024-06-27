using System.ComponentModel;

namespace Experiment.Views;

partial class Form8
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
        addGrandButton = new Button();
        findButton = new Button();
        showSelectedButton = new Button();
        label1 = new Label();
        comboBox1 = new ComboBox();
        textBox1 = new TextBox();
        SuspendLayout();
        // 
        // addButton
        // 
        addButton.Location = new Point(196, 205);
        addButton.Name = "addButton";
        addButton.Size = new Size(110, 81);
        addButton.TabIndex = 0;
        addButton.Text = "添加";
        addButton.UseVisualStyleBackColor = true;
        addButton.Click += addButton_Click;
        // 
        // addGrandButton
        // 
        addGrandButton.Location = new Point(331, 205);
        addGrandButton.Name = "addGrandButton";
        addGrandButton.Size = new Size(127, 81);
        addGrandButton.TabIndex = 1;
        addGrandButton.Text = "添加100个项";
        addGrandButton.UseVisualStyleBackColor = true;
        addGrandButton.Click += addGrandButton_Click;
        // 
        // findButton
        // 
        findButton.Location = new Point(480, 205);
        findButton.Name = "findButton";
        findButton.Size = new Size(110, 81);
        findButton.TabIndex = 2;
        findButton.Text = "查找";
        findButton.UseVisualStyleBackColor = true;
        findButton.Click += findButton_Click;
        // 
        // showSelectedButton
        // 
        showSelectedButton.Location = new Point(321, 319);
        showSelectedButton.Name = "showSelectedButton";
        showSelectedButton.Size = new Size(152, 81);
        showSelectedButton.TabIndex = 3;
        showSelectedButton.Text = "被选中的项是：";
        showSelectedButton.UseVisualStyleBackColor = true;
        showSelectedButton.Click += showSelectedButton_Click;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(321, 403);
        label1.Name = "label1";
        label1.Size = new Size(148, 24);
        label1.TabIndex = 4;
        label1.Text = "* ComBox中的项";
        // 
        // comboBox1
        // 
        comboBox1.FormattingEnabled = true;
        comboBox1.Location = new Point(154, 46);
        comboBox1.Name = "comboBox1";
        comboBox1.Size = new Size(489, 32);
        comboBox1.TabIndex = 5;
        // 
        // textBox1
        // 
        textBox1.Location = new Point(154, 169);
        textBox1.Name = "textBox1";
        textBox1.Size = new Size(489, 30);
        textBox1.TabIndex = 6;
        // 
        // Form8
        // 
        AutoScaleDimensions = new SizeF(11F, 24F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(textBox1);
        Controls.Add(comboBox1);
        Controls.Add(label1);
        Controls.Add(showSelectedButton);
        Controls.Add(findButton);
        Controls.Add(addGrandButton);
        Controls.Add(addButton);
        Name = "Form8";
        Text = "Form8";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button addButton;
    private Button addGrandButton;
    private Button findButton;
    private Button showSelectedButton;
    private Label label1;
    private ComboBox comboBox1;
    private TextBox textBox1;
}