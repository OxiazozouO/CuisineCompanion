namespace Experiment.Views;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        label1 = new Label();
        label2 = new Label();
        textBox1 = new TextBox();
        textBox2 = new TextBox();
        label3 = new Label();
        textBox3 = new TextBox();
        addButton = new Button();
        ExitButton = new Button();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(290, 76);
        label1.Name = "label1";
        label1.Size = new Size(75, 24);
        label1.TabIndex = 0;
        label1.Text = "操作数1";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(290, 154);
        label2.Name = "label2";
        label2.Size = new Size(75, 24);
        label2.TabIndex = 1;
        label2.Text = "操作数2";
        // 
        // textBox1
        // 
        textBox1.Location = new Point(371, 70);
        textBox1.Name = "textBox1";
        textBox1.Size = new Size(172, 30);
        textBox1.TabIndex = 2;
        // 
        // textBox2
        // 
        textBox2.Location = new Point(371, 148);
        textBox2.Name = "textBox2";
        textBox2.Size = new Size(172, 30);
        textBox2.TabIndex = 3;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(290, 238);
        label3.Name = "label3";
        label3.Size = new Size(82, 24);
        label3.TabIndex = 4;
        label3.Text = "运算结果";
        // 
        // textBox3
        // 
        textBox3.Location = new Point(371, 232);
        textBox3.Name = "textBox3";
        textBox3.Size = new Size(172, 30);
        textBox3.TabIndex = 5;
        // 
        // addButton
        // 
        addButton.Location = new Point(308, 310);
        addButton.Name = "addButton";
        addButton.Size = new Size(80, 40);
        addButton.TabIndex = 6;
        addButton.Text = "相加";
        addButton.UseVisualStyleBackColor = true;
        addButton.Click += AddButtonClick;
        // 
        // ExitButton
        // 
        ExitButton.Location = new Point(417, 310);
        ExitButton.Name = "ExitButton";
        ExitButton.Size = new Size(80, 40);
        ExitButton.TabIndex = 7;
        ExitButton.Text = "退出";
        ExitButton.UseVisualStyleBackColor = true;
        ExitButton.Click += ExitButtonClick;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(11F, 24F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(ExitButton);
        Controls.Add(addButton);
        Controls.Add(textBox3);
        Controls.Add(label3);
        Controls.Add(textBox2);
        Controls.Add(textBox1);
        Controls.Add(label2);
        Controls.Add(label1);
        Name = "Form1";
        Text = "Form1";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private Label label2;
    private TextBox textBox1;
    private TextBox textBox2;
    private Label label3;
    private TextBox textBox3;
    private Button addButton;
    private Button ExitButton;
}