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
        GetProgress();
    }

    public float CalculateCompletion()
    {
        if (progressCurrent == null || progressMax == null)
            return 0.0f;
        if (progressMax == 0)
            return 0.0f;
        return (float)progressCurrent / (float)progressMax;
    }

    public override List<BaseItem> Children()
    {
        return [];
    }

    public unsafe void GetProgress()
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
                CompletionAmount = CalculateCompletion();
            })
        );
    }

    public override string Description()
    {
        return achievementRow.Description;
    }
}
