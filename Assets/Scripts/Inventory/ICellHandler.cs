using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventoryItems;

namespace InventorySystem
{

    
    public interface ICellHandler
    {
        public List<Cell> Cells { get; }
    }

    public abstract class CellHandler : ICellHandler
    {
        public abstract List<Cell> Cells { get; }

        public virtual void PickUpItem(ItemBase item) => CellHandlerMethods._PickUpItem(item, Cells);
    }

    public abstract class MonoCellHandler : MonoBehaviour, ICellHandler
    {
        public abstract List<Cell> Cells { get; }

        public virtual void PickUpItem(ItemBase item) => CellHandlerMethods._PickUpItem(item, Cells);
    }

    public static class CellHandlerMethods
    {
        public static void _PickUpItem(ItemBase item, List<InventorySystem.Cell> Cells)
        {
            foreach (var cell in Cells.FindAll(c => c.Item != null && c.Item.GetType() == item.GetType()))
            {
                cell.PickUpItem(item);
                if (item.Amount == 0) return;
            }
            foreach (var cell in Cells)
            {
                cell.PickUpItem(item);
                if (item.Amount == 0) return;
            }
        }
    }
}