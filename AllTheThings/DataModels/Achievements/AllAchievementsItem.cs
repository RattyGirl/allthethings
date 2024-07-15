using System.Collections.Generic;
using System.Linq;

namespace AllTheThings.DataModels.Achievements;

public class AllAchievementsItem : BaseItem
{
    public AllAchievementsItem() : base("Achievements") { }

    public override List<BaseItem> Children()
    {
        var achItems = Plugin.allItems.OfType<AchievementKindItem>().ToList();
        return achItems.Cast<BaseItem>().ToList();
    }
}
