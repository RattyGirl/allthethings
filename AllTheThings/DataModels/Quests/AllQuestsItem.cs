using System.Collections.Generic;
using System.Linq;
using ImGuiNET;

namespace AllTheThings.DataModels.Quests;

public class AllQuestsItem : BaseItem
{
    public AllQuestsItem() : base("Quests") { }

    public override bool IsComplete()
    {
        return Children().All(item => item.IsComplete());
    }

    public override List<BaseItem> Children()
    {
        var questItems = Plugin.allItems.OfType<QuestExpansionItem>().ToList();
        return questItems.Cast<BaseItem>().ToList();
    }

    public override void Render()
    {
        if (ImGui.TreeNode("Quests"))
        {
            foreach (var child in Children()) child.Render();
            ImGui.TreePop();
        }
    }
}
