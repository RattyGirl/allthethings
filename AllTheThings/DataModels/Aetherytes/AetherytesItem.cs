using System.Collections.Generic;
using Lumina.Excel.GeneratedSheets2;

namespace AllTheThings.DataModels.Aetherytes;

public class AetherytesItem : BaseItem
{
    private Aetheryte aetheryteRow;
    public AetherytesItem(Aetheryte aetheryte) : base(aetheryte.PlaceName.Value?.Name.ToString() ?? "Unknown")
    {
        aetheryteRow = aetheryte;
    }

    public override List<BaseItem> Children()
    {
        return [];
    }
}
