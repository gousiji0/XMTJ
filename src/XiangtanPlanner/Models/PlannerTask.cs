using System;
using XiangtanPlanner.Utilities;

namespace XiangtanPlanner.Models;

public class PlannerTask : ObservableObject
{
    private string _title = string.Empty;
    private string _description = string.Empty;
    private string _owner = string.Empty;
    private DateTime? _dueDate = DateTime.Today;
    private bool _isCompleted;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public string Owner
    {
        get => _owner;
        set => SetProperty(ref _owner, value);
    }

    public DateTime? DueDate
    {
        get => _dueDate;
        set => SetProperty(ref _dueDate, value);
    }

    public bool IsCompleted
    {
        get => _isCompleted;
        set => SetProperty(ref _isCompleted, value);
    }
}
