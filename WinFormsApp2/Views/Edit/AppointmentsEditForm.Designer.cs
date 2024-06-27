using System.ComponentModel;

namespace WinFormsApp2.Views.Edit;

partial class AppointmentsEditForm
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
        richTextBox1 = new RichTextBox();
        splitContainer1 = new SplitContainer();
        dateTimePicker1 = new DateTimePicker();
        label1 = new Label();
        label4 = new Button();
        label5 = new Label();
        label6 = new Label();
        dateTimePicker2 = new DateTimePicker();
        label2 = new Label();
        label10 = new Label();
        label9 = new Label();
        label8 = new Label();
        label7 = new Button();
        label3 = new Label();
        button1 = new Button();
        button2 = new Button();
        label11 = new Label();
        label12 = new Label();
        label13 = new Label();
        label14 = new Label();
        label15 = new Label();
        label16 = new Label();
        label17 = new Label();
        comboBox1 = new ComboBox();
        ((ISupportInitialize)splitContainer1).BeginInit();
        splitContainer1.Panel1.SuspendLayout();
        splitContainer1.Panel2.SuspendLayout();
        splitContainer1.SuspendLayout();
        SuspendLayout();
        // 
        // richTextBox1
        // 
        richTextBox1.Location = new Point(6, 256);
        richTextBox1.Name = "richTextBox1";
        richTextBox1.Size = new Size(784, 336);
        richTextBox1.TabIndex = 5;
        richTextBox1.Text = "";
        // 
        // splitContainer1
        // 
        splitContainer1.Location = new Point(6, 5);
        splitContainer1.Name = "splitContainer1";
        // 
        // splitContainer1.Panel1
        // 
        splitContainer1.Panel1.Controls.Add(label14);
        splitContainer1.Panel1.Controls.Add(label12);
        splitContainer1.Panel1.Controls.Add(label11);
        splitContainer1.Panel1.Controls.Add(dateTimePicker1);
        splitContainer1.Panel1.Controls.Add(label1);
        splitContainer1.Panel1.Controls.Add(label4);
        splitContainer1.Panel1.Controls.Add(label5);
        splitContainer1.Panel1.Controls.Add(label6);
        splitContainer1.Panel1.Controls.Add(dateTimePicker2);
        splitContainer1.Panel1.Controls.Add(label2);
        // 
        // splitContainer1.Panel2
        // 
        splitContainer1.Panel2.Controls.Add(comboBox1);
        splitContainer1.Panel2.Controls.Add(label17);
        splitContainer1.Panel2.Controls.Add(label16);
        splitContainer1.Panel2.Controls.Add(label15);
        splitContainer1.Panel2.Controls.Add(label13);
        splitContainer1.Panel2.Controls.Add(label10);
        splitContainer1.Panel2.Controls.Add(label9);
        splitContainer1.Panel2.Controls.Add(label8);
        splitContainer1.Panel2.Controls.Add(label7);
        splitContainer1.Panel2.Controls.Add(label3);
        splitContainer1.Size = new Size(784, 245);
        splitContainer1.SplitterDistance = 376;
        splitContainer1.TabIndex = 6;
        // 
        // dateTimePicker1
        // 
        dateTimePicker1.Location = new Point(28, 128);
        dateTimePicker1.Name = "dateTimePicker1";
        dateTimePicker1.Size = new Size(307, 30);
        dateTimePicker1.TabIndex = 13;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(28, 101);
        label1.Name = "label1";
        label1.Size = new Size(82, 24);
        label1.TabIndex = 15;
        label1.Text = "预约日期";
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(28, 20);
        label4.Name = "label4";
        label4.Size = new Size(82, 24);
        label4.TabIndex = 18;
        label4.Text = "医生名称";
        label4.Click+= Label4OnClick;
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Location = new Point(28, 60);
        label5.Name = "label5";
        label5.Size = new Size(46, 24);
        label5.TabIndex = 19;
        label5.Text = "专科";
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Location = new Point(171, 60);
        label6.Name = "label6";
        label6.Size = new Size(82, 24);
        label6.TabIndex = 20;
        label6.Text = "医生电话";
        // 
        // dateTimePicker2
        // 
        dateTimePicker2.Location = new Point(28, 188);
        dateTimePicker2.Name = "dateTimePicker2";
        dateTimePicker2.Size = new Size(306, 30);
        dateTimePicker2.TabIndex = 14;
        dateTimePicker2.Format = DateTimePickerFormat.Custom;
        dateTimePicker2.CustomFormat = "HH:mm";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(28, 161);
        label2.Name = "label2";
        label2.Size = new Size(82, 24);
        label2.TabIndex = 16;
        label2.Text = "预约时间";
        // 
        // label10
        // 
        label10.AutoSize = true;
        label10.Location = new Point(56, 161);
        label10.Name = "label10";
        label10.Size = new Size(82, 24);
        label10.TabIndex = 24;
        label10.Text = "病人电话";
        // 
        // label9
        // 
        label9.AutoSize = true;
        label9.Location = new Point(56, 77);
        label9.Name = "label9";
        label9.Size = new Size(82, 24);
        label9.TabIndex = 23;
        label9.Text = "病人性别";
        // 
        // label8
        // 
        label8.AutoSize = true;
        label8.Location = new Point(216, 77);
        label8.Name = "label8";
        label8.Size = new Size(82, 24);
        label8.TabIndex = 22;
        label8.Text = "病人年龄";
        // 
        // label7
        // 
        label7.AutoSize = true;
        label7.Location = new Point(56, 20);
        label7.Name = "label7";
        label7.Size = new Size(82, 24);
        label7.TabIndex = 21;
        label7.Text = "病人名称";
        label7.Click+= Label7OnClick;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(216, 161);
        label3.Name = "label3";
        label3.Size = new Size(82, 24);
        label3.TabIndex = 17;
        label3.Text = "当前状态";
        // 
        // button1
        // 
        button1.Location = new Point(73, 608);
        button1.Name = "button1";
        button1.Size = new Size(287, 63);
        button1.TabIndex = 7;
        button1.Text = "确定";
        button1.UseVisualStyleBackColor = true;
        button1.Click += Save_Click;
        // 
        // button2
        // 
        button2.Location = new Point(442, 608);
        button2.Name = "button2";
        button2.Size = new Size(287, 63);
        button2.TabIndex = 8;
        button2.Text = "取消";
        button2.UseVisualStyleBackColor = true;
        button2.Click += Close_Click;
        // 
        // label11
        // 
        label11.AutoSize = true;
        label11.Location = new Point(80, 60);
        label11.Name = "label11";
        label11.Size = new Size(73, 24);
        label11.TabIndex = 21;
        label11.Text = "xxxxxxx";
        // 
        // label12
        // 
        label12.AutoSize = true;
        label12.Location = new Point(259, 60);
        label12.Name = "label12";
        label12.Size = new Size(109, 24);
        label12.TabIndex = 22;
        label12.Text = "xxxxxxxxxxx";
        // 
        // label13
        // 
        label13.AutoSize = true;
        label13.Location = new Point(144, 20);
        label13.Name = "label13";
        label13.Size = new Size(73, 24);
        label13.TabIndex = 23;
        label13.Text = "xxxxxxx";
        // 
        // label14
        // 
        label14.AutoSize = true;
        label14.Location = new Point(116, 20);
        label14.Name = "label14";
        label14.Size = new Size(73, 24);
        label14.TabIndex = 25;
        label14.Text = "xxxxxxx";
        // 
        // label15
        // 
        label15.AutoSize = true;
        label15.Location = new Point(56, 101);
        label15.Name = "label15";
        label15.Size = new Size(73, 24);
        label15.TabIndex = 26;
        label15.Text = "xxxxxxx";
        // 
        // label16
        // 
        label16.AutoSize = true;
        label16.Location = new Point(216, 101);
        label16.Name = "label16";
        label16.Size = new Size(73, 24);
        label16.TabIndex = 27;
        label16.Text = "xxxxxxx";
        // 
        // label17
        // 
        label17.AutoSize = true;
        label17.Location = new Point(56, 185);
        label17.Name = "label17";
        label17.Size = new Size(73, 24);
        label17.TabIndex = 28;
        label17.Text = "xxxxxxx";
        // 
        // comboBox1
        // 
        comboBox1.FormattingEnabled = true;
        comboBox1.Location = new Point(199, 186);
        comboBox1.Name = "comboBox1";
        comboBox1.Size = new Size(119, 32);
        comboBox1.TabIndex = 30;
        // 
        // AppointmentsEditForm
        // 
        AutoScaleDimensions = new SizeF(11F, 24F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(797, 674);
        Controls.Add(button2);
        Controls.Add(button1);
        Controls.Add(splitContainer1);
        Controls.Add(richTextBox1);
        Name = "AppointmentsEditForm";
        splitContainer1.Panel1.ResumeLayout(false);
        splitContainer1.Panel1.PerformLayout();
        splitContainer1.Panel2.ResumeLayout(false);
        splitContainer1.Panel2.PerformLayout();
        ((ISupportInitialize)splitContainer1).EndInit();
        splitContainer1.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion
    private RichTextBox richTextBox1;
    private SplitContainer splitContainer1;
    private DateTimePicker dateTimePicker1;
    private Label label1;
    private Button label4;
    private Label label5;
    private Label label6;
    private DateTimePicker dateTimePicker2;
    private Label label2;
    private Label label10;
    private Label label9;
    private Label label8;
    private Button label7;
    private Label label3;
    private Button button1;
    private Button button2;
    private Label label14;
    private Label label12;
    private Label label11;
    private Label label17;
    private Label label16;
    private Label label15;
    private Label label13;
    private ComboBox comboBox1;
}