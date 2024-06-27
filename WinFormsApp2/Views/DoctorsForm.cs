using WinFormsApp2.DatabaseModel;
using WinFormsApp2.Helper;
using WinFormsApp2.Views.Edit;

namespace WinFormsApp2.Views;

public partial class DoctorsForm : Form
{
    public DoctorsForm()
    {
        InitializeComponent();
        dataGridView1.Columns.Clear();
        dataGridView1.Columns.AddRange(
            new DataGridViewTextBoxColumn
            {
                Name = "DoctorID", HeaderText = "医生ID", DataPropertyName = "DoctorID", ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "DoctorName", HeaderText = "医生姓名", DataPropertyName = "DoctorName", ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "Specialty", HeaderText = "专科", DataPropertyName = "Specialty", ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "ContactNumber", HeaderText = "联系电话", DataPropertyName = "ContactNumber", ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "Email", HeaderText = "电子邮件", DataPropertyName = "Email", ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "HireDate", HeaderText = "入职时间", DataPropertyName = "HireDate", ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "Degree", HeaderText = "学位", DataPropertyName = "Degree", ReadOnly = true
            }
        );

        string dbName = "medical_management_system";
        string baseSelectSql =
            "SELECT DoctorID, DoctorName, Specialty, ContactNumber, Email, HireDate, Degree FROM doctors";
        var read = SqlHelper.BuildReader<DoctorDTO>(
            "DoctorID", nameof(DoctorDTO.DoctorID),
            "DoctorName", nameof(DoctorDTO.DoctorName),
            "Specialty", nameof(DoctorDTO.Specialty),
            "ContactNumber", nameof(DoctorDTO.ContactNumber),
            "Email", nameof(DoctorDTO.Email),
            "HireDate", nameof(DoctorDTO.HireDate),
            "Degree", nameof(DoctorDTO.Degree)
        );


        _select = new SqlManager<DoctorDTO>(dbName, baseSelectSql)
            .SetReader(read);
        _findById = new SqlManager<DoctorDTO>(dbName, baseSelectSql + " WHERE DoctorID = {0}")
            .SetReader(read);
        _likeSelect = new SqlManager<DoctorDTO>(dbName,
                baseSelectSql +
                " WHERE DoctorName LIKE '%{1}%' OR Specialty LIKE '%{2}%' OR ContactNumber LIKE '%{4}%' OR Email LIKE '%{5}%' OR Degree LIKE '%{6}%';")
            .SetReader(read);

        _update = new SqlManager<DoctorDTO>(dbName,
            "UPDATE doctors d SET d.DoctorName = '{0}',d.Specialty = '{1}',d.ContactNumber = '{2}',d.Email = '{3}',d.Degree = '{4}' WHERE d.DoctorID = '{5}';");

        _insert = new SqlManager<DoctorDTO>(dbName,
            "INSERT INTO doctors (DoctorName, Specialty, ContactNumber, Email, Degree) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');");

        _delete = new SqlManager<DoctorDTO>(dbName, "DELETE FROM doctors WHERE DoctorID = {0};");
    }

    private readonly SqlManager<DoctorDTO> _select;
    private readonly SqlManager<DoctorDTO> _findById;
    private readonly SqlManager<DoctorDTO> _likeSelect;
    private readonly SqlManager<DoctorDTO> _update;
    private readonly SqlManager<DoctorDTO> _insert;
    private readonly SqlManager<DoctorDTO> _delete;

    public void Init(int did = -1)
    {
        List<DoctorDTO> list;
        if (did == -1)
        {
            _select.Query(out list);
        }
        else
        {
            _findById.BuildParameters(did);
            _findById.Query(out list);
        }

        dataGridView1.DataSource = list;
        dataGridView1.Refresh();
    }

    /// <summary>
    /// 查找
    /// </summary>
    private void QueryButtonClick(object sender, EventArgs e)
    {
        string? s = textBox1.Text;
        List<DoctorDTO> list;
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
    /// 添加一行
    /// </summary>
    private void AddButtonClick(object sender, EventArgs e)
    {
        var list = dataGridView1.DataSource as List<DoctorDTO>;
        if (list is null) return;
        new DoctorsEditForm(new DoctorDTO(), "添加医生信息")
            .BuildRunning(d =>
            {
                _insert.BuildParameters(d.DoctorName, d.Specialty, d.ContactNumber, d.Email, d.Degree);

                d.DoctorID = (int)_insert.ExecuteInsert();
                _findById.BuildParameters(d.DoctorID);
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
        dataGridView1.GetSelectedIndex<DoctorDTO>(out var list, out var set);

        foreach (int item in set)
        {
            _delete.BuildParameters(list[item].DoctorID);
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
        dataGridView1.GetSelectedIndex<DoctorDTO>(out var list, out var set);

        List<DoctorsEditForm> listView = new List<DoctorsEditForm>(set.Count);
        listView.AddRange(set.Select(item => new DoctorsEditForm(list[item], $"修改 {list[item].DoctorName} 医生信息")
            .BuildRunning(d =>
            {
                _update.BuildParameters(d.DoctorName, d.Specialty, d.ContactNumber, d.Email, d.Degree, d.DoctorID);
                _update.Execute();
                list[item] = d;
                dataGridView1.Refresh();
            })));

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