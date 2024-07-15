using System.Collections.Generic;
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
    }

    public override bool IsComplete()
    {
        return Children().All(item => item.IsComplete());
    }

    public override List<BaseItem> Children()
    {
        var achItems = Plugin.allItems.OfType<AchievementCategoryItem>().ToList()
                             .Where(achievement => achievement.categoryRow.AchievementKind.Row == kindRow.RowId);
        return achItems.Cast<BaseItem>().ToList();
    }

    public override void Render()
    {
        if (ImGui.TreeNode("Kind: " + kindRow.Name))
        {
            foreach (var child in Children()) child.Render();

            ImGui.TreePop();
        }
    }
}
