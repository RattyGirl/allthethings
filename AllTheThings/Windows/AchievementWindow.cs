using System;
using System.Collections.Generic;
using System.Linq;
using AllTheThings.DataModels;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets2;

namespace AllTheThings.Windows;
public class AchievementWindow : Window, IDisposable
{
    private readonly Dictionary<uint, BaseItem> allAchievements = new();

    private Plugin Plugin;
    private bool showComplete = true;

    public AchievementWindow(Plugin plugin) :
        base("Achievements##HiddenID")
    {
        Plugin = plugin;
        ReadData();
    }

    public void Dispose() { }

    public void ReadData()
    {
        foreach (var achieve in Plugin.DataManager.GetExcelSheet<Achievement>()!.ToList())
            allAchievements.Add(achieve.RowId, new AchievementItem(achieve));
    }

    public override unsafe void Draw()
    {
        
        if (ImGui.Button(showComplete ? "Hide Complete" : "Show Complete")) showComplete = !showComplete;
        ImGui.Text("Completion Amount: " + Plugin.CompletionTaskService.TaskCount + ":" + allAchievements.Count);
        // foreach (var item in allAchievements)
        // {
        //     item.Value.Render();
        // }

        foreach (var ach in allAchievements)
        {
            AchievementItem item = (AchievementItem) ach.Value;
        //     //complete & showComplete = True
        //     //!complete & showComplete = True
        //     //complete & !showComplete = False
        //     //!complete & !showComplete = True
            if (showComplete || !item.IsComplete)
                ImGui.Text(item.progressCurrent + ":" + item.progressMax + "--" + item.IsComplete + ":" +
                           item.achievementRow.Name);
        }
    }
}
