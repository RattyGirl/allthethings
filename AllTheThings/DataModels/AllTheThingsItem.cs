using System.Collections.Generic;
using System.Linq;
using AllTheThings.DataModels.Achievements;
using AllTheThings.DataModels.Aetherytes;
using AllTheThings.DataModels.Quests;

namespace AllTheThings.DataModels;

public class AllTheThingsItem : BaseItem
{
    public AllTheThingsItem() : base("All The Things") { }

    public override List<BaseItem> Children()
    {
        return Plugin.allItems.Where(item => item.GetType() == typeof(AllAchievementsItem) ||
                                             item.GetType() == typeof(AllQuestsItem) ||
                                             item.GetType() == typeof(AllMountsItem) ||
                                             item.GetType() == typeof(AllAetherytesItem)).ToList();
    }
}
