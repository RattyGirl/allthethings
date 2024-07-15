using System.Collections.Generic;
using System.Linq;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets2;

namespace AllTheThings.DataModels.Quests;

public class QuestExpansionItem : BaseItem
{
    ExVersion expansionRow;
    
    public QuestExpansionItem(ExVersion expansion) : base(expansion.Name)
    {
        expansionRow = expansion;
    }

    public override bool IsComplete()
    {
        return Children().All(item => item.IsComplete());
    }

    public override List<BaseItem> Children()
    {
        var achItems = Plugin.allItems.OfType<QuestItem>().ToList()
                             .Where(quest =>
                                        quest.questRow.Expansion.Row == expansionRow.RowId);
        return achItems.Cast<BaseItem>().ToList();
    }

    public override void Render()
    {
        if (ImGui.TreeNode("Expansion: " + expansionRow.Name + "##Quest"))
        {
            foreach (var child in Children()) child.Render();
            ImGui.TreePop();
        }
    }
}
