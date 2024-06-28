using WinFormsApp2.Views;

namespace WinFormsApp2;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void JumpPage_Click(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button == PatientsButton)
        {
            var patientsForm = new PatientsForm { Text = "患者信息管理" };
            patientsForm.Init();
            patientsForm.Show();
        }
        else if (button == DoctorsButton)
        {
            var doctorsForm = new DoctorsForm { Text = "医生信息管理" };
            doctorsForm.Init();
            doctorsForm.Show();
        }
        else if (button == AppointmentsButton)
        {
            var appointmentsForm = new AppointmentsForm { Text = "预约信息管理" };
            appointmentsForm.Init();
            appointmentsForm.Show();
        }
        else if (button == MedicalRecordsButton)
        {
            var medicalRecordsForm = new MedicalRecordsForm { Text = "病例信息管理" };
            medicalRecordsForm.Init();
            medicalRecordsForm.Show();
        }
    }
}