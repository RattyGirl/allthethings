using System.Linq;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets2;

namespace AllTheThings.DataModels.Achievements;

public class AchievementKindItem : BaseItem
{
    public AchievementKind kindRow;
    public AchievementKindItem(AchievementKind kind) : base(kind.Name)
    {
        kindRow = kind;
        foreach (var achievement in Plugin.DataManager.GetExcelSheet<AchievementCategory>()!.ToList()
                                          .Where(achievement => achievement.AchievementKind.Row == kind.RowId))
        {
            Children.Add(new AchievementCategoryItem(achievement));
        }
    }

    public override bool IsComplete()
    {
        return Children.All(item => item.IsComplete());
    }

    public override void Render()
    {
        if (ImGui.TreeNode("Kind: " + kindRow.Name))
        {
            foreach (var child in Children)
            {
                child.Render();
            }
            ImGui.TreePop();
        }
    }
}
