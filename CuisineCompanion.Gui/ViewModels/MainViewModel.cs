using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CuisineCompanion.Helper;
using CuisineCompanion.HttpClients;
using CuisineCompanion.Models;
using CuisineCompanion.Views;

namespace CuisineCompanion.ViewModels;

public class MainViewModel : ObservableObject
{
    public static UserTokenModel UserToken = new() { UserId = 0, UserName = "admin", Token = "admin" };


    private static MainViewModel _instance;

    private static MyInfoModel _myInfo;
    public static MainViewModel Instance => _instance ??= new MainViewModel();

    public MyInfoModel MyInfo
    {
        get
        {
            if (_myInfo is not null) return _myInfo;
            _myInfo = ApiService.GetMyAllInfo() ?? new MyInfoModel();
            InitNutritional(_myInfo.Gender, _myInfo.BirthDate.GetAge());
            return _myInfo;
        }
        set
        {
            if (value is null) return;
            InitNutritional(value.Gender, value.BirthDate.GetAge());
            SetProperty(ref _myInfo, value);
        }
    }


    public static CultureInfo I18N => new(Lang);

    public static void Init(object? res)
    {
        UserToken = res.ToEntity<UserTokenModel>();
    }

    #region 配置项

    public static string Lang { get; set; } = "zh-CN";

    private static Dictionary<string, Dictionary<string, string>>? _referenceIntakeOfNutrients;

    private static Dictionary<string, Dictionary<string, string>>? _userNutrients;

    private static Dictionary<string, double>? _EAR, _RNI, _UL;

    public static Dictionary<string, double>? EAR
    {
        get
        {
            if (_EAR is not null) return _EAR;
            InitConfig();
            return _EAR;
        }
    }

    public static Dictionary<string, double>? RNI
    {
        get
        {
            if (_RNI is not null) return _RNI;
            InitConfig();
            return _RNI;
        }
    }

    public static Dictionary<string, double>? UL
    {
        get
        {
            if (_UL is not null) return _UL;
            InitConfig();
            return _UL;
        }
    }


    private static Dictionary<string, string>? _nutritionalMap;

    public static Dictionary<string, string>? NutritionalMap
    {
        get
        {
            if (_nutritionalMap is not null) return _nutritionalMap;
            InitConfig();
            return _nutritionalMap;
        }
    }

    private static Dictionary<string, decimal>? _unit;

    public static Dictionary<string, decimal> Unit
    {
        get
        {
            if (_unit is not null) return _unit;
            InitConfig();
            return _unit;
        }
    }

    private static Dictionary<string, string>? _baseUnit;

    public static Dictionary<string, string> BaseUnit
    {
        get
        {
            if (_baseUnit is not null) return _baseUnit;
            InitConfig();
            return _baseUnit;
        }
    }

    private static Dictionary<string, string>? _unitNext;

    public static Dictionary<string, string> UnitNext
    {
        get
        {
            if (_unitNext is not null) return _unitNext;
            InitConfig();
            return _unitNext;
        }
    }

    private static Dictionary<string, string>? _unitPre;

    public static Dictionary<string, string> UnitPre
    {
        get
        {
            if (_unitPre is not null) return _unitPre;
            InitConfig();
            return _unitPre;
        }
    }

    private static Dictionary<string, Dictionary<string, string>>? _local;

    public static Dictionary<string, Dictionary<string, string>> Local
    {
        get
        {
            if (_local is not null) return _local;
            InitConfig();
            return _local;
        }
    }

    public static Dictionary<string, double>? _activityLevels;

    public static Dictionary<string, double> ActivityLevels
    {
        get
        {
            if (_activityLevels is not null) return _activityLevels;
            InitConfig();
            return _activityLevels;
        }
    }

    /// <summary>
    ///     蛋白质需要量
    /// </summary>
    public static Dictionary<bool, byte[]>? _proteinRequirement;


    public static Dictionary<bool, byte[]> ProteinRequirement
    {
        get
        {
            if (_proteinRequirement is not null) return _proteinRequirement;
            InitConfig();
            return _proteinRequirement;
        }
    }

