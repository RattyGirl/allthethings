using System.Collections.Generic;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets2;

namespace AllTheThings.DataModels.Quests;

public class QuestItem : BaseItem
{
    public Quest questRow;

    public QuestItem(Quest quest) : base(quest.Name)
    {
        questRow = quest;
        //TODO getProgress
    }

    public override float CompletionAmount()
    {
        return 0.0f;
    }

    public override List<BaseItem> Children()
    {
        return [];
    }
}
