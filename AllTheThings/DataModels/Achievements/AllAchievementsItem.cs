using System.Collections.Generic;
using System.Linq;
using ImGuiNET;

namespace AllTheThings.DataModels.Achievements;

public class AllAchievementsItem : BaseItem
{
    public AllAchievementsItem() : base("Achievements") { }

    public override bool IsComplete()
    {
        return Children().All(item => item.IsComplete());
    }

    public override List<BaseItem> Children()
    {
        var achItems = Plugin.allItems.OfType<AchievementKindItem>().ToList();
        return achItems.Cast<BaseItem>().ToList();
    }

    public override void Render()
    {
        if (ImGui.TreeNode("Achievements"))
        {
            foreach (var child in Children()) child.Render();
            ImGui.TreePop();
        }
    }
}
