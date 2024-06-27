using WinFormsApp2.DatabaseModel;
using WinFormsApp2.DTO;
using WinFormsApp2.Helper;

namespace WinFormsApp2.Views.Edit;

public partial class AppointmentsEditForm : EditForm<AppointmentDTO, AppointmentsEditForm>
{
    public AppointmentsEditForm(AppointmentDTO model, string text) : base(model, text)
    {
        InitializeComponent();
        SetEnt();
    }

    /// <summary>
    /// model赋值到view
    /// </summary>
    public sealed override void SetEnt()
    {
        label14.Text = _model.DoctorName;
        label11.Text = _model.Specialty;
        label12.Text = _model.DoctorPhone;
        dateTimePicker1.Value = _model.AppointmentDate;
        dateTimePicker2.Value = DateTime.Today.Add(_model.AppointmentTime);
        label13.Text = _model.PatientName;
        label15.Text = _model.Gender;
        label16.Text = _model.PatientBirthday.Age();
        label17.Text = _model.PatientPhone;
        comboBox1.DataSource = AppointmentDTO.StatusDict.Keys.ToList();
        comboBox1.SelectedItem = _model.Status;
        richTextBox1.Text = _model.Notes;
    }

    /// <summary>
    /// view赋值到model
    /// </summary>
    protected override AppointmentDTO GetEnt()
    {
        var result = new AppointmentDTO
        {
            DID = _model.DID,
            PID = _model.PID,
            AID = _model.AID,
            DoctorName = _model.DoctorName,
            Specialty = _model.Specialty,
            DoctorPhone = _model.DoctorPhone,
            PatientName = _model.PatientName,
            Gender = _model.Gender,
            PatientBirthday = _model.PatientBirthday,
            PatientPhone = _model.PatientPhone,
            AppointmentDate = dateTimePicker1.Value,
            AppointmentTime = dateTimePicker2.Value.TimeOfDay,
            Notes = richTextBox1.Text
        };
        if (AppointmentDTO.StatusDict.TryGetValue((string)comboBox1.SelectedItem, out var status))
        {
            result.Status = status;
        }

        return result;
    }


    public override AppointmentsEditForm BuildRunning(Action<AppointmentDTO> running)
    {
        Running = running;
        return this;
    }

    /// <summary>
    /// 相关医生信息
    /// </summary>
    private void Label4OnClick(object? sender, EventArgs e)
    {
        var df = new DoctorsForm();
        df.Closed += (_, _) =>
        {
            df.dataGridView1.GetSelectedIndex<DoctorDTO>(out var list, out var set);
            foreach (var p in set.Select(i => list[i]))
            {
                _model.DID = p.DoctorID;
                label14.Text = _model.DoctorName = p.DoctorName;
                label11.Text = _model.Specialty = p.Specialty;
                label12.Text = _model.DoctorPhone = p.ContactNumber;

                break;
            }
        };
        df.Init(_model.DID);
        df.Show();
    }

    /// <summary>
    /// 相关病人信息
    /// </summary>
    private void Label7OnClick(object? sender, EventArgs e)
    {
        var pf = new PatientsForm();
        pf.Closed += (_, _) =>
        {
            pf.dataGridView1.GetSelectedIndex<PatientDTO>(out var list, out var set);
            foreach (var p in set.Select(i => list[i]))
            {
                _model.PID = p.PatientID;
                label13.Text = _model.PatientName = p.PatientName;
                label15.Text = _model.Gender = p.Gender;
                _model.PatientBirthday = p.DateOfBirth;
                label16.Text = ((DateTime.Now - _model.PatientBirthday).Days / 365).ToString();
                label17.Text = _model.PatientPhone = p.ContactNumber;
                break;
            }
        };

        pf.Init(_model.PID);
        pf.Show();
    }
}