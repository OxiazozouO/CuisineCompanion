using WinFormsApp2.DTO;
using WinFormsApp2.Helper;

namespace WinFormsApp2.Views.Edit;

public partial class MedicalRecordsEditForm : EditForm<MedicalRecordDTO, MedicalRecordsEditForm>
{
    public MedicalRecordsEditForm(MedicalRecordDTO model, string text) : base(model, text)
    {
        InitializeComponent();
        SetEnt();
    }

    /// <summary>
    ///     model赋值到view
    /// </summary>
    public sealed override void SetEnt()
    {
        label2.Text = _model.DoctorName;
        label4.Text = _model.Specialty;
        label6.Text = _model.DoctorPhone;

        label8.Text = _model.PatientName;
        label12.Text = _model.Gender;
        label10.Text = _model.PatientBirthday.Age();
        label14.Text = _model.PatientPhone;

        richTextBox1.Text = _model.Diagnosis;
        richTextBox2.Text = _model.Treatment;
        richTextBox3.Text = _model.FollowUp;

        label16.Text = _model.RecordDate.ToString("yyyy-M-d dddd");
    }

    /// <summary>
    ///     view赋值到model
    /// </summary>
    protected override MedicalRecordDTO GetEnt()
    {
        var result = new MedicalRecordDTO
        {
            DID = _model.DID,
            PID = _model.PID,
            MID = _model.MID,
            DoctorName = _model.DoctorName,
            Specialty = _model.Specialty,
            DoctorPhone = _model.DoctorPhone,
            PatientName = _model.PatientName,
            Gender = _model.Gender,
            PatientBirthday = _model.PatientBirthday,
            PatientPhone = _model.PatientPhone,
            Diagnosis = richTextBox1.Text,
            Treatment = richTextBox2.Text,
            FollowUp = richTextBox3.Text,
            RecordDate = DateTime.Now
        };

        return result;
    }

    public override MedicalRecordsEditForm BuildRunning(Action<MedicalRecordDTO> running)
    {
        Running = running;
        return this;
    }
}