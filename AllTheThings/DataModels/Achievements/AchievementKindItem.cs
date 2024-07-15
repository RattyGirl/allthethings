using System.Collections.Generic;
using System.Linq;
using Lumina.Excel.GeneratedSheets2;

namespace AllTheThings.DataModels.Achievements;

public class AchievementKindItem : BaseItem
{
    public AchievementKind kindRow;

    public AchievementKindItem(AchievementKind kind) : base(kind.Name)
    {
        kindRow = kind;
    }

    public override List<BaseItem> Children()
    {
        var achItems = Plugin.allItems.OfType<AchievementCategoryItem>().ToList()
                             .Where(achievement => achievement.categoryRow.AchievementKind.Row == kindRow.RowId);
        return achItems.Cast<BaseItem>().ToList();
    }
}
