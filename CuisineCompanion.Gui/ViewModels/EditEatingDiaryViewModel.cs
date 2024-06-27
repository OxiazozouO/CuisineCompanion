using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CuisineCompanion.Helper;
using CuisineCompanion.HttpClients;
using CuisineCompanion.Models;

namespace CuisineCompanion.ViewModels;

public partial class EditEatingDiaryViewModel : ObservableObject
{
    [ObservableProperty] private EatingDiaryViewModel eatingDiary;


    #region 计算出来的变量

    [ObservableProperty] private Dictionary<DateTime, EnergyViewModel>? energiesCache;

    [ObservableProperty] private ObservableCollection<EatingDiaryAtViewModel> selectedEatingDiaries;


    [ObservableProperty] private ObservableCollection<EnergyViewModel> energies;

    [ObservableProperty] private Dictionary<string, decimal> allNutrients;

    [ObservableProperty] private ObservableCollection<NutrientContentRatioViewModel> otherNutrientContent;

    [ObservableProperty] private List<NutrientContentViewModel> proteinContent; //三大营养素模型

    #endregion

    #region 辅助变量

    [ObservableProperty] private bool isUpdate;

    [ObservableProperty] private double maxWidth;

    [ObservableProperty] private EnergyViewModel selectedItem;

    [ObservableProperty] private int proMaxCount;
    [ObservableProperty] private int nextMaxCount;
    private readonly DateTime _baseObject = DateTime.Now;


    [ObservableProperty] private decimal energy; //总能量

    [ObservableProperty] private TimeModel time;

    [ObservableProperty] private Func<DateTime, EatingDiaryAtViewModel> addTo;

    #endregion

    partial void OnEatingDiaryChanged(EatingDiaryViewModel? oldValue, EatingDiaryViewModel newValue)
    {
        EatingDiary.PropertyChanged += EatingDiaryOnPropertyChanged;
        SelectedItem = EatingDiary.SelectedItem;
        MaxWidth = 200;
        Energies = EatingDiary.Energies;
        DateTime? date = MainViewModel.Instance.MyInfo?.BirthDate;
        if (date is not null)
        {
            DateTime dateTime = (DateTime)date;
            ProMaxCount = Math.Max(0, (_baseObject - dateTime).Days);
            NextMaxCount = Math.Max(0, (dateTime.AddYears(100) - _baseObject).Days);
        }

        AdditionalEatingDiariesInfo();
    }

