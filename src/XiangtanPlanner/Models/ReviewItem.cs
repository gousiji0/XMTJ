using XiangtanPlanner.Utilities;

namespace XiangtanPlanner.Models;

public class ReviewItem : ObservableObject
{
    private string _title = string.Empty;
    private string _reflection = string.Empty;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Reflection
    {
        get => _reflection;
        set => SetProperty(ref _reflection, value);
    }
}
