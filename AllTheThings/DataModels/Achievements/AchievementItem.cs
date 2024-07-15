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

    public override bool IsComplete() => progressCurrent != null && progressCurrent == progressMax;

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

    public override void Render()
    {
        ImGui.Text((IsComplete() ? "\u2713" : "\u274c") + ":" + achievementRow.Name);
    }
}
