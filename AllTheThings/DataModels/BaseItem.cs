using System;
using System.Collections.Generic;

namespace AllTheThings.DataModels;

public abstract class BaseItem
{

    public string ItemName { get; set; }
    public List<BaseItem> Children = [];
    public abstract bool IsComplete();

    protected BaseItem(String itemName)
    {
        ItemName = itemName;
    }

    public abstract void Render();
}