    private void EatingDiaryOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(EatingDiaryViewModel.Energies):
                InitEnergiesCache();
                break;
        }
    }

    private void AdditionalEatingDiariesInfo()
    {
        HashSet<KeyValuePair<ModelFlags, int>> IdFlags = EatingDiary.EatingDiaries
            .Where(diary => diary.EatingDiary.FileUrl is null || diary.EatingDiary.Name is null)
            .Select(diary => new KeyValuePair<ModelFlags, int>(diary.EatingDiary.Flag, diary.EatingDiary.TId))
            .ToHashSet();
        var req = ApiEndpoints.GetGetEatingEatingDiaryInfos(new
        {
            IdFlags = IdFlags,
        });
        if (req.Execute(out var ret))
        {
            var data = ret.Data.ToEntity<List<EatingDiaryRetInfoDTO>>();
            var mp = data.ToDictionary(
                d => (d.Flag, d.TId),
                d => (d.Name, d.FileUrl));
            foreach (var diary in EatingDiary.EatingDiaries)
            {
                (diary.EatingDiary.Name, diary.EatingDiary.FileUrl) =
                    mp.TryGetValue((diary.EatingDiary.Flag, diary.EatingDiary.TId), out var item)
                        ? item
                        : (null, null);
            }
        }
    }


    partial void OnSelectedItemChanged(EnergyViewModel? oldValue, EnergyViewModel newValue)
    {
        if (SelectedItem is null)
        {
            SelectedEatingDiaries = null;
            return;
        }

        SelectedEatingDiaries = new ObservableCollection<EatingDiaryAtViewModel>(EatingDiary.EatingDiaries
            .Where(e => e.EatingDiary.UpdateTime.Date == SelectedItem.Date.Date));
    }

    partial void OnSelectedEatingDiariesChanged(ObservableCollection<EatingDiaryAtViewModel>? oldValue,
        ObservableCollection<EatingDiaryAtViewModel> newValue)
    {
        newValue ??= new ObservableCollection<EatingDiaryAtViewModel>();
        var dic = new Dictionary<string, decimal>();
        foreach (var model in newValue)
        {
            foreach (var (key, value) in model.EatingDiary.Nutrients)
            {
                dic.TryAdd(key, 0);
                dic[key] += value;
            }
        }

        dic = dic.OrderByDescending(kv => kv.Value)
            .ToDictionary(kv => kv.Key, kv => kv.Value);

        Energy = newValue.Sum(e => e.Energy);

        AllNutrients = dic;
        OtherNutrientContent = new ObservableCollection<NutrientContentRatioViewModel>(AllNutrients.Select(i =>
            new NutrientContentRatioViewModel
            {
                Name = i.Key,
                Value = i.Value
            }));
        foreach (var model in OtherNutrientContent)
        {
            model.InitMaxValue();
        }
    }

    partial void OnIsUpdateChanged(bool oldValue, bool newValue)
    {
        if (IsUpdate)
        {
            Energies = new ObservableCollection<EnergyViewModel>();
            InitAt();
        }
        else
        {
            Energies = EatingDiary.Energies;
        }
    }

    partial void OnAddToChanged(Func<DateTime, EatingDiaryAtViewModel>? oldValue,
        Func<DateTime, EatingDiaryAtViewModel> newValue)
    {
        if (AddTo is null) return;
        Time = new TimeModel { Time = TimeSpan.Zero };
    }

    partial void OnEnergiesChanged(ObservableCollection<EnergyViewModel>? oldValue,
        ObservableCollection<EnergyViewModel> newValue)
    {
        InitEnergiesCache();
    }

    private void InitEnergiesCache()
    {
        EnergiesCache = EatingDiary.Energies.ToDictionary(i => i.Date.Date, i => i);
    }

    public void InitAt()
    {
        var time = _baseObject;
        var en = new EnergyViewModel { Date = time.Date };
        EnergyViewModel? obj = null;
        EnergiesCache?.TryGetValue(en.Date.Date, out obj);
        if (obj != null)
        {
            en.Energy = obj.Energy;
        }

        Energies.Add(en);

        EatingDiaryViewModel.InitEnergies(Energies);
    }

    public int Top(int pro)
    {
        int addLen = 10;

        var time = _baseObject;
        for (int i = 0; i < addLen; i++)
        {
            var en = new EnergyViewModel { Date = time.AddDays(pro--).Date, Energy = 0, Flag = 0 };
            EnergyViewModel? obj = null;
            EnergiesCache?.TryGetValue(en.Date.Date, out obj);
            if (obj != null)
            {
                en.Energy = obj.Energy;
            }

            Energies.Insert(0, en);
        }

        EatingDiaryViewModel.InitEnergies(Energies);

        return pro;
    }

    public int Bottom(int nex)
    {
        int addLen = 10;

        var time = _baseObject;

        for (int i = 0; i < addLen; i++)
        {
            var en = new EnergyViewModel { Date = time.AddDays(nex++).Date, Energy = 0, Flag = 0 };
            EnergyViewModel? obj = null;
            EnergiesCache?.TryGetValue(en.Date.Date, out obj);
            if (obj != null)
            {
                en.Energy = obj.Energy;
            }

            Energies.Add(en);
        }

        EatingDiaryViewModel.InitEnergies(Energies);

        return nex;
    }

    [RelayCommand]
    private void ChangeUpdate()
    {
        IsUpdate = !IsUpdate;
    }

    [RelayCommand]
    private void RemoveEatingDiaryAt(EatingDiaryAtViewModel eatingDiaryAtViewModel)
    {
        if (!MsgBoxHelper.OkCancel(
                $"是否删除时间为{eatingDiaryAtViewModel.EatingDiary.UpdateTime:yyyy-MM-dddd hh:mm:ss}的饮食记录？"))
            return;

        var req = ApiEndpoints.DeleteEatingDiary(new
        {
            MainViewModel.UserToken,
            Flag = eatingDiaryAtViewModel.EatingDiary.Flag,
            EdId = eatingDiaryAtViewModel.EatingDiary.EdId,
        });
        if (req.Execute(out var ret))
        {
            UpdateUI(eatingDiaryAtViewModel, 2);

            MsgBoxHelper.Info("删除成功！");
        }
        else
        {
            MsgBoxHelper.TryError(ret.Message);
        }
    }

    [RelayCommand]
    private void AddEatingDiaryAt()
    {
        if (AddTo is null)
        {
            MainViewModel.Navigate.GoHome();
        }
        else
        {
            if (MsgBoxHelper.TryError(Time.Error + SelectedItem.Error)) return;
            var model = AddTo(SelectedItem.Date + Time.Time);
            UpdateUI(model, 1);
            MsgBoxHelper.Info("添加成功！");
        }
    }
    
    // private async Task UpdateUIAsync(EatingDiaryAtViewModel model, int i)
    // {
    //     await Task.Run(() => { UpdateUI(model, i); });
    // }
    /// <summary>
    /// 更新ui
    /// </summary>
    /// <param name="model"></param>
    /// <param name="i">1：添加  2：删除</param>
    private void UpdateUI(EatingDiaryAtViewModel model, int i)
    {
        var item = SelectedItem;
        var item2 = SelectedEatingDiaries;
        switch (i)
        {
            case 1:
                var instance = HomeIndexViewModel._instance;
                EatingDiary.EatingDiaries.Add(model);
                SelectedEatingDiaries.Add(model);
                instance.EatingDiary.EatingDiaries.Add(model);
                
                item.Energy += model.Energy;
                var list = new List<ObservableCollection<EnergyViewModel>>() { instance.EatingDiary.Energies, EatingDiary.Energies };
                foreach (var models in list)
                {
                    var item3 = models.FirstOrDefault(e => e.Date.Date == item.Date.Date);
                    if (item3 is null)
                    {
                        models.Add(new EnergyViewModel
                        {
                            Date = item.Date,
                            Energy = model.Energy,
                            Flag = item.Flag
                        });
                    }
                    else
                    {
                        item3.Energy += model.Energy;
                    }
                }
                break;
            case 2:
                EatingDiary.EatingDiaries.Remove(model);
                SelectedEatingDiaries.Remove(model);

                EnergyViewModel? obj = null;
                var key = model.EatingDiary.UpdateTime.Date;
                EnergiesCache?.TryGetValue(key, out obj);
                if (obj is not null)
                {
                    obj.Energy -= model.Energy;
                    if (obj.Energy < 0.00000001m)
                    {
                        EnergiesCache?.Remove(key);
                        Energies.Remove(obj);
                        EatingDiaryViewModel.InitEnergies(Energies);
                    }
                }

                break;
        }

        SelectedItem = null;
        SelectedItem = item;

        SelectedEatingDiaries = null;
        SelectedEatingDiaries = item2;
    }
}

public record EatingDiaryRetInfoDTO
{
    public ModelFlags Flag { get; set; }
    public int TId { get; set; }
    public string? Name { get; set; }
    public string? FileUrl { get; set; }
}