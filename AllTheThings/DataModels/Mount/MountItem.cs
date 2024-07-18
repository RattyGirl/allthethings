using System.Collections.Generic;
using AllTheThings.DataModels;
using Lumina.Excel.GeneratedSheets2;

public class MountItem : BaseItem
{
    public MountItem(Mount mount) : base(mount.Singular.ToString()) { }

    public override List<BaseItem> Children()
    {
        return [];
    }
}
