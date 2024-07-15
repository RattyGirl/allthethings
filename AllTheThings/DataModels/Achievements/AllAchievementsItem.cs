using System.Collections.Generic;
using System.Linq;
using ImGuiNET;

namespace AllTheThings.DataModels.Achievements;

public class AllAchievementsItem : BaseItem
{
    public AllAchievementsItem() : base("Achievements") { }

    public override float CompletionAmount()
    {
        return 0.0f;
    }

    public override List<BaseItem> Children()
    {
        var achItems = Plugin.allItems.OfType<AchievementKindItem>().ToList();
        return achItems.Cast<BaseItem>().ToList();
    }
}
