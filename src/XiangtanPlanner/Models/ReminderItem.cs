using System;
using XiangtanPlanner.Utilities;

namespace XiangtanPlanner.Models;

public class ReminderItem : ObservableObject
{
    private string _title = string.Empty;
    private DateTime _dueDate = DateTime.Now;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public DateTime DueDate
    {
        get => _dueDate;
        set => SetProperty(ref _dueDate, value);
    }
}
