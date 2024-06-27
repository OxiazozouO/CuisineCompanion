using System.ComponentModel;

namespace WinFormsApp2.Views;

partial class AppointmentsForm
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
        dataGridView1 = new DataGridView();
        textBox1 = new TextBox();
        queryButton = new Button();
        addButton = new Button();
        deleteButton = new Button();
        updateButton = new Button();
        button5 = new Button();
        button6 = new Button();
        ((ISupportInitialize)dataGridView1).BeginInit();
        SuspendLayout();
        // 
        // dataGridView1
        // 
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Location = new Point(12, 123);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.RowHeadersWidth = 62;
        dataGridView1.RowTemplate.Height = 32;
        dataGridView1.Size = new Size(776, 315);
        dataGridView1.TabIndex = 0;
        dataGridView1.AutoGenerateColumns = false;
        dataGridView1.DoubleClick+= DataGridView1OnDoubleClick;
        dataGridView1.KeyDown += DataGridView1OnKeyDown;
        ;
        // 
        // textBox1
        // 
        textBox1.Location = new Point(12, 25);
        textBox1.Name = "textBox1";
        textBox1.Size = new Size(225, 30);
        textBox1.TabIndex = 1;
        // 
        // button1
        // 
        queryButton.Location = new Point(243, 21);
        queryButton.Name = "queryButton";
        queryButton.Size = new Size(108, 39);
        queryButton.TabIndex = 2;
        queryButton.Text = "查找";
        queryButton.UseVisualStyleBackColor = true;
        queryButton.Click += QueryButtonClick;
        // 
        // button2
        // 
        addButton.Location = new Point(357, 21);
        addButton.Name = "addButton";
        addButton.Size = new Size(108, 39);
        addButton.TabIndex = 3;
        addButton.Text = "添加一行";
        addButton.UseVisualStyleBackColor = true;
        addButton.Click += AddButtonClick;
        // 
        // button3
        // 
        deleteButton.Location = new Point(471, 21);
        deleteButton.Name = "deleteButton";
        deleteButton.Size = new Size(108, 39);
        deleteButton.TabIndex = 4;
        deleteButton.Text = "删除";
        deleteButton.UseVisualStyleBackColor = true;
        deleteButton.Click += DeleteButtonClick;
        // 
        // button4
        // 
        updateButton.Location = new Point(585, 21);
        updateButton.Name = "updateButton";
        updateButton.Size = new Size(108, 39);
        updateButton.TabIndex = 5;
        updateButton.Text = "修改";
        updateButton.UseVisualStyleBackColor = true;
        updateButton.Click += UpdateButtonClick;
        // 
        // button5
        // 
        button5.Location = new Point(12, 78);
        button5.Name = "button5";
        button5.Size = new Size(108, 39);
        button5.TabIndex = 6;
        button5.Text = "医师详情";
        button5.UseVisualStyleBackColor = true;
        button5.Click += button5_Click;
        // 
        // button6
        // 
        button6.Location = new Point(129, 78);
        button6.Name = "button6";
        button6.Size = new Size(108, 39);
        button6.TabIndex = 7;
        button6.Text = "病人详情";
        button6.TextImageRelation = TextImageRelation.TextBeforeImage;
        button6.UseVisualStyleBackColor = true;
        button6.Click += button6_Click;
        // 
        // AppointmentsForm
        // 
        AutoScaleDimensions = new SizeF(11F, 24F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(button6);
        Controls.Add(button5);
        Controls.Add(updateButton);
        Controls.Add(deleteButton);
        Controls.Add(addButton);
        Controls.Add(queryButton);
        Controls.Add(textBox1);
        Controls.Add(dataGridView1);
        Name = "AppointmentsForm";
        ((ISupportInitialize)dataGridView1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private DataGridView dataGridView1;
    private TextBox textBox1;
    private Button queryButton;
    private Button addButton;
    private Button deleteButton;
    private Button updateButton;
    private Button button5;
    private Button button6;
}