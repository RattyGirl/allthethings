using System;
using System.Collections.Generic;
using System.Linq;
using Dalamud.Hooking;
using Dalamud.Plugin.Services;
using Dalamud.Utility.Signatures;
using FFXIVClientStructs.FFXIV.Client.Game.UI;

namespace AllTheThings.Services;

public class CompletionTaskService : IDisposable
{
    private const int MaxTasks = 50;

    private readonly List<CompletionTaskType> curTask = [];
    private readonly Queue<CompletionTaskType> Tasks = [];

    public CompletionTaskService()
    {
        Plugin.GameInteropProvider.InitializeFromAttributes(this);
        _receiveAchievementProgressDelegate?.Enable();
    }

    public int TaskCount => Tasks.Count;
    public String CurrentTask => curTask.Count().ToString();

    public void Dispose()
    {
        _receiveAchievementProgressDelegate?.Dispose();
    }

    public void AddTask(CompletionTaskType task)
    {
        Tasks.Enqueue(task);
    }

    public void SetupCurrentTasks()
    {
        foreach (var task in curTask) SetupTask(task);
    }

    public void SetupTask(CompletionTaskType nextTask)
    {
        switch (nextTask)
        {
            case CompletionTaskType.AchievementTask achievementTask:
                achievementTask.Setup(true);
                break;
        }
    }

    private void _AddNext()
    {
        try
        {
            var nextTask = Tasks.Dequeue();
            SetupTask(nextTask);
            curTask.Add(nextTask);
        }
        catch { }
    }

    public void Update(IFramework framework)
    {
        if (curTask.Count() < MaxTasks) _AddNext();
    }


    [Signature("C7 81 ?? ?? ?? ?? ?? ?? ?? ?? 89 91 ?? ?? ?? ?? 44 89 81",
               DetourName = nameof(ReceiveAchievementProgress))]
    private readonly Hook<ReceiveAchievementProgressDelegate>? _receiveAchievementProgressDelegate;
    private unsafe void ReceiveAchievementProgress(Achievement* self, uint id, uint current, uint max)
    {
        // Plugin.Logger.Info("Received Achievement" + id + ":" + current + "/" + max);
        
        try
        {
            var index = curTask.FindIndex(type => type.RowID == id);
            ((CompletionTaskType.AchievementTask)curTask[index]).Complete((current, max));
            curTask.RemoveAt(index);
        }
        catch { }

        _receiveAchievementProgressDelegate.Original(self, id, current, max);
    }

    private unsafe delegate void ReceiveAchievementProgressDelegate(Achievement* self, uint id, uint current, uint max);
}
