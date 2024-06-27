using System.ComponentModel;

namespace WinFormsApp2.Views.Edit;

partial class MedicalRecordsEditForm
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
        label8 = new Label();
        label9 = new Label();
        label10 = new Label();
        label11 = new Label();
        label12 = new Label();
        label13 = new Label();
        label14 = new Label();
        button1 = new Button();
        button2 = new Button();
        richTextBox1 = new RichTextBox();
        richTextBox2 = new RichTextBox();
        richTextBox3 = new RichTextBox();
        label15 = new Label();
        label16 = new Label();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(25, 12);
        label1.Name = "label1";
        label1.Size = new Size(82, 24);
        label1.TabIndex = 0;
        label1.Text = "医生姓名";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(113, 12);
        label2.Name = "label2";
        label2.Size = new Size(73, 24);
        label2.TabIndex = 1;
        label2.Text = "xxxxxxx";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(61, 56);
        label3.Name = "label3";
        label3.Size = new Size(46, 24);
        label3.TabIndex = 3;
        label3.Text = "专科";
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(113, 56);
        label4.Name = "label4";
        label4.Size = new Size(73, 24);
        label4.TabIndex = 2;
        label4.Text = "xxxxxxx";
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Location = new Point(25, 100);
        label5.Name = "label5";
        label5.Size = new Size(82, 24);
        label5.TabIndex = 4;
        label5.Text = "联系电话";
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Location = new Point(113, 100);
        label6.Name = "label6";
        label6.Size = new Size(73, 24);
        label6.TabIndex = 5;
        label6.Text = "xxxxxxx";
        // 
        // label7
        // 
        label7.AutoSize = true;
        label7.Location = new Point(341, 12);
        label7.Name = "label7";
        label7.Size = new Size(82, 24);
        label7.TabIndex = 6;
        label7.Text = "患者姓名";
        // 
        // label8
        // 
        label8.AutoSize = true;
        label8.Location = new Point(429, 12);
        label8.Name = "label8";
        label8.Size = new Size(73, 24);
        label8.TabIndex = 7;
        label8.Text = "xxxxxxx";
        // 
        // label9
        // 
        label9.AutoSize = true;
        label9.Location = new Point(377, 91);
        label9.Name = "label9";
        label9.Size = new Size(46, 24);
        label9.TabIndex = 8;
        label9.Text = "年龄";
        // 
        // label10
        // 
        label10.AutoSize = true;
        label10.Location = new Point(429, 91);
        label10.Name = "label10";
        label10.Size = new Size(73, 24);
        label10.TabIndex = 9;
        label10.Text = "xxxxxxx";
        // 
        // label11
        // 
        label11.AutoSize = true;
        label11.Location = new Point(582, 12);
        label11.Name = "label11";
        label11.Size = new Size(46, 24);
        label11.TabIndex = 10;
        label11.Text = "性别";
        // 
        // label12
        // 
        label12.AutoSize = true;
        label12.Location = new Point(643, 12);
        label12.Name = "label12";
        label12.Size = new Size(73, 24);
        label12.TabIndex = 11;
        label12.Text = "xxxxxxx";
        // 
        // label13
        // 
        label13.AutoSize = true;
        label13.Location = new Point(546, 91);
        label13.Name = "label13";
        label13.Size = new Size(82, 24);
        label13.TabIndex = 12;
        label13.Text = "联系电话";
        // 
        // label14
        // 
        label14.AutoSize = true;
        label14.Location = new Point(643, 91);
        label14.Name = "label14";
        label14.Size = new Size(73, 24);
        label14.TabIndex = 13;
        label14.Text = "xxxxxxx";
        // 
        // button1
        // 
        button1.Location = new Point(61, 862);
        button1.Name = "button1";
        button1.Size = new Size(266, 69);
        button1.TabIndex = 15;
        button1.Text = "确定";
        button1.UseVisualStyleBackColor = true;
        button1.Click += Save_Click;
        // 
        // button2
        // 
        button2.Location = new Point(459, 862);
        button2.Name = "button2";
        button2.Size = new Size(266, 69);
        button2.TabIndex = 16;
        button2.Text = "取消";
        button2.UseVisualStyleBackColor = true;
        button2.Click += Close_Click;
        // 
        // richTextBox1
        // 
        richTextBox1.Location = new Point(8, 137);
        richTextBox1.Name = "richTextBox1";
        richTextBox1.Size = new Size(780, 194);
        richTextBox1.TabIndex = 17;
        richTextBox1.Text = "";
        // 
        // richTextBox2
        // 
        richTextBox2.Location = new Point(8, 337);
        richTextBox2.Name = "richTextBox2";
        richTextBox2.Size = new Size(780, 175);
        richTextBox2.TabIndex = 18;
        richTextBox2.Text = "";
        // 
        // richTextBox3
        // 
        richTextBox3.Location = new Point(8, 518);
        richTextBox3.Name = "richTextBox3";
        richTextBox3.Size = new Size(780, 271);
        richTextBox3.TabIndex = 19;
        richTextBox3.Text = "";
        // 
        // label15
        // 
        label15.AutoSize = true;
        label15.Location = new Point(519, 803);
        label15.Name = "label15";
        label15.Size = new Size(82, 24);
        label15.TabIndex = 20;
        label15.Text = "记录时间";
        // 
        // label16
        // 
        label16.AutoSize = true;
        label16.Location = new Point(607, 803);
        label16.Name = "label16";
        label16.Size = new Size(73, 24);
        label16.TabIndex = 21;
        label16.Text = "xxxxxxx";
        // 
        // MedicalRecordsEditForm
        // 
        AutoScaleDimensions = new SizeF(11F, 24F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 943);
        Controls.Add(label16);
        Controls.Add(label15);
        Controls.Add(richTextBox3);
        Controls.Add(richTextBox2);
        Controls.Add(richTextBox1);
        Controls.Add(button2);
        Controls.Add(button1);
        Controls.Add(label14);
        Controls.Add(label13);
        Controls.Add(label12);
        Controls.Add(label11);
        Controls.Add(label10);
        Controls.Add(label9);
        Controls.Add(label8);
        Controls.Add(label7);
        Controls.Add(label6);
        Controls.Add(label5);
        Controls.Add(label3);
        Controls.Add(label4);
        Controls.Add(label2);
        Controls.Add(label1);
        Name = "MedicalRecordsEditForm";
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
    private Label label8;
    private Label label9;
    private Label label10;
    private Label label11;
    private Label label12;
    private Label label13;
    private Label label14;
    private Button button1;
    private Button button2;
    private RichTextBox richTextBox1;
    private RichTextBox richTextBox2;
    private RichTextBox richTextBox3;
    private Label label15;
    private Label label16;
}