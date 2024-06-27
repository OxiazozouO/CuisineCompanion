using System.ComponentModel;

namespace WinFormsApp2.Views;

partial class PatientsForm
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
        components = new Container();
        dataGridView1 = new DataGridView();
        textBox1 = new TextBox();
        queryButton = new Button();
        updateButton = new Button();
        deleteButton = new Button();
        addButton = new Button();
        ((ISupportInitialize)dataGridView1).BeginInit();
        SuspendLayout();
        // 
        // dataGridView1
        // 
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Location = new Point(0, 87);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.RowHeadersWidth = 62;
        dataGridView1.RowTemplate.Height = 32;
        dataGridView1.Size = new Size(800, 363);
        dataGridView1.TabIndex = 0;
        dataGridView1.AutoGenerateColumns = false;
        dataGridView1.DoubleClick+= DataGridView1OnDoubleClick;
        dataGridView1.KeyDown += DataGridView1OnKeyDown;
        // 
        // textBox1
        // 
        textBox1.Location = new Point(29, 22);
        textBox1.Name = "textBox1";
        textBox1.Size = new Size(157, 30);
        textBox1.TabIndex = 2;
        // 
        // button1
        // 
        queryButton.Location = new Point(203, 21);
        queryButton.Name = "queryButton";
        queryButton.Size = new Size(80, 32);
        queryButton.TabIndex = 3;
        queryButton.Text = "查询";
        queryButton.UseVisualStyleBackColor = true;
        queryButton.Click += QueryButtonClick;
        // 
        // button2
        // 
        updateButton.Location = new Point(306, 22);
        updateButton.Name = "updateButton";
        updateButton.Size = new Size(80, 32);
        updateButton.TabIndex = 4;
        updateButton.Text = "修改";
        updateButton.UseVisualStyleBackColor = true;
        updateButton.Click += UpdateButtonClick;
        // 
        // button3
        // 
        deleteButton.Location = new Point(407, 22);
        deleteButton.Name = "deleteButton";
        deleteButton.Size = new Size(80, 32);
        deleteButton.TabIndex = 5;
        deleteButton.Text = "删除";
        deleteButton.UseVisualStyleBackColor = true;
        deleteButton.Click += DeleteButtonClick;
        // 
        // button4
        // 
        addButton.Location = new Point(517, 22);
        addButton.Name = "addButton";
        addButton.Size = new Size(100, 32);
        addButton.TabIndex = 6;
        addButton.Text = "添加一行";
        addButton.UseVisualStyleBackColor = true;
        addButton.Click += AddButtonClick;
        // 
        // PatientsForm
        // 
        AutoScaleDimensions = new SizeF(11F, 24F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(addButton);
        Controls.Add(deleteButton);
        Controls.Add(updateButton);
        Controls.Add(queryButton);
        Controls.Add(textBox1);
        Controls.Add(dataGridView1);
        Name = "PatientsForm";
        Text = "PatientsForm";
        ((ISupportInitialize)dataGridView1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    public DataGridView dataGridView1;
    private TextBox textBox1;
    private Button queryButton;
    private Button updateButton;
    private Button deleteButton;
    private Button addButton;
}