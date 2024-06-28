using WinFormsApp2.DTO;
using WinFormsApp2.Helper;
using WinFormsApp2.Views.Edit;

namespace WinFormsApp2.Views;

public partial class AppointmentsForm : Form
{
    private readonly SqlManager<AppointmentDTO> _delete;
    private readonly SqlManager<AppointmentDTO> _findById;
    private readonly SqlManager<AppointmentDTO> _insert;
    private readonly SqlManager<AppointmentDTO> _likeSelect;

    private readonly SqlManager<AppointmentDTO> _select;
    private readonly SqlManager<AppointmentDTO> _update;

    public AppointmentsForm()
    {
        InitializeComponent();
        dataGridView1.Columns.Clear();
        dataGridView1.Columns.AddRange(
            new DataGridViewTextBoxColumn
            {
                Name = "DoctorName", HeaderText = "医生姓名", DataPropertyName = "DoctorName", ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "Specialty", HeaderText = "专科", DataPropertyName = "Specialty", ReadOnly = true
            },
            // new DataGridViewTextBoxColumn
            // {
            //     Name = "DoctorPhone", HeaderText = "医生联系电话", DataPropertyName = "DoctorPhone", ReadOnly = true
            // },
            new DataGridViewTextBoxColumn
            {
                Name = "PatientName", HeaderText = "病人名称", DataPropertyName = "PatientName", ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "Gender", HeaderText = "病人性别", DataPropertyName = "Gender", ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "PatientBirthday", HeaderText = "病人生日", DataPropertyName = "PatientBirthday", ReadOnly = true
            },
            // new DataGridViewTextBoxColumn
            // {
            //     Name = "PatientPhone", HeaderText = "病人电话", DataPropertyName = "PatientPhone", ReadOnly = true
            // },
            new DataGridViewTextBoxColumn
            {
                Name = "AppointmentDate", HeaderText = "预约日期", DataPropertyName = "AppointmentDate", ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "AppointmentTime", HeaderText = "预约时间", DataPropertyName = "AppointmentTime", ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "StringStatus", HeaderText = "预约状态", DataPropertyName = "StringStatus", ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "Notes", HeaderText = "备注", DataPropertyName = "Notes", ReadOnly = true
            }
        );

        var dbName = "medical_management_system";
        var baseSelectSql =
            "SELECT d.DoctorID        as did," +
            "       p.PatientID       as pid," +
            "       a.AppointmentID   as aid," +
            "       d.DoctorName      as DoctorName," +
            "       d.Specialty       as Specialty," +
            "       d.ContactNumber   as DoctorPhone," +
            "       p.PatientName     as PatientName," +
            "       p.Gender          as Gender," +
            "       p.DateOfBirth     as PatientBirthday," +
            "       p.ContactNumber   as PatientPhone," +
            "       a.AppointmentDate as AppointmentDate," +
            "       a.AppointmentTime as AppointmentTime," +
            "       a.Status          as Status," +
            "       a.Notes           as Notes " +
            "FROM appointments as a " +
            "         LEFT JOIN Doctors as d ON a.DoctorID = d.DoctorID" +
            "         LEFT JOIN patients p ON p.PatientID = a.DoctorID";


        var read = SqlHelper.BuildReader<AppointmentDTO>(
            "did", nameof(AppointmentDTO.DID),
            "pid", nameof(AppointmentDTO.PID),
            "aid", nameof(AppointmentDTO.AID),
            "DoctorName", nameof(AppointmentDTO.DoctorName),
            "Specialty", nameof(AppointmentDTO.Specialty),
            "DoctorPhone", nameof(AppointmentDTO.DoctorPhone),
            "PatientName", nameof(AppointmentDTO.PatientName),
            "Gender", nameof(AppointmentDTO.Gender),
            "PatientBirthday", nameof(AppointmentDTO.PatientBirthday),
            "PatientPhone", nameof(AppointmentDTO.PatientPhone),
            "AppointmentDate", nameof(AppointmentDTO.AppointmentDate),
            "AppointmentTime", nameof(AppointmentDTO.AppointmentTime),
            "Status", nameof(AppointmentDTO.Status),
            "Notes", nameof(AppointmentDTO.Notes)
        );


        _select = new SqlManager<AppointmentDTO>(dbName, baseSelectSql)
            .SetReader(read);

        _findById = new SqlManager<AppointmentDTO>(dbName, baseSelectSql + " WHERE a.AppointmentID ={0}")
            .SetReader(read);

        _likeSelect = new SqlManager<AppointmentDTO>(dbName,
                baseSelectSql +
                " WHERE d.DoctorName LIKE '%{0}%' OR d.Specialty LIKE '%{1}%' OR d.ContactNumber LIKE '%{2}%' OR p.PatientName LIKE '%{3}%' OR p.Gender LIKE '%{4}%' OR p.ContactNumber LIKE '%{5}%' OR a.Notes LIKE '%{6}%'")
            .SetReader(read);


        _update = new SqlManager<AppointmentDTO>(dbName,
            "UPDATE appointments t SET t.PatientID = {0}, t.DoctorID = {1}, t.AppointmentDate = '{2}', t.AppointmentTime = '{3}', t.Status = {4}, t.Notes = '{5}' WHERE t.AppointmentID = {6};");

        _insert = new SqlManager<AppointmentDTO>(dbName,
            "INSERT INTO appointments (PatientID, DoctorID, AppointmentDate, AppointmentTime, Status, Notes) VALUES ({0}, {1}, '{2}', '{3}', {4}, '{5}');");

        _delete = new SqlManager<AppointmentDTO>(dbName, "DELETE FROM appointments a WHERE a.AppointmentID = {0};");
    }