    public static void InitConfig()
    {
        // @formatter:off
        var l = new List<string?>
        {
            _nutritionalMap             is null ? "NutrientUnits"              : "",
            _unit                       is null ? "Units"                      : "",
            _baseUnit                   is null ? "BaseUnit"                   : "",
            _unitNext                   is null ? "UnitNext"                   : "",
            _unitPre                    is null ? "UnitPre"                    : "",
            _local                      is null ? "UnitLocal"                  : "",
            _activityLevels             is null ? "ActivityLevels"             : "",
            _proteinRequirement         is null ? "ProteinRequirement"         : "",
            _referenceIntakeOfNutrients is null ? "ReferenceIntakeOfNutrients" : ""

        };
        // @formatter:on
        var reqStr = string.Join(',', l.Where(s => !string.IsNullOrEmpty(s)));

        if (string.IsNullOrEmpty(reqStr)) return;

        var req = ApiEndpoints.GetConfigs(new
        {
            UserToken,
            Key = reqStr
        });

        if (!req.Execute(out var res)) return;

        var json = res.Data?.ToString();
        var mp = json.ToEntity<Dictionary<string, string>>();
        if (mp.TryGetValue("NutrientUnits", out json))
            _nutritionalMap = json.ToEntity<Dictionary<string, string>>();

        if (mp.TryGetValue("Units", out json))
            _unit = json.ToEntity<Dictionary<string, decimal>>();

        if (mp.TryGetValue("BaseUnit", out json))
            _baseUnit = json.ToEntity<Dictionary<string, string>>();

        if (mp.TryGetValue("UnitNext", out json))
            _unitNext = json.ToEntity<Dictionary<string, string>>();

        if (mp.TryGetValue("UnitPre", out json))
            _unitPre = json.ToEntity<Dictionary<string, string>>();

        if (mp.TryGetValue("UnitLocal", out json))
            _local = json.ToEntity<Dictionary<string, Dictionary<string, string>>>();

        if (mp.TryGetValue("ActivityLevels", out json))
            _activityLevels = json.ToEntity<Dictionary<string, double>>()
                .OrderBy(kv => kv.Value)
                .ToDictionary(kv => kv.Key, kv => kv.Value);

        if (mp.TryGetValue("ProteinRequirement", out json))
            _proteinRequirement = json.ToEntity<Dictionary<bool, byte[]>>();


        if (mp.TryGetValue("ReferenceIntakeOfNutrients", out json))
        {
            _referenceIntakeOfNutrients = json.ToEntity<Dictionary<string, Dictionary<string, string>>>();
        }
    }

    public static void InitNutritional(bool gender, int age)
    {
        try
        {
            var dic = _referenceIntakeOfNutrients;
            if (dic is null) return;

            var indDic = new Dictionary<string, string>(); //营养成分的索引  <id,name>
            {
                if (!dic.TryGetValue("index", out var indMap)
                    || !indMap.TryGetValue("index", out var ind)
                    || string.IsNullOrEmpty(ind)
                   ) return;

                var indArr = ind.Split(',');
                for (var i = 0; i < indArr.Length; i++)
                {
                    indDic[i.ToString()] = indArr[i];
                }
            }

            var result = new Dictionary<string, Dictionary<string, double>>();
            foreach (var (key, value) in dic)
            {
                if (key == "index") continue;
                result[key] = new Dictionary<string, double>();
                foreach (var (s, value1) in value)
                {
                    var arr = value1.Split('|');
                    if (arr.Length != 2) return;
                    var arr2 = arr[0].Split('@');
                    gender:
                    if (arr2.Length != 2) return;
                    var values = arr2[0].Split(',');
                    var inds = arr2[1].Split(',').Select(int.Parse).ToArray();
                    if (values.Length != inds.Length) return;
                    int ageCopy = age;
                    string value2 = "";
                    equal:
                    if (gender)
                    {
                        for (var i = 0; i < inds.Length; i++)
                        {
                            if (ageCopy > inds[i])
                            {
                                ageCopy -= inds[i];
                            }
                            else
                            {
                                value2 = values[i];
                                break;
                            }
                        }

                        double value3 = string.IsNullOrEmpty(value2) ? 0 : double.Parse(value2);
                        result[key][indDic[s]] = value3;
                    }
                    else
                    {
                        gender = true;
                        if (arr[1] == "=")
                        {
                            goto equal;
                        }

                        arr2 = arr[1].Split('@');
                        goto gender;
                    }
                }
            }

            result.TryGetValue("EAR", out _EAR);
            result.TryGetValue("RNI", out _RNI);
            result.TryGetValue("UL", out _UL);
            
        }
        catch (Exception e)
        {
            MsgBoxHelper.TryError("初始化失败！\n", e.Message);
        }
    }
    
    public static bool TryGetNutritional(string key, out string? value)
    {
        value = null;
        return NutritionalMap?.TryGetValue(key, out value) ?? false;
    }

    #endregion

    #region 导航

    private static NavigateControl _navigate;

    public static NavigateControl Navigate
    {
        get
        {
            _navigate ??= new NavigateControl();
            return _navigate;
        }
        set => _navigate = value;
    }

    private static Frame _mainFrame;

    public static Frame MainFrame
    {
        get
        {
            if (_mainFrame is not null) return _mainFrame;
            var win = Application.Current.MainWindow as MainWindow;
            if (win is null)
            {
                MsgBoxHelper.TryError("主界面初始化失败");
                throw new Exception("主界面初始化失败");
            }

            _mainFrame = win.MainFrame;

            return _mainFrame;
        }
        set => _mainFrame = value;
    }

    #endregion
}