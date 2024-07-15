using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using ImGuiNET;

namespace AllTheThings.DataModels;

public abstract class BaseItem
{
    protected BaseItem(String itemName)
    {
        ItemName = itemName;
    }

    public string ItemName { get; set; }
    public abstract float CompletionAmount();

    public abstract List<BaseItem> Children();

    public virtual void Render(Vector2 windowSize)
    {
        if (!Children().Any())
        {
            ImGui.TableNextColumn();
            ImGui.Text(ItemName);
            ImGui.TableNextColumn();
            ImGui.Text(CompletionAmount().ToString("0.00%"));
        }
        else
        {
            ImGui.TableNextColumn();
            if (ImGui.TreeNode(ItemName + "##" + GetType().Name))
            {
                if (ImGui.BeginTable(ItemName, 2, ImGuiTableFlags.SizingStretchSame))
                {
                    foreach (var child in Children())
                    {
                        ImGui.TableNextRow();
                        child.Render(windowSize);
                    }
                }
                ImGui.EndTable();
                ImGui.TreePop();
            }

            ImGui.TableNextColumn();
            ImGui.Text(CompletionAmount().ToString("0.00%"));
        }
    }

}
