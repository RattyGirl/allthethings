using System;
using System.Collections.Generic;
using AllTheThings.Services;
using Lumina.Excel.GeneratedSheets2;

namespace AllTheThings.DataModels.Achievements;

public class AchievementItem : BaseItem
{
    public Achievement achievementRow;
    public uint? progressCurrent;
    public uint? progressMax;

    public AchievementItem(Achievement achievement) : base(achievement.Name)
    {
        achievementRow = achievement;
    }

    public override String Description => achievementRow.Description;

    public override void CalculateCompletion()
    {
        if (progressCurrent == null || progressMax == null)
            CompletionAmount = 0.0f;
        else if (progressMax == 0)
            CompletionAmount = 0.0f;
        else
            CompletionAmount = (float)progressCurrent / (float)progressMax;
    }

    public override List<BaseItem> Children()
    {
        return [];
    }

    public override unsafe void GetProgress()
    {
        Plugin.CompletionTaskService.AddTask(
            new CompletionTaskType.AchievementTask(achievementRow.RowId, b =>
            {
                var achInstance = FFXIVClientStructs.FFXIV.Client.Game.UI.Achievement.Instance();
                achInstance->RequestAchievementProgress(
                    achievementRow.RowId);
            }, currentMaximum =>
            {
                progressCurrent = currentMaximum.current;
                progressMax = currentMaximum.maximum;
                CalculateCompletion();
            })
        );
    }
}
