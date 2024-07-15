using System;

namespace AllTheThings.Services;

public record CompletionTaskType
{
    public readonly uint RowID;

    private CompletionTaskType(uint rowId)
    {
        RowID = rowId;
    }

    public record AchievementTask(uint rowId, Action<bool> Setup, Action<(uint current, uint maximum)> Complete)
        : CompletionTaskType(rowId);

    public record QuestTask(uint rowId, Action<bool> Setup, Action<(uint current, uint maximum)> Complete)
        : CompletionTaskType(rowId);
}
