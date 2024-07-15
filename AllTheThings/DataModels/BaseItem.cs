using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AllTheThings.Windows;
using ImGuiNET;

namespace AllTheThings.DataModels;

public abstract class BaseItem
{
    protected BaseItem(String itemName)
    {
        ItemName = itemName;
    }

    public string ItemName { get; set; }
    public float CompletionAmount { get; set; } = 0.0f;

    public virtual float CalculateCompletion()
    {
        try
        {
            Children().ForEach(item => item.CalculateCompletion());
            return Children().Average(item => item.CompletionAmount);
        }
        catch
        {
            return 1.0f;
        }
    }

    public abstract List<BaseItem> Children();

    public virtual void Render(Vector2 windowSize)
    {
        if (!Children().Any())
        {
            if(!(Math.Abs(CompletionAmount - 1.0f) < 0.01f) || CompletionWindow.showComplete)
            {
                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                ImGui.Text(ItemName);
                ImGui.TableSetColumnIndex(1);
                ImGui.Text(CompletionAmount.ToString("0.00%"));
            }
        }
        else
        {
            ImGui.TableNextRow();
            ImGui.TableSetColumnIndex(1);
            ImGui.Text(CompletionAmount.ToString("0.00%"));
            ImGui.TableSetColumnIndex(0);
            if (ImGui.TreeNode(ItemName + "##" + GetType().Name))
            {
                foreach (var child in Children())
                {
                    child.Render(windowSize);
                }

                ImGui.TreePop();
            }
        }
    }

    public virtual String Description()
    {
        return "";
    }
}
