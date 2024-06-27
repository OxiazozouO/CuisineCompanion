using System;
using System.Collections.ObjectModel;

namespace CuisineCompanion.Common;

public partial class DateTimeScrollViewer : DynamicBoundsVerticalScrollListBox
{
    public DateTimeScrollViewer()
    {
        GetTop = Top;
        GetBottom = Bottom;
        BaseObject = DateTime.Now;
        InitializeComponent();
    }

    private readonly DateTime BaseObject;

    public int Top(int pro)
    {
        var time = BaseObject;
        for (int i = 0; i < 10; i++)
        {
            DateTimes.Insert(0, time.AddDays(pro--));
        }

        return pro;
    }

    public int Bottom(int nex)
    {
        var time = BaseObject;
        for (int i = 0; i < 10; i++)
        {
            DateTimes.Add(time.AddDays(nex++));
        }

        return nex;
    }

    public ObservableCollection<DateTime> DateTimes { get; set; } = new();
}