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

    public virtual float CompletionAmount()
    {
        return Children().Average(item => item.CompletionAmount() * 100);
    }

    public abstract List<BaseItem> Children();

    public virtual void Render(Vector2 windowSize)
    {
        if (!Children().Any())
        {
            ImGui.TableSetColumnIndex(0);
            ImGui.Text(ItemName);
            ImGui.TableSetColumnIndex(1);
            ImGui.Text(CompletionAmount().ToString("0.00%"));
        }
        else
        {
            ImGui.TableSetColumnIndex(0);
            if (ImGui.TreeNode(ItemName + "##" + GetType().Name))
            {
                foreach (var child in Children())
                {
                    ImGui.TableNextRow();
                    child.Render(windowSize);
                }
                ImGui.TreePop();
            }

            ImGui.TableSetColumnIndex(1);
            ImGui.Text(CompletionAmount().ToString("0.00%"));
        }
    }

}
