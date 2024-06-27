using System.ComponentModel;

namespace WinFormsApp2.Views;

partial class DoctorsForm
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
        addButton = new Button();
        deleteButton = new Button();
        updateButton = new Button();
        queryButton = new Button();
        ((ISupportInitialize)dataGridView1).BeginInit();
        SuspendLayout();
        // 
        // dataGridView1
        // 
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Location = new Point(12, 86);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.RowHeadersWidth = 62;
        dataGridView1.RowTemplate.Height = 32;
        dataGridView1.Size = new Size(776, 352);
        dataGridView1.TabIndex = 0;
        dataGridView1.AutoGenerateColumns = false;
        dataGridView1.DoubleClick+= DataGridView1OnDoubleClick;
        dataGridView1.KeyDown += DataGridView1OnKeyDown;
        // 
        // textBox1
        // 
        textBox1.Location = new Point(22, 43);
        textBox1.Name = "textBox1";
        textBox1.Size = new Size(225, 30);
        textBox1.TabIndex = 1;
        // 
        // button1
        // 
        addButton.Location = new Point(379, 37);
        addButton.Name = "addButton";
        addButton.Size = new Size(120, 43);
        addButton.TabIndex = 2;
        addButton.Text = "添加一行";
        addButton.UseVisualStyleBackColor = true;
        addButton.Click += AddButtonClick;
        // 
        // button2
        // 
        deleteButton.Location = new Point(505, 37);
        deleteButton.Name = "deleteButton";
        deleteButton.Size = new Size(120, 43);
        deleteButton.TabIndex = 3;
        deleteButton.Text = "删除";
        deleteButton.UseVisualStyleBackColor = true;
        deleteButton.Click += DeleteButtonClick;
        // 
        // button3
        // 
        updateButton.Location = new Point(631, 37);
        updateButton.Name = "updateButton";
        updateButton.Size = new Size(120, 43);
        updateButton.TabIndex = 4;
        updateButton.Text = "修改";
        updateButton.UseVisualStyleBackColor = true;
        updateButton.Click += UpdateButtonClick;
        // 
        // button4
        // 
        queryButton.Location = new Point(253, 37);
        queryButton.Name = "queryButton";
        queryButton.Size = new Size(120, 43);
        queryButton.TabIndex = 5;
        queryButton.Text = "查找";
        queryButton.UseVisualStyleBackColor = true;
        queryButton.Click += QueryButtonClick;
        // 
        // DoctorsForm
        // 
        AutoScaleDimensions = new SizeF(11F, 24F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(queryButton);
        Controls.Add(updateButton);
        Controls.Add(deleteButton);
        Controls.Add(addButton);
        Controls.Add(textBox1);
        Controls.Add(dataGridView1);
        Name = "DoctorsForm";
        ((ISupportInitialize)dataGridView1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    public DataGridView dataGridView1;
    private TextBox textBox1;
    private Button addButton;
    private Button deleteButton;
    private Button updateButton;
    private Button queryButton;
}