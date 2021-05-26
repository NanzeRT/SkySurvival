using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class ShortLifeCell : Cell
    {
        protected override void RemoveItem() => Destroy();
    }
}
