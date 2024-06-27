using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CuisineCompanion.Views;

namespace CuisineCompanion.ViewModels;

public partial class EatingDiaryViewModel : ObservableObject
{
    [ObservableProperty] private List<EatingDiaryAtViewModel> eatingDiaries;

    #region 计算出来的变量

    [ObservableProperty] private ObservableCollection<EnergyViewModel> energies;

    [ObservableProperty] private decimal maxEnergy;

    #endregion

    #region 辅助变量

    [ObservableProperty] private double maxWidth = 0;

    [ObservableProperty] private MainViewModel mainRoot;

    #endregion

    partial void OnEnergiesChanged(ObservableCollection<EnergyViewModel>? oldValue,
        ObservableCollection<EnergyViewModel> newValue)
    {
        InitEnergies(Energies);
    }

    public EatingDiaryViewModel()
    {
        MainRoot = MainViewModel.Instance;
    }

    public static void InitEnergies(ObservableCollection<EnergyViewModel> value)
    {
        if (value.Count > 0) value[0].Flag = 3;
        for (var i = 1; i < value.Count; i++)
        {
            value[i].Flag = 0;
            if (value[i].Date.Year != value[i - 1].Date.Year)
            {
                value[i].Flag += 1;
            }

            if (value[i].Date.Month != value[i - 1].Date.Month)
            {
                value[i].Flag += 2;
            }
        }
    }

    partial void OnEatingDiariesChanged(List<EatingDiaryAtViewModel>? oldValue, List<EatingDiaryAtViewModel> newValue)
    {
        InitEnergies();
    }

    public void InitEnergies()
    {
        //按日期分组 同日期的Energy累加
        var dic = new Dictionary<DateTime, decimal>();
        foreach (var model in EatingDiaries)
        {
            var date = model.EatingDiary.UpdateTime.Date;
            dic.TryAdd(date, 0);
            dic[date] += model.Energy;
        }

        MaxEnergy = dic.Values.Count == 0 ? 0 : dic.Values.Max();
        MaxEnergy = Math.Max(MaxEnergy, (decimal)MainRoot.MyInfo.MaxTdee);
        if (MaxEnergy == 0) MaxEnergy = 100;

        EatingDiaries.Sort((l, r) => l.EatingDiary.UpdateTime.CompareTo(r.EatingDiary.UpdateTime));

        Energies = new ObservableCollection<EnergyViewModel>(dic
            .Select(x => new EnergyViewModel { Date = x.Key, Energy = x.Value, Flag = 0 })
            .OrderBy(e => e.Date));

        foreach (var diary in EatingDiaries)
        {
            diary.InitNutritional((decimal)MainRoot.MyInfo.Tdee);
        }
    }


    [ObservableProperty] private EnergyViewModel selectedItem;

    partial void OnSelectedItemChanged(EnergyViewModel? oldValue, EnergyViewModel newValue)
    {
        if (SelectedItem is not null)
        {
            var model = new EditEatingDiaryViewModel
            {
                EatingDiary = this
            };
            MainViewModel.Navigate.Navigate($"饮食日记详情",
                new EditEatingDiaryView
                {
                    DataContext = model
                }, true);
        }
    }
}