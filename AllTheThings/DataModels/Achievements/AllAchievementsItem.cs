using System.Linq;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets2;

namespace AllTheThings.DataModels.Achievements;

public class AllAchievementsItem : BaseItem
{
    public AllAchievementsItem() : base("Achievements")
    {
        foreach (var achievementKind in Plugin.DataManager.GetExcelSheet<AchievementKind>()!.ToList())
            Children.Add(new AchievementKindItem(achievementKind));
    }

    public override bool IsComplete()
    {
        return Children.All(item => item.IsComplete());
    }

    public override void Render()
    {
        if (ImGui.TreeNode("Achievements"))
        {
            foreach (var child in Children)
            {
                child.Render();
            }
            ImGui.TreePop();
        }
    }
}
