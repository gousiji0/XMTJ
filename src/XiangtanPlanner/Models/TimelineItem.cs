using XiangtanPlanner.Utilities;

namespace XiangtanPlanner.Models;

public class TimelineItem : ObservableObject
{
    private string _title = string.Empty;
    private string _schedule = string.Empty;
    private string _description = string.Empty;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Schedule
    {
        get => _schedule;
        set => SetProperty(ref _schedule, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }
}
