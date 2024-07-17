using System.Collections.Generic;
using System.Linq;
using AllTheThings.DataModels.Quests;

namespace AllTheThings.DataModels;

public class AllTheThingsItem : BaseItem
{
    public AllTheThingsItem() : base("All The Things") { }

    public override List<BaseItem> Children()
    {
        return Plugin.allItems.ToList();
    }
}
