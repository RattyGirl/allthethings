using System;
using System.Collections.Generic;
using Dalamud.Hooking;
using Dalamud.Plugin.Services;
using Dalamud.Utility.Signatures;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;

namespace AllTheThings.Services;

public class CompletionTaskService : IDisposable
{
    private Queue<CompletionTaskType> Tasks = [];
    private CompletionTaskType? curTask = null;

    public int TaskCount => Tasks.Count;
    public String CurrentTask => curTask.ToString();

    public CompletionTaskService()
    {
        Plugin.GameInteropProvider.InitializeFromAttributes(this);
        _receiveAchievementProgressDelegate?.Enable();
    }

    public void AddTask(CompletionTaskType task)
    {
        Plugin.Logger.Info("Added Task: " + task.ToString());
        Tasks.Enqueue(task);
    }

    public void SetupTask()
    {
        switch (curTask)
        {
            case CompletionTaskType.AchievementTask achievementTask:
                achievementTask.Setup(true);
                break;
        }
    }

    public unsafe void Update(IFramework framework)
    {
        if (curTask == null)
        {
            curTask = Tasks.Dequeue();
            Plugin.Logger.Info("Now completing: " + curTask.ToString());
            SetupTask();
        }
        else
        {
            switch (curTask)
            {
                case CompletionTaskType.AchievementTask achievementTask:
                {
                    
                    // var achInstance = FFXIVClientStructs.FFXIV.Client.Game.UI.Achievement.Instance();
                    // if (achievementTask.rowId ==
                    //     achInstance->ProgressAchievementId)
                    // {
                    //     achievementTask.Complete((achInstance->ProgressCurrent, achInstance->ProgressMax));
                    //     curTask = null;
                    // }
                    break;
                }
            }
        }
    }

    private unsafe delegate void ReceiveAchievementProgressDelegate(Achievement* self, uint id, uint current, uint max);

    [Signature("C7 81 ?? ?? ?? ?? ?? ?? ?? ?? 89 91 ?? ?? ?? ?? 44 89 81", DetourName = nameof(ReceiveAchievementProgress))]
    private readonly Hook<ReceiveAchievementProgressDelegate>? _receiveAchievementProgressDelegate;

    private unsafe void ReceiveAchievementProgress(Achievement* self, uint id, uint current, uint max)
    {
        Plugin.Logger.Info("Received Achievement" + id + ":" + current + "/" + max);
        this._receiveAchievementProgressDelegate.Original(self, id, current, max);
    }

    public void Dispose()
    {
        _receiveAchievementProgressDelegate?.Dispose();
    }
}
