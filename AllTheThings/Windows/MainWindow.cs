using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets2;

namespace AllTheThings.Windows;

public class MainWindow : Window, IDisposable
{
    private Plugin Plugin;

    public static string searchValue = "";

    private Dictionary<uint, AttAchievementKind> achKind = new Dictionary<uint, AttAchievementKind>();

    public MainWindow(Plugin plugin)
        : base("My Amazing Window##With a hidden ID", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(375, 330),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };

        Plugin = plugin;
    }

    public void Dispose() { }
    public override void Draw()
    {
        if (ImGui.Button("Achievements"))
        {
            Plugin.AchievementWindow.Toggle();
            Toggle();
        }
        ImGui.InputText("Search", ref searchValue, 64);
        ImGui.BeginChild("Scrolling");
        foreach (var name in Plugin.DataManager.GetExcelSheet<Lumina.Excel.GeneratedSheets2.Orchestrion>())
        {
            ImGui.Text(name.Name);
        }
        // foreach (var (id, kind) in achKind)
        // {
        //     kind.draw();
        // }
        ImGui.EndChild();
    }

    private void drawKinds() { }

}


