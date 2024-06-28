using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CuisineCompanion.Common.JsonConverter;
using Newtonsoft.Json;

namespace CuisineCompanion.Models;

public partial class EatingDiaryModel : ObservableObject
{
    [ObservableProperty] private int edId;

    [ObservableProperty] private string fileUrl;
    [ObservableProperty] private ModelFlags flag;
    [ObservableProperty] private string name;
    [ObservableProperty] private int tId;

    [ObservableProperty] private DateTime updateTime;

    [JsonConverter(typeof(StringToObjectJsonConverter))]
    public Dictionary<string, decimal> Dosages { get; set; }

    [JsonConverter(typeof(StringToObjectJsonConverter))]
    public Dictionary<string, decimal> Nutrients { get; set; }
}