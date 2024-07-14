using System.Linq;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets2;

namespace AllTheThings.DataModels.Achievements;

public class AchievementCategoryItem : BaseItem
{
    public AchievementCategory categoryRow;
    public AchievementCategoryItem(AchievementCategory category) : base(category.Name)
    {
        categoryRow = category;
        foreach (var achievement in Plugin.DataManager.GetExcelSheet<Achievement>()!.ToList()
                                       .Where(achievement => achievement.AchievementCategory.Row == category.RowId))
        {
            Children.Add(new AchievementItem(achievement));
        }
    }

    public override bool IsComplete()
    {
        return Children.All(item => item.IsComplete());
    }

    public override void Render()
    {
        if (ImGui.TreeNode("Category: " + categoryRow.Name))
        {
            foreach (var child in Children)
            {
                child.Render();
            }
            ImGui.TreePop();
        }
    }
}
