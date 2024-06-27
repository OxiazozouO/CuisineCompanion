﻿using System;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CuisineCompanion.Models;

public partial class TimeModel : ObservableValidator
{
    [Required(ErrorMessage = "添加饮食记录的必填项")] [NotifyDataErrorInfo] [ObservableProperty]
    private TimeSpan time;

    public string Error
    {
        get
        {
            ValidateAllProperties();
            string s = "";
            if (Time < TimeSpan.Zero || Time > new TimeSpan(23, 59, 59))
            {
                s = "时间必须在0点到23点59分之间";
            }

            return s + string.Join('\n', GetErrors());
        }
    }
}