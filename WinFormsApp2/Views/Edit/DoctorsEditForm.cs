using WinFormsApp2.DatabaseModel;

namespace WinFormsApp2.Views.Edit;

public partial class DoctorsEditForm : EditForm<DoctorDTO, DoctorsEditForm>
{
    public DoctorsEditForm(DoctorDTO model, string text) : base(model, text)
    {
        InitializeComponent();
        SetEnt();
    }

    /// <summary>
    /// model赋值到view
    /// </summary>
    public sealed override void SetEnt()
    {
        comboBox1.DataSource = new List<string>
        {
            "心脏病学", "神经学", "肿瘤学", "内分泌学", "消化病学", "呼吸科", "肾脏病学", "血液学", "风湿病学", "感染病学",
            "皮肤科", "精神病学", "儿科学", "妇产科学", "眼科学", "耳鼻喉科学", "麻醉学", "骨科学", "泌尿外科学", "普通外科学"
        };

        textBox4.Text = _model.DoctorName;
        comboBox1.SelectedItem = _model.Specialty;
        textBox1.Text = _model.ContactNumber;
        textBox2.Text = _model.Email;
        textBox3.Text = _model.Degree;

        label2.Text = _model.DoctorID.ToString();
        label3.Text = _model.HireDate.ToString("yyyy-M-d dddd");
    }

    /// <summary>
    /// view赋值到model
    /// </summary>
    protected sealed override DoctorDTO GetEnt()
    {
        return new DoctorDTO
        {
            DoctorName = textBox4.Text,
            Specialty = (string)comboBox1.SelectedItem,
            ContactNumber = textBox1.Text,
            Email = textBox2.Text,
            Degree = textBox3.Text,
            DoctorID = _model.DoctorID,
            HireDate = _model.HireDate
        };
    }

    public override DoctorsEditForm BuildRunning(Action<DoctorDTO> running)
    {
        Running = running;
        return this;
    }
}