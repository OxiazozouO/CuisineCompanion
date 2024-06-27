using WinFormsApp2.DatabaseModel;
using WinFormsApp2.Helper;
using WinFormsApp2.Views.Edit;

namespace WinFormsApp2.Views;

public partial class PatientsForm : Form
{
    public PatientsForm()
    {
        InitializeComponent();

        dataGridView1.AutoGenerateColumns = false;
        dataGridView1.Columns.Clear();
        dataGridView1.Columns.AddRange(
            new DataGridViewTextBoxColumn
            {
                Name = "PatientID", HeaderText = "患者ID", DataPropertyName = "PatientID", ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "PatientName", HeaderText = "患者姓名", DataPropertyName = "PatientName", ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "Gender", HeaderText = "性别", DataPropertyName = "Gender", ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "DateOfBirth", HeaderText = "出生日期", DataPropertyName = "DateOfBirth", ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "ContactNumber", HeaderText = "联系方式", DataPropertyName = "ContactNumber", ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "Email", HeaderText = "电子邮件", DataPropertyName = "Email", ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "RegDate", HeaderText = "注册日期", DataPropertyName = "RegDate", ReadOnly = true
            }
        );


        string dbName = "medical_management_system";
        string baseSelectSql =
            "SELECT PatientID, PatientName, Gender, DateOfBirth,ContactNumber,Email,RegDate FROM patients";
        var read = SqlHelper.BuildReader<PatientDTO>(
            "PatientID", nameof(PatientDTO.PatientID),
            "PatientName", nameof(PatientDTO.PatientName),
            "Gender", nameof(PatientDTO.Gender),
            "DateOfBirth", nameof(PatientDTO.DateOfBirth),
            "ContactNumber", nameof(PatientDTO.ContactNumber),
            "Email", nameof(PatientDTO.Email),
            "RegDate", nameof(PatientDTO.RegDate)
        );


        _select = new SqlManager<PatientDTO>(dbName, baseSelectSql)
            .SetReader(read);

        _findById = new SqlManager<PatientDTO>(dbName, baseSelectSql + " WHERE PatientID={0};")
            .SetReader(read);

        _likeSelect = new SqlManager<PatientDTO>(dbName,
                baseSelectSql +
                " WHERE PatientName LIKE '%{1}%' OR Gender LIKE '%{2}%' OR ContactNumber LIKE '%{4}%' OR Email LIKE '%{5}%';")
            .SetReader(read);

        _update = new SqlManager<PatientDTO>(dbName,
            "UPDATE patients p SET p.PatientName = '{0}',p.Gender = '{1}',p.DateOfBirth = '{2}',p.ContactNumber = '{3}',p.Email = '{4}' WHERE p.PatientID = '{5}';");

        _insert = new SqlManager<PatientDTO>(dbName,
            "INSERT INTO patients (PatientName, Gender, DateOfBirth, ContactNumber, Email) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');");

        _delete = new SqlManager<PatientDTO>(dbName,
            "DELETE FROM patients WHERE PatientID = {0};");
    }

    private readonly SqlManager<PatientDTO> _select;
    private readonly SqlManager<PatientDTO> _findById;
    private readonly SqlManager<PatientDTO> _likeSelect;
    private readonly SqlManager<PatientDTO> _update;
    private readonly SqlManager<PatientDTO> _insert;
    private readonly SqlManager<PatientDTO> _delete;

    public void Init(int pid = -1)
    {
        List<PatientDTO> list;
        if (pid == -1)
        {
            _select.Query(out list);
        }
        else
        {
            _findById.BuildParameters(pid);
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
        if (s is null or "")
        {
            _select.Query(out var list1);
            dataGridView1.DataSource = list1;
            dataGridView1.Refresh();
            return;
        }

        _likeSelect.BuildParameters(s, s, s, s, s, s, s);
        _likeSelect.Query(out var list2);
        dataGridView1.DataSource = list2;
    }

    /// <summary>
    /// 修改
    /// </summary>
    private void UpdateButtonClick(object sender, EventArgs e)
    {
        dataGridView1.GetSelectedIndex<PatientDTO>(out var list, out var set);

        List<PatientsEditForm> listView = new List<PatientsEditForm>(set.Count);
        listView.AddRange(set.Select(item => new PatientsEditForm(list[item], $"修改 {list[item].PatientName} 患者信息")
            .BuildRunning(p =>
            {
                _update.BuildParameters(p.PatientName, p.Gender, p.DateOfBirth, p.ContactNumber, p.Email, p.PatientID);
                _update.Execute();
                list[item] = p;
                dataGridView1.Refresh();
            })));

        listView.QueueOpen();
    }

    /// <summary>
    /// 删除
    /// </summary>
    private void DeleteButtonClick(object sender, EventArgs e)
    {
        dataGridView1.GetSelectedIndex<PatientDTO>(out var list, out var set);

        foreach (int item in set)
        {
            _delete.BuildParameters(list[item].PatientID);
            var dialogResult = list[item].PatientName.ShowWarning();
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
    /// 添加一行
    /// </summary>
    private void AddButtonClick(object sender, EventArgs e)
    {
        var list = dataGridView1.DataSource as List<PatientDTO>;
        if (list is null) return;
        new PatientsEditForm(new PatientDTO(), "添加患者")
            .BuildRunning(p =>
            {
                _insert.BuildParameters(p.PatientName, p.Gender, p.DateOfBirth, p.ContactNumber, p.Email);

                p.PatientID = (int)_insert.ExecuteInsert();
                _findById.BuildParameters(p.PatientID);
                if (!_findById.Query(out var list2)) return;

                dataGridView1.DataSource = null;
                list.Add(list2[0]);
                dataGridView1.DataSource = list;
                dataGridView1.Refresh();
            }).Show();
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