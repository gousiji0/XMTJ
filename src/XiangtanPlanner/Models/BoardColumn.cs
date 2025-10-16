using System.Collections.ObjectModel;
using XiangtanPlanner.Utilities;

namespace XiangtanPlanner.Models;

public class BoardColumn : ObservableObject
{
    private string _title = string.Empty;
    private ObservableCollection<BoardCard> _cards = new();

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public ObservableCollection<BoardCard> Cards
    {
        get => _cards;
        set => SetProperty(ref _cards, value);
    }
}
