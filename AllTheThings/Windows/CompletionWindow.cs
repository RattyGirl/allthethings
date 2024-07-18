using System;
using System.Linq;
using AllTheThings.DataModels;
using AllTheThings.DataModels.Achievements;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace AllTheThings.Windows;

public class CompletionWindow : Window, IDisposable
{
    public static bool showComplete = true;
    private Plugin Plugin;
    private AllTheThingsItem allTheThingsItem = new AllTheThingsItem();

    public CompletionWindow(Plugin plugin) :
        base("AllTheThings##HiddenID")
    {
        Plugin = plugin;
    }

    public void Dispose() { }


    public override void Draw()
    {
        var windowSize = ImGui.GetContentRegionAvail();
        
        if (ImGui.Button(showComplete ? "Hide Complete" : "Show Complete")) showComplete = !showComplete;
        if (ImGui.Button("Reset Current Task")) Plugin.CompletionTaskService.SetupCurrentTasks();
        ImGui.SameLine();
        ImGui.Text("Completion Amount: " + Plugin.CompletionTaskService.TaskCount + ":" + Plugin.allItems.Count);
        ImGui.Text("Task: " + Plugin.CompletionTaskService.CurrentTask ?? "Nonew");

        if (ImGui.Button("Reload Achievements"))
            Plugin.allItems.ToList().ForEach(item => item.GetProgress());
        ImGui.SameLine();
        if (ImGui.Button("Calculate Completion"))
            allTheThingsItem.CalculateCompletion();

        if (ImGui.BeginTable("AllCollectables", 2))
        {
            ImGui.TableSetupColumn("", ImGuiTableColumnFlags.None);
            ImGui.TableSetupColumn("", ImGuiTableColumnFlags.WidthFixed, ImGui.CalcTextSize("100.0%").X + 20.0f);
            allTheThingsItem.Render(windowSize);
        }

        ImGui.EndTable();
    }

    public void TextAlignLeftAndRight(string leftText, string rightText, float spacing = 0.0f)
    {
        // Get the current window width
        var windowWidth = ImGui.GetContentRegionAvail().X;

        // Get the width of the left and right text
        var leftTextWidth = ImGui.CalcTextSize(leftText).X;
        var rightTextWidth = ImGui.CalcTextSize(rightText).X;

        // Set the cursor position for the left text
        ImGui.TextUnformatted(leftText);

        // Calculate the position for the right text
        var rightTextPos = windowWidth - rightTextWidth;
        if (rightTextPos > leftTextWidth + spacing)
            ImGui.SameLine(rightTextPos);
        else
            ImGui.SameLine(windowWidth - rightTextWidth);

        // Render the right text
        ImGui.TextUnformatted(rightText);
    }
}
