using System;
using Dalamud.Utility.Signatures;

namespace AllTheThings;

public class GameFunctions
{
    private Plugin Plugin;
    private delegate byte IsCompleteDelegate(int achievementId);

    private delegate void RequestAchievementProgressDelegate(uint id);

    [Signature("E8 ?? ?? ?? ?? 04 30 FF C3")]
    private readonly IsCompleteDelegate? _isComplete = null;

    [Signature("48 83 EC ?? C7 81 ?? ?? ?? ?? ?? ?? ?? ?? 45 33 C9")]
    private readonly RequestAchievementProgressDelegate? _requestAchievementProgress = null;

    public GameFunctions(Plugin plugin)
    {
        Plugin = plugin;
        Plugin.GameInteropProvider.InitializeFromAttributes(this);
    }

    public bool IsQuestCompleted(int achievementId) {
        if (this._isComplete == null)
            throw new InvalidOperationException("IsQuestCompleted signature wasn't found!");

        return this._isComplete(achievementId) > 0;
    }

    public void RequestAchievementProgress(uint achievementId)
    {
        if (_requestAchievementProgress == null)
            throw new InvalidOperationException("RequestAchievementProgress signature wasn't found!");
    }
}