    public void Init()
    {
        _select.Query(out var list);
        dataGridView1.DataSource = list;
        dataGridView1.Refresh();
    }

    /// <summary>
    ///     查找
    /// </summary>
    private void QueryButtonClick(object sender, EventArgs e)
    {
        var s = textBox1.Text;
        List<AppointmentDTO> list;
        if (s is null or "")
        {
            _select.Query(out list);
        }
        else
        {
            _likeSelect.BuildParameters(s, s, s, s, s, s, s);
            _likeSelect.Query(out list);
        }

        dataGridView1.DataSource = list;
        dataGridView1.Refresh();
    }

    /// <summary>
    ///     添加一行
    /// </summary>
    private void AddButtonClick(object sender, EventArgs e)
    {
        var list = dataGridView1.DataSource as List<AppointmentDTO>;
        if (list is null) return;
        new AppointmentsEditForm(new AppointmentDTO(), "添加预约信息")
            .BuildRunning(a =>
            {
                _insert.BuildParameters(a.PID, a.DID, a.AppointmentDate, a.AppointmentTime, a.Status, a.Notes);

                a.AID = (int)_insert.ExecuteInsert();
                _findById.BuildParameters(a.AID);
                if (!_findById.Query(out var list2)) return;

                dataGridView1.DataSource = null;
                list.Add(list2[0]);
                dataGridView1.DataSource = list;
                dataGridView1.Refresh();
            }).Show();
    }

    /// <summary>
    ///     删除
    /// </summary>
    private void DeleteButtonClick(object sender, EventArgs e)
    {
        dataGridView1.GetSelectedIndex<AppointmentDTO>(out var list, out var set);


        foreach (var item in set)
        {
            _delete.BuildParameters(list[item].AID);
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
    ///     修改
    /// </summary>
    private void UpdateButtonClick(object sender, EventArgs e)
    {
        dataGridView1.GetSelectedIndex<AppointmentDTO>(out var list, out var set);

        var listView = new List<AppointmentsEditForm>(set.Count);
        listView.AddRange(set.Select(item => new AppointmentsEditForm(list[item], $"修改 {list[item].AID} 预约信息")
            .BuildRunning(a =>
            {
                _update.BuildParameters(a.PID, a.DID, a.AppointmentDate, a.AppointmentTime, a.Status, a.Notes, a.AID);
                _update.Execute();
                list[item] = a;
                dataGridView1.Refresh();
            })));

        listView.QueueOpen();
    }

    /// <summary>
    ///     医师详情
    /// </summary>
    private void button5_Click(object sender, EventArgs e)
    {
        dataGridView1.GetSelectedIndex<AppointmentDTO>(out var list, out var set);

        var listView = new List<DoctorsForm>(set.Count);
        foreach (var item in set)
        {
            var pf = new DoctorsForm();
            pf.Init(list[item].DID);
            listView.Add(pf);
        }

        listView.QueueOpen();
    }

    /// <summary>
    ///     病人详情
    /// </summary>
    private void button6_Click(object sender, EventArgs e)
    {
        dataGridView1.GetSelectedIndex<AppointmentDTO>(out var list, out var set);

        var listView = new List<PatientsForm>(set.Count);
        foreach (var item in set)
        {
            var pf = new PatientsForm();
            pf.Init(list[item].PID);
            listView.Add(pf);
        }

        listView.QueueOpen();
    }

    /// <summary>
    ///     双击修改
    /// </summary>
    private void DataGridView1OnDoubleClick(object? sender, EventArgs e)
    {
        UpdateButtonClick(sender, e);
    }

    /// <summary>
    ///     按下delete键删除
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