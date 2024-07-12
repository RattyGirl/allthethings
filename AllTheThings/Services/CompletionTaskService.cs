using System.Collections.Generic;
using Dalamud.Plugin.Services;

namespace AllTheThings.Services;

public class CompletionTaskService
{
    private Queue<CompletionTaskType> Tasks = [];
    private CompletionTaskType? curTask = null;

    public int TaskCount => Tasks.Count;

    public void AddTask(CompletionTaskType task)
    {
        Plugin.Logger.Info("Added Task: " + task.ToString());
        Tasks.Enqueue(task);
    }

    public unsafe void Update(IFramework framework)
    {
        framework.
        if (curTask == null)
        {
            curTask = Tasks.Dequeue();
            Plugin.Logger.Info("Now completing: " + curTask.ToString());
            switch (curTask)
            {
                case CompletionTaskType.AchievementTask achievementTask:
                    achievementTask.Setup(true);
                    break;
            }
        }
        else
        {
            switch (curTask)
            {
                case CompletionTaskType.AchievementTask achievementTask:
                {
                    var achInstance = FFXIVClientStructs.FFXIV.Client.Game.UI.Achievement.Instance();
                    if (achievementTask.rowId ==
                        achInstance->ProgressAchievementId)
                    {
                        achievementTask.Complete((achInstance->ProgressCurrent, achInstance->ProgressMax));
                        curTask = null;
                    }
                    break;
                }
            }
        }
        
        // CompletionTaskType curTask = Tasks.Peek();
        // switch (curTask)
        // {
        //     case CompletionTaskType.AchievementTask achTask:
        //         checkAchievement();
        //         break;
        // }
    }
}
