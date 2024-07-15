using System.Collections.Generic;
using System.Linq;
using Lumina.Excel.GeneratedSheets2;

namespace AllTheThings.DataModels.Achievements;

public class AchievementCategoryItem : BaseItem
{
    public AchievementCategory categoryRow;

    public AchievementCategoryItem(AchievementCategory category) : base(category.Name)
    {
        categoryRow = category;
    }

    public override List<BaseItem> Children()
    {
        var achItems = Plugin.allItems.OfType<AchievementItem>().ToList()
                             .Where(achievement =>
                                        achievement.achievementRow.AchievementCategory.Row == categoryRow.RowId);
        return achItems.Cast<BaseItem>().ToList();
    }
}
