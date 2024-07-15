using System.Collections.Generic;
using System.Linq;
using Lumina.Excel.GeneratedSheets2;

namespace AllTheThings.DataModels.Quests;

public class QuestExpansionItem : BaseItem
{
    private readonly ExVersion expansionRow;

    public QuestExpansionItem(ExVersion expansion) : base(expansion.Name)
    {
        expansionRow = expansion;
    }

    public override List<BaseItem> Children()
    {
        var achItems = Plugin.allItems.OfType<QuestItem>().ToList()
                             .Where(quest =>
                                        quest.questRow.Expansion.Row == expansionRow.RowId);
        return achItems.Cast<BaseItem>().ToList();
    }
}
