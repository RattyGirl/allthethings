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

    public override float CompletionAmount()
    {
        return 0.0f;
    }

    public override List<BaseItem> Children()
    {
        var achItems = Plugin.allItems.OfType<QuestItem>().ToList()
                             .Where(quest =>
                                        quest.questRow.Expansion.Row == expansionRow.RowId);
        return achItems.Cast<BaseItem>().ToList();
    }
}
