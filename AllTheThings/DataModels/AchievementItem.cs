using System;
using AllTheThings.Services;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets2;

namespace AllTheThings.DataModels;

public class AchievementItem : BaseItem
{
    public Achievement achievementRow;
    public uint? progressCurrent;
    public uint? progressMax;

    public AchievementItem(Achievement achievement) : base(achievement.Name)
    {
        achievementRow = achievement;
        getProgress();
    }

    public bool IsComplete => progressCurrent != null && progressCurrent == progressMax;

    public unsafe void getProgress()
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

    public override void Render()
    {
        ImGui.Text(achievementRow.Name);
    }
}
