using System;

namespace AllTheThings.DataModels;

public abstract class BaseItem
{
    public string ItemName { get; set; }
    
    protected BaseItem(String itemName)
    {
        ItemName = itemName;
    }
}
