using ImGuiNET;
using Lumina.Excel.GeneratedSheets2;

namespace AllTheThings.DataModels;

public class QuestItem : BaseItem
{
    public Quest questRow;

    public QuestItem(Quest quest) : base(quest.Name)
    {
        questRow = quest;
    }

    public override bool IsComplete()
    {
        return false;
    }

    public override void Render()
    {
        ImGui.Text(questRow.Name);
    }
}
