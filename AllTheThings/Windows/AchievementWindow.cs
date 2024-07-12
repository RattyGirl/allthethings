using System;
using System.Collections.Generic;
using System.Linq;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets2;

namespace AllTheThings.Windows;
class AttAchievementKind
{
    public AttAchievementKind(AchievementKind kind)
    {
        Kind = kind;
    }

    public uint rowId => Kind.RowId;
    public string title => Kind.Name;

    public void addCategory(ATTAchievementCategory category)
    {
        achCategory.Add(category.rowId, category);
    }

    public List<ATTAchievementCategory> categoryNames => achCategory.ToList().ConvertAll(a => a.Value);


    private AchievementKind Kind;
    public Dictionary<uint, ATTAchievementCategory> achCategory = new Dictionary<uint, ATTAchievementCategory>();
}

class ATTAchievementCategory
{
    public ATTAchievementCategory(AchievementCategory category)
    {
        Category = category;
    }

    public Dictionary<uint, ATTAchievement> achieves => achievements;

    public uint kindId => Category.AchievementKind.Value.RowId;
    public uint rowId => Category.RowId;
    public string title => Category.Name;


    private AchievementCategory Category;
    private Dictionary<uint, ATTAchievement> achievements = new Dictionary<uint, ATTAchievement>();

    public void addAchieve(ATTAchievement achieve)
    {
        achievements.Add(achieve.achievementRow.RowId, achieve);
    }
}

class ATTAchievement
{
    public uint? progressCurrent = null;
    public uint? progressMax = null;
    public Achievement achievementRow;
    public bool IsComplete => progressCurrent != null && (progressCurrent == progressMax);
    
    public unsafe ATTAchievement(Achievement achievement)
    {
        achievementRow = achievement;
    }

    public unsafe void getProgress()
    {
        var achInstance = FFXIVClientStructs.FFXIV.Client.Game.UI.Achievement.Instance();
        achInstance->RequestAchievementProgress(
            achievementRow.RowId);
    }
}

public class AchievementWindow : Window, IDisposable
{
    private Plugin Plugin;
    
    private Dictionary<uint, ATTAchievement> allAchievements = new Dictionary<uint, ATTAchievement>();

    public AchievementWindow(Plugin plugin) :
        base("Achievements##HiddenID")
    {
        Plugin = plugin;
        ReadData();
    }

    public void ReadData()
    {
        foreach (var achieve in Plugin.DataManager.GetExcelSheet<Achievement>()!.ToList())
        {
            allAchievements.Add(achieve.RowId, new ATTAchievement(achieve));
            // categories[achieve.AchievementCategory.Value.RowId].addAchieve(new ATTAchievement(achieve));
        }
    }

    private uint? currentAchievement = null;
    private bool showComplete = true;
    public unsafe override void Draw()
    {
        ImGui.Text("Completion Amount: " + allAchievements.Where(pair => pair.Value.IsComplete).Count() + "(" + allAchievements.Where(pair => pair.Value.progressCurrent != null).Count() + ")" + ":" + allAchievements.Count);
        if (ImGui.Button(showComplete ? "Hide Complete" : "Show Complete")) showComplete = !showComplete;
        if (currentAchievement != null)
        {
            if (currentAchievement ==
                FFXIVClientStructs.FFXIV.Client.Game.UI.Achievement.Instance()->ProgressAchievementId)
            {
                allAchievements[currentAchievement.Value].progressCurrent =
                    FFXIVClientStructs.FFXIV.Client.Game.UI.Achievement.Instance()->ProgressCurrent;
                allAchievements[currentAchievement.Value].progressMax =
                    FFXIVClientStructs.FFXIV.Client.Game.UI.Achievement.Instance()->ProgressMax;
                var filtered = allAchievements.First(pair => pair.Value.progressCurrent == null);
                currentAchievement = filtered.Key;
                FFXIVClientStructs.FFXIV.Client.Game.UI.Achievement.Instance()->RequestAchievementProgress(
                    currentAchievement.Value);
            }
        }
        else
        {
            try
            {
                var filtered = allAchievements.First(pair => pair.Value.progressCurrent == null);
                currentAchievement = filtered.Key;
                FFXIVClientStructs.FFXIV.Client.Game.UI.Achievement.Instance()->RequestAchievementProgress(
                    currentAchievement.Value);
            } catch {}
        }

        foreach (var ach in allAchievements)
        {
            //complete & showComplete = True
            //!complete & showComplete = True
            //complete & !showComplete = False
            //!complete & !showComplete = True
            if (showComplete || !ach.Value.IsComplete)
            {
                ImGui.Text( ach.Value.progressCurrent + ":" + ach.Value.progressMax + "--" + ach.Value.IsComplete + ":" + ach.Value.achievementRow.Name);
            }
        }
    }

    public void Dispose() { }
}
