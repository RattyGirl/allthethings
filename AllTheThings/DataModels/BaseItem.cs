using System;
using System.Collections.Generic;

namespace AllTheThings.DataModels;

public abstract class BaseItem
{
    protected BaseItem(String itemName)
    {
        ItemName = itemName;
    }

    public string ItemName { get; set; }
    public abstract bool IsComplete();

    public abstract List<BaseItem> Children();
    public abstract void Render();
}
