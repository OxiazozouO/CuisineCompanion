using System.ComponentModel;

namespace WinFormsApp2.Views.Edit;

partial class PatientsEditForm
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
        label3 = new Label();
        label4 = new Label();
        label5 = new Label();
        label6 = new Label();
        label7 = new Label();
        textBox1 = new TextBox();
        comboBox1 = new ComboBox();
        dateTimePicker1 = new DateTimePicker();
        textBox2 = new TextBox();
        textBox3 = new TextBox();
        button1 = new Button();
        button2 = new Button();
        label8 = new Label();
        label9 = new Label();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(79, 58);
        label1.Name = "label1";
        label1.Size = new Size(82, 24);
        label1.TabIndex = 0;
        label1.Text = "患者姓名";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(96, 120);
        label2.Name = "label2";
        label2.Size = new Size(46, 24);
        label2.TabIndex = 1;
        label2.Text = "性别";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(79, 186);
        label3.Name = "label3";
        label3.Size = new Size(82, 24);
        label3.TabIndex = 2;
        label3.Text = "出生日期";
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(79, 250);
        label4.Name = "label4";
        label4.Size = new Size(82, 24);
        label4.TabIndex = 3;
        label4.Text = "联系方式";
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Location = new Point(79, 317);
        label5.Name = "label5";
        label5.Size = new Size(82, 24);
        label5.TabIndex = 4;
        label5.Text = "电子邮件";
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Location = new Point(230, 9);
        label6.Name = "label6";
        label6.Size = new Size(81, 24);
        label6.TabIndex = 5;
        label6.Text = "患者id：";
        // 
        // label7
        // 
        label7.AutoSize = true;
        label7.Location = new Point(298, 9);
        label7.Name = "label7";
        label7.Size = new Size(82, 24);
        label7.TabIndex = 6;
        label7.Text = "xxxxxxxx";
        // 
        // textBox1
        // 
        textBox1.Location = new Point(202, 55);
        textBox1.Name = "textBox1";
        textBox1.Size = new Size(465, 30);
        textBox1.TabIndex = 7;
        // 
        // comboBox1
        // 
        comboBox1.FormattingEnabled = true;
        comboBox1.Location = new Point(202, 117);
        comboBox1.Name = "comboBox1";
        comboBox1.Size = new Size(465, 32);
        comboBox1.TabIndex = 8;
        // 
        // dateTimePicker1
        // 
        dateTimePicker1.Location = new Point(202, 181);
        dateTimePicker1.Name = "dateTimePicker1";
        dateTimePicker1.Size = new Size(465, 30);
        dateTimePicker1.TabIndex = 9;
        // 
        // textBox2
        // 
        textBox2.Location = new Point(202, 247);
        textBox2.Name = "textBox2";
        textBox2.Size = new Size(465, 30);
        textBox2.TabIndex = 10;
        // 
        // textBox3
        // 
        textBox3.Location = new Point(202, 314);
        textBox3.Name = "textBox3";
        textBox3.Size = new Size(465, 30);
        textBox3.TabIndex = 11;
        // 
        // button1
        // 
        button1.Location = new Point(211, 376);
        button1.Name = "button1";
        button1.Size = new Size(160, 38);
        button1.TabIndex = 12;
        button1.Text = "保存";
        button1.UseVisualStyleBackColor = true;
        button1.Click += Save_Click;
        // 
        // button2
        // 
        button2.Location = new Point(487, 376);
        button2.Name = "button2";
        button2.Size = new Size(160, 38);
        button2.TabIndex = 13;
        button2.Text = "取消";
        button2.UseVisualStyleBackColor = true;
        button2.Click += Close_Click;
        // 
        // label8
        // 
        label8.AutoSize = true;
        label8.Location = new Point(426, 9);
        label8.Name = "label8";
        label8.Size = new Size(100, 24);
        label8.TabIndex = 14;
        label8.Text = "注册时间：";
        // 
        // label9
        // 
        label9.AutoSize = true;
        label9.Location = new Point(513, 9);
        label9.Name = "label9";
        label9.Size = new Size(82, 24);
        label9.TabIndex = 15;
        label9.Text = "xxxxxxxx";
        // 
        // PatientsEditForm
        // 
        AutoScaleDimensions = new SizeF(11F, 24F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(label9);
        Controls.Add(label8);
        Controls.Add(button2);
        Controls.Add(button1);
        Controls.Add(textBox3);
        Controls.Add(textBox2);
        Controls.Add(dateTimePicker1);
        Controls.Add(comboBox1);
        Controls.Add(textBox1);
        Controls.Add(label7);
        Controls.Add(label6);
        Controls.Add(label5);
        Controls.Add(label4);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(label1);
        Name = "PatientsEditForm";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label label6;
    private Label label7;
    private TextBox textBox1;
    private ComboBox comboBox1;
    private DateTimePicker dateTimePicker1;
    private TextBox textBox2;
    private TextBox textBox3;
    private Button button1;
    private Button button2;
    private Label label8;
    private Label label9;
}