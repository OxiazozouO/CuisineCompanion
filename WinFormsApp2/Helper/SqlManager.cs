using System.Data;
using System.Diagnostics;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace WinFormsApp2.Helper;

public class SqlManager<T>
{
    private const string ConnectionString = "server=localhost;user=root;password=123456;database={0}";
    private List<PropertyInfo>? _readerPropertyInfos;
    private List<PropertyInfo>? _propertyInfos;
    private Func<DataRow, T>? _read;
    private readonly Type _type = typeof(T);

    private readonly MySqlConnection _connection;
    private readonly MySqlCommand _command;
    private string _sql;


    public SqlManager(string dbName, string sql)
    {
        _connection = new MySqlConnection(string.Format(ConnectionString, dbName));
        _command = _connection.CreateCommand();
        _sql = sql;
        _command.CommandText = sql;
    }

    public SqlManager<T> SetReader(Func<DataRow, T> f)
    {
        _read = f;
        return this;
    }

    public SqlManager<T> BuildParameters(params object[] param)
    {
        if (param is not null && param.Length != 0)
            _command.CommandText = string.Format(_sql, param);

        return this;
    }


    public bool Query(out List<T> result)
    {
        var tab = new DataTable();
        result = new List<T>();

        try
        {
            Execute(com => { new MySqlDataAdapter(com).Fill(tab); });
            result = (from DataRow row in tab.Rows select _read(row)).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return result.Any();
    }

    public bool Execute(T obj)
    {
        if (_propertyInfos is null) return false;
        bool ret = false;
        try
        {
            Execute(com =>
            {
                for (var i = 0; i < _propertyInfos.Count; i++)
                {
                    com.Parameters.AddWithValue("@" + (char)('a' + i), _propertyInfos[i].GetValue(obj));
                }

                ret = com.ExecuteNonQuery() != -1;
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }

        return ret;
    }

    public bool Execute()
    {
        bool ret = false;
        try
        {
            Execute(com => { ret = com.ExecuteNonQuery() != -1; });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }

        return ret;
    }

    public long ExecuteInsert()
    {
        long ret = -1;
        try
        {
            Execute(com =>
            {
                com.CommandText += " SELECT LAST_INSERT_ID()";
                ret = Convert.ToInt64(com.ExecuteScalar());
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return -1;
        }

        return ret;
    }

    private void Execute(Action<MySqlCommand> run)
    {
        _connection.Open();
        run(_command);
        _connection.Close();
    }
}

public static class SqlHelper
{
    public static Func<DataRow, T> BuildReader<T>(params string[] parameters)
    {
        Debug.Assert(parameters.Length % 2 == 0, "parameters.Length%2==0");
        var readerPropertyInfos = new List<PropertyInfo>();
        for (var i = 1; i < parameters.Length; i += 2)
        {
            var s = parameters[i];
            var property = typeof(T).GetProperty(s);
            Debug.Assert(property is not null, $"未找到指定名称的字段或字段不具有 parameter 属性：{s}");
            readerPropertyInfos.Add(property);
        }

        return row =>
        {
            var obj = Activator.CreateInstance<T>();
            int ind = 0;
            foreach (var info in readerPropertyInfos)
            {
                var ww = row[parameters[ind]];
                if (ww != DBNull.Value)
                    info.SetValue(obj, ww);
                ind += 2;
            }

            return obj;
        };
    }
}