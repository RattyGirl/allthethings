using System;

namespace AllTheThings.Services;

public record CompletionTaskType
{
    public record AchievementTask(uint rowId, Action<bool> Setup, Action<(uint current, uint maximum)> Complete) : CompletionTaskType();
    private CompletionTaskType(){}
};
