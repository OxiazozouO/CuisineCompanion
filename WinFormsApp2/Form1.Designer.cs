namespace WinFormsApp2;

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
        splitContainer1 = new SplitContainer();
        MedicalRecordsButton = new Button();
        AppointmentsButton = new Button();
        DoctorsButton = new Button();
        PatientsButton = new Button();
        ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
        splitContainer1.Panel1.SuspendLayout();
        splitContainer1.SuspendLayout();
        SuspendLayout();
        // 
        // splitContainer1
        // 
        splitContainer1.Dock = DockStyle.Fill;
        splitContainer1.Location = new Point(0, 0);
        splitContainer1.Name = "splitContainer1";
        // 
        // splitContainer1.Panel1
        // 
        splitContainer1.Panel1.Controls.Add(MedicalRecordsButton);
        splitContainer1.Panel1.Controls.Add(AppointmentsButton);
        splitContainer1.Panel1.Controls.Add(DoctorsButton);
        splitContainer1.Panel1.Controls.Add(PatientsButton);
        splitContainer1.Size = new Size(1090, 842);
        splitContainer1.SplitterDistance = 363;
        splitContainer1.TabIndex = 0;
        // 
        // MedicalRecordsButton
        // 
        MedicalRecordsButton.Location = new Point(25, 664);
        MedicalRecordsButton.Name = "MedicalRecordsButton";
        MedicalRecordsButton.Size = new Size(308, 100);
        MedicalRecordsButton.TabIndex = 3;
        MedicalRecordsButton.Text = "病例信息管理";
        MedicalRecordsButton.UseVisualStyleBackColor = true;
        MedicalRecordsButton.Click += JumpPage_Click;
        // 
        // AppointmentsButton
        // 
        AppointmentsButton.Location = new Point(25, 472);
        AppointmentsButton.Name = "AppointmentsButton";
        AppointmentsButton.Size = new Size(308, 100);
        AppointmentsButton.TabIndex = 2;
        AppointmentsButton.Text = "预约信息管理";
        AppointmentsButton.UseVisualStyleBackColor = true;
        AppointmentsButton.Click += JumpPage_Click;
        // 
        // DoctorsButton
        // 
        DoctorsButton.Location = new Point(25, 280);
        DoctorsButton.Name = "DoctorsButton";
        DoctorsButton.Size = new Size(308, 100);
        DoctorsButton.TabIndex = 1;
        DoctorsButton.Text = "医生信息管理";
        DoctorsButton.UseVisualStyleBackColor = true;
        DoctorsButton.Click += JumpPage_Click;
        // 
        // PatientsButton
        // 
        PatientsButton.Location = new Point(25, 88);
        PatientsButton.Name = "PatientsButton";
        PatientsButton.Size = new Size(308, 100);
        PatientsButton.TabIndex = 0;
        PatientsButton.Text = "患者信息管理";
        PatientsButton.UseVisualStyleBackColor = true;
        PatientsButton.Click += JumpPage_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(11F, 24F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1090, 842);
        Controls.Add(splitContainer1);
        Name = "Form1";
        Text = "导航 主页面";
        splitContainer1.Panel1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
        splitContainer1.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private SplitContainer splitContainer1;
    private Button MedicalRecordsButton;
    private Button AppointmentsButton;
    private Button DoctorsButton;
    private Button PatientsButton;
}