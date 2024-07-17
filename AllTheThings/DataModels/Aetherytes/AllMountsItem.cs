using System.Collections.Generic;
using System.Linq;

namespace AllTheThings.DataModels.Aetherytes;

public class AllAetherytesItem : BaseItem
{
    public AllAetherytesItem() : base("Aetherytes") { }

    public override List<BaseItem> Children()
    {
        var mountItems = Plugin.allItems.OfType<AetherytesItem>().ToList();
        return mountItems.Cast<BaseItem>().ToList();
    }
}
