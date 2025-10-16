using XiangtanPlanner.Utilities;

namespace XiangtanPlanner.Models;

public class BoardCard : ObservableObject
{
    private string _title = string.Empty;
    private string _owner = string.Empty;
    private string _description = string.Empty;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Owner
    {
        get => _owner;
        set => SetProperty(ref _owner, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }
}
