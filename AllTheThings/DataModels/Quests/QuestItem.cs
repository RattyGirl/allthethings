using System.Collections.Generic;
using Lumina.Excel.GeneratedSheets2;

namespace AllTheThings.DataModels.Quests;

public class QuestItem : BaseItem
{
    public Quest questRow;

    public QuestItem(Quest quest) : base(quest.Name)
    {
        questRow = quest;
        GetProgress();
    }

    public void GetProgress()
    {
        CompletionAmount = FFXIVClientStructs.FFXIV.Client.Game.QuestManager.IsQuestComplete(questRow.RowId) ? 1.0f : 0.0f;
    }

    public override List<BaseItem> Children()
    {
        return [];
    }
}
