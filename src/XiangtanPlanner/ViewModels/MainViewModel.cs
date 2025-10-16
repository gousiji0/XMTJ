using System;
using System.Linq;
using System.Collections.ObjectModel;
using XiangtanPlanner.Models;
using XiangtanPlanner.Services;
using XiangtanPlanner.Utilities;

namespace XiangtanPlanner.ViewModels;

public class MainViewModel : ObservableObject, IDisposable
{
    private readonly DataService _dataService;
    private readonly AutoSaveService _autoSaveService;
    private bool _isDisposed;

    public PlannerState State { get; }

    public RelayCommand AddTaskCommand { get; }
    public RelayCommand<PlannerTask> RemoveTaskCommand { get; }
    public RelayCommand AddReviewCommand { get; }
    public RelayCommand<ReviewItem> RemoveReviewCommand { get; }
    public RelayCommand AddReminderCommand { get; }
    public RelayCommand AddWorkItemCommand { get; }
    public RelayCommand AddTimelineCommand { get; }

    public MainViewModel()
    {
        _dataService = new DataService();
        State = _dataService.LoadAsync().GetAwaiter().GetResult();
        _autoSaveService = new AutoSaveService(State, _dataService);

        AddTaskCommand = new RelayCommand(AddTask);
        RemoveTaskCommand = new RelayCommand<PlannerTask>(RemoveTask);
        AddReviewCommand = new RelayCommand(AddReview);
        RemoveReviewCommand = new RelayCommand<ReviewItem>(RemoveReview);
        AddReminderCommand = new RelayCommand(AddReminder);
        AddWorkItemCommand = new RelayCommand(AddWorkItem);
        AddTimelineCommand = new RelayCommand(AddTimeline);
    }

    private void AddTask()
    {
        State.Tasks.Add(new PlannerTask
        {
            Title = "新的任务",
            Description = "描述任务的目标与步骤",
            DueDate = DateTime.Today.AddDays(1)
        });
    }

    private void RemoveTask(PlannerTask? task)
    {
        if (task is null)
        {
            return;
        }

        State.Tasks.Remove(task);
    }

    private void AddReview()
    {
        State.Reviews.Add(new ReviewItem
        {
            Title = "复盘标题",
            Reflection = "记录今日复盘要点"
        });
    }

    private void RemoveReview(ReviewItem? item)
    {
        if (item is null)
        {
            return;
        }

        State.Reviews.Remove(item);
    }

    private void AddReminder()
    {
        State.Reminders.Add(new ReminderItem
        {
            Title = "新的提醒事项",
            DueDate = DateTime.Now.AddHours(2)
        });
    }

    private void AddWorkItem()
    {
        if (State.WorkColumns.Count == 0)
        {
            State.WorkColumns.Add(new BoardColumn { Title = "新的阶段" });
        }

        var column = State.WorkColumns.First();
        column.Cards.Add(new BoardCard
        {
            Title = "新增工作项",
            Owner = "未分配",
            Description = "补充工作内容"
        });
    }

    private void AddTimeline()
    {
        State.Timeline.Add(new TimelineItem
        {
            Title = "新增节点",
            Schedule = $"{DateTime.Now:HH:mm}",
            Description = "描述关键事项"
        });
    }

    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        _autoSaveService.Dispose();
        _isDisposed = true;
    }
}
