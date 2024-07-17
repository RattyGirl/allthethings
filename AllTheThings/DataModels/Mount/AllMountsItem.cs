using System.Collections.Generic;
using System.Linq;

namespace AllTheThings.DataModels.Quests;

public class AllMountsItem : BaseItem
{
    public AllMountsItem() : base("Mounts") { }

    public override List<BaseItem> Children()
    {
        var mountItems = Plugin.allItems.OfType<MountItem>().ToList();
        return mountItems.Cast<BaseItem>().ToList();
    }
}
