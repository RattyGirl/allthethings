using System.Collections.Generic;
using System.Linq;
using AllTheThings.DataModels;
using AllTheThings.DataModels.Achievements;
using AllTheThings.DataModels.Quests;
using AllTheThings.Services;
using AllTheThings.Windows;
using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Lumina.Excel.GeneratedSheets2;

namespace AllTheThings;

public sealed class Plugin : IDalamudPlugin
{
    private const string CommandName = "/att";
    public static CompletionTaskService CompletionTaskService;

    public static List<BaseItem> allItems = [];

    public readonly WindowSystem WindowSystem = new("AllTheThings");


    public Plugin()
    {
        PluginInterface.Inject(this);
        Configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
        GameFunctions = new GameFunctions(this);
        CompletionTaskService = new CompletionTaskService();

        RegisterWindows();

        CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
        {
            HelpMessage = "A useful message to display in /xlhelp"
        });

        PluginInterface.UiBuilder.Draw += DrawUI;

        // This adds a button to the plugin installer entry of this plugin which allows
        // to toggle the display status of the configuration ui
        PluginInterface.UiBuilder.OpenConfigUi += ToggleConfigUI;

        // Adds another button that is doing the same but for the main ui of the plugin
        PluginInterface.UiBuilder.OpenMainUi += ToggleMainUI;

        FrameworkInterface.Update += CompletionTaskService.Update;

        ReadData();

        CompletionWindow.Toggle();
    }

    [PluginService]
    internal static IFramework FrameworkInterface { get; private set; } = null!;

    [PluginService]
    internal static IDalamudPluginInterface PluginInterface { get; private set; } = null!;

    [PluginService]
    internal static ICommandManager CommandManager { get; private set; } = null!;

    [PluginService]
    internal static IDataManager DataManager { get; private set; } = null!;

    [PluginService]
    internal static IPluginLog Logger { get; private set; } = null!;

    [PluginService]
    internal static IGameInteropProvider GameInteropProvider { get; private set; } = null!;

    public Configuration Configuration { get; init; }
    internal static GameFunctions GameFunctions { get; private set; } = null!;
    internal ConfigWindow ConfigWindow { get; private set; } = null!;
    internal CompletionWindow CompletionWindow { get; private set; } = null!;

    public void Dispose()
    {
        WindowSystem.RemoveAllWindows();

        ConfigWindow.Dispose();

        CommandManager.RemoveHandler(CommandName);

        CompletionTaskService.Dispose();
    }

    public void RegisterWindows()
    {
        ConfigWindow = new ConfigWindow(this);
        CompletionWindow = new CompletionWindow(this);

        WindowSystem.AddWindow(ConfigWindow);
        WindowSystem.AddWindow(CompletionWindow);
    }

    private void ReadData()
    {
        //Achieves
        allItems.Add(new AllAchievementsItem());
        foreach (var achievementKind in DataManager.GetExcelSheet<AchievementKind>()!.ToList())
            allItems.Add(new AchievementKindItem(achievementKind));
        foreach (var achievementCategory in DataManager.GetExcelSheet<AchievementCategory>()!.ToList())
            allItems.Add(new AchievementCategoryItem(achievementCategory));
        foreach (var achievement in DataManager.GetExcelSheet<Achievement>()!.ToList())
            allItems.Add(new AchievementItem(achievement));
        
        //Quests
        allItems.Add(new AllQuestsItem());
        
        foreach (var quest in DataManager.GetExcelSheet<Quest>()!.ToList())
            allItems.Add(new QuestItem(quest));
        foreach (var expansion in DataManager.GetExcelSheet<ExVersion>()!.ToList())
            allItems.Add(new QuestExpansionItem(expansion));
    }

    private void OnCommand(string command, string args)
    {
        // in response to the slash command, just toggle the display status of our main ui
        ToggleMainUI();
    }

    private void DrawUI()
    {
        WindowSystem.Draw();
    }

    public void ToggleConfigUI()
    {
        ConfigWindow.Toggle();
    }

    public void ToggleMainUI()
    {
        CompletionWindow.Toggle();
    }
}
