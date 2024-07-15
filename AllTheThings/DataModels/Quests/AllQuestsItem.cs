using System.Collections.Generic;
using System.Linq;
using ImGuiNET;

namespace AllTheThings.DataModels.Quests;

public class AllQuestsItem : BaseItem
{
    public AllQuestsItem() : base("Quests") { }

    public override float CompletionAmount()
    {
        return 0.0f;
    }

    public override List<BaseItem> Children()
    {
        var questItems = Plugin.allItems.OfType<QuestExpansionItem>().ToList();
        return questItems.Cast<BaseItem>().ToList();
    }
}
