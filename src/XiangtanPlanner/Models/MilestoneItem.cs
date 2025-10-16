using XiangtanPlanner.Utilities;

namespace XiangtanPlanner.Models;

public class MilestoneItem : ObservableObject
{
    private string _title = string.Empty;
    private string _target = string.Empty;
    private double _progress;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Target
    {
        get => _target;
        set => SetProperty(ref _target, value);
    }

    public double Progress
    {
        get => _progress;
        set => SetProperty(ref _progress, value);
    }
}
