using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AllTheThings.Windows;
using Dalamud.Interface;
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

    public virtual String Description => "";

    public virtual void CalculateCompletion()
    {
        try
        {
            Children().ForEach(item => item.CalculateCompletion());
            CompletionAmount = Children().Average(item => item.CompletionAmount);
        }
        catch
        {
            CompletionAmount = 1.0f;
        }
    }
    
    public virtual int ChildrenAmount()
    {
        return !Children().Any() ? 1 : Children().Sum(item => item.ChildrenAmount());
    }

    public abstract List<BaseItem> Children();

    public virtual void Render(Vector2 windowSize)
    {
        if (!Children().Any())
        {
            if (!(Math.Abs(CompletionAmount - 1.0f) < 0.01f) || CompletionWindow.showComplete)
            {
                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                ImGui.Text(ItemName);
                ImGui.TableSetColumnIndex(1);
                String completionText = CompletionAmount.ToString("0.00%");
                float cellWidth = ImGui.GetColumnWidth();
                float textWidth = ImGui.CalcTextSize(completionText).X;
                float padding = 0.0f;
                ImGui.SetCursorPosX(ImGui.GetCursorPosX() + cellWidth - textWidth - padding);
                ImGui.Text(completionText);
            }
        }
        else
        {
            if(!(Math.Abs(CompletionAmount - 1.0f) < 0.01f) || CompletionWindow.showComplete)
            {
                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(1);
                String completionText = CompletionAmount.ToString("0.00%");
                float cellWidth = ImGui.GetColumnWidth();
                float textWidth = ImGui.CalcTextSize(completionText).X;
                float padding = 0.0f;
                ImGui.SetCursorPosX(ImGui.GetCursorPosX() + cellWidth - textWidth - padding);
                ImGui.Text(completionText);
                ImGui.TableSetColumnIndex(0);
                if (ImGui.TreeNode(ItemName + "##" + GetType().Name))
                {
                    foreach (var child in Children().OrderBy(item => item.CompletionAmount).Reverse())
                        child.Render(windowSize);

                    ImGui.TreePop();
                }
            }
        }
    }
}
