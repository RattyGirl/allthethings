using System;
using System.Collections.Generic;
using System.Linq;
using AllTheThings.DataModels;
using AllTheThings.DataModels.Achievements;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets2;

namespace AllTheThings.Windows;
public class CompletionWindow : Window, IDisposable
{
    private Plugin Plugin;
    private bool showComplete = true;

    public CompletionWindow(Plugin plugin) :
        base("AllTheThings##HiddenID")
    {
        Plugin = plugin;
    }

    public void Dispose() { }
    
    

    public override void Draw()
    {
        
        if (ImGui.Button(showComplete ? "Hide Complete" : "Show Complete")) showComplete = !showComplete;
        if (ImGui.Button("Reset Current Task"))
        {
            Plugin.CompletionTaskService.SetupTask();
        }
        ImGui.SameLine();
        ImGui.Text("Completion Amount: " + Plugin.CompletionTaskService.TaskCount + ":" + Plugin.allItems.Count);
        ImGui.Text("Task: " + Plugin.CompletionTaskService.CurrentTask ?? "Nonew");
        // foreach (var item in allAchievements)
        // {
        //     item.Value.Render();
        // }w

        foreach (var item in Plugin.allItems)
        {
            if (showComplete || !item.IsComplete())
                item.Render();
        }
    }
}
