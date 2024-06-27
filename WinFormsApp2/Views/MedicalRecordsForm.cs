using WinFormsApp2.DTO;
using WinFormsApp2.Helper;
using WinFormsApp2.Views.Edit;

namespace WinFormsApp2.Views;

public partial class MedicalRecordsForm : Form
{
    public MedicalRecordsForm()
    {
        InitializeComponent();
        dataGridView1.Columns.Clear();
        dataGridView1.Columns.AddRange(
            new DataGridViewTextBoxColumn
            {
                Name = "DoctorName",
                HeaderText = "医生姓名",
                DataPropertyName = "DoctorName",
                ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "Specialty",
                HeaderText = "专科",
                DataPropertyName = "Specialty",
                ReadOnly = true
            },
            // new DataGridViewTextBoxColumn
            // {
            //     Name = "DoctorPhone",
            //     HeaderText = "医生联系电话",
            //     DataPropertyName = "DoctorPhone",
            //     ReadOnly = true
            // },
            new DataGridViewTextBoxColumn
            {
                Name = "PatientName",
                HeaderText = "病人名称",
                DataPropertyName = "PatientName",
                ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "Gender",
                HeaderText = "病人性别",
                DataPropertyName = "Gender",
                ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "PatientBirthday",
                HeaderText = "病人生日",
                DataPropertyName = "PatientBirthday",
                ReadOnly = true
            },
            // new DataGridViewTextBoxColumn
            // {
            //     Name = "PatientPhone",
            //     HeaderText = "病人电话",
            //     DataPropertyName = "PatientPhone",
            //     ReadOnly = true
            // },
            new DataGridViewTextBoxColumn
            {
                Name = "Diagnosis",
                HeaderText = "诊断",
                DataPropertyName = "Diagnosis",
                ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "Treatment",
                HeaderText = "治疗方案",
                DataPropertyName = "Treatment",
                ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "RecordDate",
                HeaderText = "记录日期",
                DataPropertyName = "RecordDate",
                ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "FollowUp",
                HeaderText = "随访情况",
                DataPropertyName = "FollowUp",
                ReadOnly = true
            }
        );

        string dbName = "medical_management_system";
        string baseSelectSql =
            "SELECT d.DoctorID      as did," +
            "       p.PatientID     as pid," +
            "       m.RecordID      as mid," +
            "       d.DoctorName    as DoctorName," +
            "       d.Specialty     as Specialty," +
            "       d.ContactNumber as DoctorPhone," +
            "       p.PatientName   as PatientName," +
            "       p.Gender        as Gender," +
            "       p.DateOfBirth   as PatientBirthday," +
            "       p.ContactNumber as PatientPhone," +
            "       m.Diagnosis     as Diagnosis," +
            "       m.Treatment     as Treatment," +
            "       m.RecordDate    as RecordDate," +
            "       m.FollowUp      as FollowUp" +
            " FROM medicalrecords as m " +
            "      LEFT JOIN Doctors as d ON m.RecordID = d.DoctorID" +
            "      LEFT JOIN patients p ON p.PatientID = m.RecordID";


        var read = SqlHelper.BuildReader<MedicalRecordDTO>(
            "did", nameof(MedicalRecordDTO.DID),
            "pid", nameof(MedicalRecordDTO.PID),
            "mid", nameof(MedicalRecordDTO.MID),
            "DoctorName", nameof(MedicalRecordDTO.DoctorName),
            "Specialty", nameof(MedicalRecordDTO.Specialty),
            "DoctorPhone", nameof(MedicalRecordDTO.DoctorPhone),
            "PatientName", nameof(MedicalRecordDTO.PatientName),
            "Gender", nameof(MedicalRecordDTO.Gender),
            "PatientBirthday", nameof(MedicalRecordDTO.PatientBirthday),
            "PatientPhone", nameof(MedicalRecordDTO.PatientPhone),
            "Diagnosis", nameof(MedicalRecordDTO.Diagnosis),
            "Treatment", nameof(MedicalRecordDTO.Treatment),
            "RecordDate", nameof(MedicalRecordDTO.RecordDate),
            "FollowUp", nameof(MedicalRecordDTO.FollowUp)
        );


        _select = new SqlManager<MedicalRecordDTO>(dbName, baseSelectSql)
            .SetReader(read);

        _findById = new SqlManager<MedicalRecordDTO>(dbName, baseSelectSql + " WHERE m.RecordID = {0}")
            .SetReader(read);

        _likeSelect = new SqlManager<MedicalRecordDTO>(dbName,
                baseSelectSql +
                " WHERE d.DoctorName LIKE '%{0}%' OR d.Specialty LIKE '%{1}%' OR d.ContactNumber LIKE '%{2}%' OR p.PatientName LIKE '%{3}%' OR p.Gender LIKE '%{4}%' OR p.ContactNumber LIKE '%{5}%' OR m.Diagnosis LIKE '%{6}%' OR m.Treatment LIKE '%{7}%' OR m.FollowUp LIKE '%{8}%'")
            .SetReader(read);


        _update = new SqlManager<MedicalRecordDTO>(dbName,
            "UPDATE medicalrecords m SET m.PatientID = {0}, m.DoctorID = {1}, m.Diagnosis = {2}, m.Treatment = {3}, m.RecordDate = {4}, m.FollowUp = {5} WHERE m.RecordID = {0};");

        _insert = new SqlManager<MedicalRecordDTO>(dbName,
            "INSERT INTO medicalrecords (PatientID, DoctorID, Diagnosis, Treatment, RecordDate, FollowUp) VALUES ({0}, {1}, '{2}', '{3}', '{4}', '{5}');");

        _delete = new SqlManager<MedicalRecordDTO>(dbName, "DELETE FROM medicalrecords WHERE RecordID = {0};");
    }

