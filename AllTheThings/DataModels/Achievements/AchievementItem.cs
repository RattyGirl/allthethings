using System.Collections.Generic;
using AllTheThings.Services;
using ImGuiNET;
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

    public override float CompletionAmount()
    {
        if (progressCurrent == null || progressMax == null)
        {
            return 0.0f;
        }
        else if (progressMax == 0)
        {
            return 0.0f;
        } else
        {
            return ((float)progressCurrent) / ((float)progressMax);
        }
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
            })
        );
    }
}
