using System;
using System.Collections.ObjectModel;
using XiangtanPlanner.Utilities;

namespace XiangtanPlanner.Models;

public class PlannerState : ObservableObject
{
    private DateTime _selectedDate = DateTime.Today;
    private string _weeklyFocus = "聚焦目标";
    private string _workSummary = string.Empty;
    private double _progressPercentage = 40;

    public DateTime SelectedDate
    {
        get => _selectedDate;
        set => SetProperty(ref _selectedDate, value);
    }

    public string WeeklyFocus
    {
        get => _weeklyFocus;
        set => SetProperty(ref _weeklyFocus, value);
    }

    public string WorkSummary
    {
        get => _workSummary;
        set => SetProperty(ref _workSummary, value);
    }

    public double ProgressPercentage
    {
        get => _progressPercentage;
        set => SetProperty(ref _progressPercentage, value);
    }

    public ObservableCollection<MilestoneItem> MonthlyMilestones { get; set; } = new();

    public ObservableCollection<ReminderItem> Reminders { get; set; } = new();

    public ObservableCollection<BoardColumn> WorkColumns { get; set; } = new();

    public ObservableCollection<TimelineItem> Timeline { get; set; } = new();

    public ObservableCollection<PlannerTask> Tasks { get; set; } = new();

    public ObservableCollection<ReviewItem> Reviews { get; set; } = new();

    public static PlannerState CreateDefault()
    {
        var state = new PlannerState
        {
            WeeklyFocus = "完善同成规划应用体验",
            WorkSummary = "记录今天的成果、风险和下一步计划。",
            ProgressPercentage = 35
        };

        state.MonthlyMilestones = new ObservableCollection<MilestoneItem>
        {
            new() { Title = "二月交付目标", Target = "完成版本1.2测试", Progress = 60 },
            new() { Title = "客户需求洞察", Target = "整理用户访谈纪要", Progress = 35 }
        };

        state.Reminders = new ObservableCollection<ReminderItem>
        {
            new() { Title = "与研发同步集成风险", DueDate = DateTime.Today.AddHours(15) },
            new() { Title = "准备周会汇报材料", DueDate = DateTime.Today.AddDays(1).AddHours(10) }
        };

        state.WorkColumns = new ObservableCollection<BoardColumn>
        {
            new()
            {
                Title = "规划中",
                Cards = new ObservableCollection<BoardCard>
                {
                    new() { Title = "梳理客户核心诉求", Owner = "王磊", Description = "收集新增功能的痛点场景" },
                    new() { Title = "评估二期排期", Owner = "张敏", Description = "与研发沟通资源计划" }
                }
            },
            new()
            {
                Title = "推进中",
                Cards = new ObservableCollection<BoardCard>
                {
                    new() { Title = "体验走查", Owner = "刘婷", Description = "使用最新原型验证用户路径" }
                }
            },
            new()
            {
                Title = "已完成",
                Cards = new ObservableCollection<BoardCard>
                {
                    new() { Title = "需求评审", Owner = "李刚", Description = "确认设计稿并输出纪要" }
                }
            }
        };

        state.Timeline = new ObservableCollection<TimelineItem>
        {
            new() { Title = "09:30 项目例会", Schedule = "09:30 - 10:30", Description = "同步项目风险与资源需求" },
            new() { Title = "14:00 用户访谈", Schedule = "14:00 - 15:00", Description = "记录访谈亮点" },
            new() { Title = "21:00 收尾复盘", Schedule = "21:00 - 21:30", Description = "总结当日成果与阻碍" }
        };

        state.Tasks = new ObservableCollection<PlannerTask>
        {
            new() { Title = "更新项目计划表", Description = "同步版本节奏与里程碑", Owner = "王磊", DueDate = DateTime.Today.AddDays(1) },
            new() { Title = "整理客户反馈", Description = "分类反馈并输出结论", Owner = "刘婷", DueDate = DateTime.Today.AddDays(2) }
        };

        state.Reviews = new ObservableCollection<ReviewItem>
        {
            new() { Title = "风险识别", Reflection = "上线时间紧，需要额外测试资源。" },
            new() { Title = "亮点记录", Reflection = "用户对新仪表盘认可度高。" }
        };

        return state;
    }
}