    private readonly SqlManager<MedicalRecordDTO> _select;
    private readonly SqlManager<MedicalRecordDTO> _findById;
    private readonly SqlManager<MedicalRecordDTO> _likeSelect;
    private readonly SqlManager<MedicalRecordDTO> _update;
    private readonly SqlManager<MedicalRecordDTO> _insert;
    private readonly SqlManager<MedicalRecordDTO> _delete;

    public void Init()
    {
        _select.Query(out var list);
        dataGridView1.DataSource = list;
        dataGridView1.Refresh();
    }

    /// <summary>
    /// 查找
    /// </summary>
    private void QueryButtonClick(object sender, EventArgs e)
    {
        string? s = textBox1.Text;
        List<MedicalRecordDTO> list;
        if (s is null or "")
        {
            _select.Query(out list);
        }
        else
        {
            _likeSelect.BuildParameters(s, s, s, s, s, s, s, s, s);
            _likeSelect.Query(out list);
        }

        dataGridView1.DataSource = list;
        dataGridView1.Refresh();
    }

    /// <summary>
    /// 添加一行
    /// </summary>
    private void AddButtonClick(object sender, EventArgs e)
    {
        var list = dataGridView1.DataSource as List<MedicalRecordDTO>;
        if (list is null) return;
        new MedicalRecordsEditForm(new MedicalRecordDTO(), "添加病历")
            .BuildRunning(m =>
            {
                _insert.BuildParameters(m.PID, m.DID, m.Diagnosis, m.Treatment, m.RecordDate, m.FollowUp);

                m.MID = (int)_insert.ExecuteInsert();
                _findById.BuildParameters(m.MID);
                if (!_findById.Query(out var list2)) return;

                dataGridView1.DataSource = null;
                list.Add(list2[0]);
                dataGridView1.DataSource = list;
                dataGridView1.Refresh();
            }).Show();
    }

    /// <summary>
    /// 删除
    /// </summary>
    private void DeleteButtonClick(object sender, EventArgs e)
    {
        dataGridView1.GetSelectedIndex<MedicalRecordDTO>(out var list, out var set);

        foreach (int item in set)
        {
            _delete.BuildParameters(list[item].MID);
            var dialogResult = list[item].DoctorName.ShowWarning();
            if (dialogResult == DialogResult.OK && _delete.Execute())
            {
                dataGridView1.DataSource = null;
                list.RemoveAt(item);
                dataGridView1.DataSource = list;
                dataGridView1.Refresh();
            }
        }
    }

    /// <summary>
    /// 修改
    /// </summary>
    private void UpdateButtonClick(object sender, EventArgs e)
    {
        dataGridView1.GetSelectedIndex<MedicalRecordDTO>(out var list, out var set);
        List<MedicalRecordsEditForm> listView = new List<MedicalRecordsEditForm>(set.Count);
        listView.AddRange(set.Select(item => new MedicalRecordsEditForm(list[item], $"修改 {list[item].PatientName} 病历信息")
            .BuildRunning(m =>
            {
                _update.BuildParameters(m.PID, m.DID, m.Diagnosis, m.Treatment, m.RecordDate, m.FollowUp, m.MID);
                _update.Execute();
                list[item] = m;
                dataGridView1.Refresh();
            })));

        listView.QueueOpen();
    }

    /// <summary>
    /// 医师详情
    /// </summary>
    private void button5_Click(object sender, EventArgs e)
    {
        dataGridView1.GetSelectedIndex<MedicalRecordDTO>(out var list, out var set);

        List<DoctorsForm> listView = new List<DoctorsForm>(set.Count);
        foreach (int item in set)
        {
            var df = new DoctorsForm();
            df.Init(list[item].DID);
            listView.Add(df);
        }

        listView.QueueOpen();
    }

    /// <summary>
    /// 病人详情
    /// </summary>
    private void button6_Click(object sender, EventArgs e)
    {
        dataGridView1.GetSelectedIndex<MedicalRecordDTO>(out var list, out var set);
        List<PatientsForm> listView = new List<PatientsForm>(set.Count);
        foreach (int item in set)
        {
            var pf = new PatientsForm();
            pf.Init(list[item].PID);
            listView.Add(pf);
        }

        listView.QueueOpen();
    }

    /// <summary>
    /// 双击修改
    /// </summary>
    private void DataGridView1OnDoubleClick(object? sender, EventArgs e) =>
        UpdateButtonClick(sender, e);

    /// <summary>
    /// 按下delete键删除
    /// </summary>
    private void DataGridView1OnKeyDown(object? sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Delete:
                DeleteButtonClick(sender, e);
                break;
        }
    }
}