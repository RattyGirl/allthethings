using System;
using System.Collections.Generic;

namespace AllTheThings.DataModels;

public abstract class BaseItem
{

    public string ItemName { get; set; }
    public List<BaseItem> Children = [];

    protected BaseItem(String itemName)
    {
        ItemName = itemName;
    }

    public void AddChild(BaseItem child)
    {
        Children.Add(child);
    }

    public abstract void Render();
}
