using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Threading;
using XiangtanPlanner.Models;
using XiangtanPlanner.Utilities;

namespace XiangtanPlanner.Services;

public sealed class AutoSaveService : IDisposable
{
    private readonly PlannerState _state;
    private readonly DataService _dataService;
    private readonly DispatcherTimer _timer;
    private bool _disposed;

    private ObservableCollection<MilestoneItem>? _milestones;
    private ObservableCollection<ReminderItem>? _reminders;
    private ObservableCollection<BoardColumn>? _workColumns;
    private ObservableCollection<TimelineItem>? _timeline;
    private ObservableCollection<PlannerTask>? _tasks;
    private ObservableCollection<ReviewItem>? _reviews;

    public AutoSaveService(PlannerState state, DataService dataService)
    {
        _state = state;
        _dataService = dataService;
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timer.Tick += OnTimerTick;

        _state.PropertyChanged += OnStatePropertyChanged;
        RegisterCollections();
    }

    private void RegisterCollections()
    {
        UpdateCollection(ref _milestones, _state.MonthlyMilestones);
        UpdateCollection(ref _reminders, _state.Reminders);
        UpdateCollection(ref _workColumns, _state.WorkColumns);
        UpdateCollection(ref _timeline, _state.Timeline);
        UpdateCollection(ref _tasks, _state.Tasks);
        UpdateCollection(ref _reviews, _state.Reviews);
    }

    private void UpdateCollection<T>(ref ObservableCollection<T>? field, ObservableCollection<T> newCollection)
        where T : class
    {
        if (field is not null)
        {
            UnregisterCollection(field);
        }

        field = newCollection;
        RegisterCollection(newCollection);
    }

    private void RegisterCollection(IList collection)
    {
        if (collection is INotifyCollectionChanged notifyCollection)
        {
            notifyCollection.CollectionChanged += OnCollectionChanged;
        }

        foreach (var item in collection)
        {
            RegisterItem(item);
        }
    }

    private void UnregisterCollection(IList collection)
    {
        if (collection is INotifyCollectionChanged notifyCollection)
        {
            notifyCollection.CollectionChanged -= OnCollectionChanged;
        }

        foreach (var item in collection)
        {
            UnregisterItem(item);
        }
    }

    private void RegisterItem(object? item)
    {
        if (item is ObservableObject observable)
        {
            observable.PropertyChanged += OnObservablePropertyChanged;
        }

        if (item is BoardColumn column)
        {
            RegisterCollection(column.Cards);
        }
    }

    private void UnregisterItem(object? item)
    {
        if (item is ObservableObject observable)
        {
            observable.PropertyChanged -= OnObservablePropertyChanged;
        }

        if (item is BoardColumn column)
        {
            UnregisterCollection(column.Cards);
        }
    }

    private void OnObservablePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        ScheduleSave();
    }

    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems is not null)
        {
            foreach (var item in e.NewItems)
            {
                RegisterItem(item);
            }
        }

        if (e.OldItems is not null)
        {
            foreach (var item in e.OldItems)
            {
                UnregisterItem(item);
            }
        }

        ScheduleSave();
    }

    private void OnStatePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(PlannerState.MonthlyMilestones):
                if (_state.MonthlyMilestones is not null)
                {
                    UpdateCollection(ref _milestones, _state.MonthlyMilestones);
                }
                break;
            case nameof(PlannerState.Reminders):
                if (_state.Reminders is not null)
                {
                    UpdateCollection(ref _reminders, _state.Reminders);
                }
                break;
            case nameof(PlannerState.WorkColumns):
                if (_state.WorkColumns is not null)
                {
                    UpdateCollection(ref _workColumns, _state.WorkColumns);
                }
                break;
            case nameof(PlannerState.Timeline):
                if (_state.Timeline is not null)
                {
                    UpdateCollection(ref _timeline, _state.Timeline);
                }
                break;
            case nameof(PlannerState.Tasks):
                if (_state.Tasks is not null)
                {
                    UpdateCollection(ref _tasks, _state.Tasks);
                }
                break;
            case nameof(PlannerState.Reviews):
                if (_state.Reviews is not null)
                {
                    UpdateCollection(ref _reviews, _state.Reviews);
                }
                break;
        }

        ScheduleSave();
    }

    private void ScheduleSave()
    {
        if (_disposed)
        {
            return;
        }

        _timer.Stop();
        _timer.Start();
    }

    private async void OnTimerTick(object? sender, EventArgs e)
    {
        _timer.Stop();
        await _dataService.SaveAsync(_state).ConfigureAwait(false);
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _timer.Stop();
        _timer.Tick -= OnTimerTick;
        _state.PropertyChanged -= OnStatePropertyChanged;

        if (_milestones is not null)
        {
            UnregisterCollection(_milestones);
        }

        if (_reminders is not null)
        {
            UnregisterCollection(_reminders);
        }

        if (_workColumns is not null)
        {
            UnregisterCollection(_workColumns);
        }

        if (_timeline is not null)
        {
            UnregisterCollection(_timeline);
        }

        if (_tasks is not null)
        {
            UnregisterCollection(_tasks);
        }

        if (_reviews is not null)
        {
            UnregisterCollection(_reviews);
        }

        _disposed = true;
    }
}
