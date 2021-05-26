using System.Collections;
using System.Collections.Generic;
using InventoryItems;
using UnityEngine;

namespace InventorySystem
{
    public class TrashCell : Cell
    {
        private InventoryLogic inventory;
        public TrashCell(InventoryLogic inv) => inventory = inv;

        public override void RefreshAmount() 
        {
            inventory.Trash(item);
            base.RefreshAmount();
        }
    }
}