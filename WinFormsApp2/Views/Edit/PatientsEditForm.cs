using WinFormsApp2.DatabaseModel;

namespace WinFormsApp2.Views.Edit;

public partial class PatientsEditForm : EditForm<PatientDTO, PatientsEditForm>
{
    public PatientsEditForm(PatientDTO model, string text) : base(model, text)
    {
        InitializeComponent();
        SetEnt();
    }

    public override PatientsEditForm BuildRunning(Action<PatientDTO> running)
    {
        Running = running;
        return this;
    }

    /// <summary>
    ///     model赋值到view
    /// </summary>
    public sealed override void SetEnt()
    {
        textBox1.Text = _model.PatientName;
        textBox2.Text = _model.ContactNumber;
        comboBox1.DataSource = new List<string> { "男", "女", "无" };
        comboBox1.SelectedItem = _model.Gender;
        dateTimePicker1.Value = _model.DateOfBirth;
        textBox3.Text = _model.Email;
        label7.Text = _model.PatientID == 0 ? "" : _model.PatientID.ToString();
        label9.Text = _model.RegDate.ToString("yyyy-MM-dd dddd");
    }

    /// <summary>
    ///     view赋值到model
    /// </summary>
    protected sealed override PatientDTO GetEnt()
    {
        return new PatientDTO
        {
            PatientName = textBox1.Text,
            ContactNumber = textBox2.Text,
            Gender = (string)comboBox1.SelectedItem,
            DateOfBirth = dateTimePicker1.Value,
            Email = textBox3.Text,
            PatientID = _model.PatientID,
            RegDate = _model.RegDate
        };
    }
}