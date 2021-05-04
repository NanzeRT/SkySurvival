using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventoryItems;

namespace InventorySystem
{
    public class InventoryLogic : MonoBehaviour
    {
        [SerializeField]
        private InventoryBehaviour[] behaviours;
        [SerializeField]
        private int cellsCount = 1;
        private List<Cell> cells = new List<Cell>();

        void Start()
        {
            for (int i=0; i<cellsCount; i++)
            {
                AddCell();
            }
        }

        private void AddCell()
        {
            Cell cell = new Cell();
            cells.Add(cell);
            foreach (var behaviour in behaviours)
            {
                behaviour.AddCell(cell);
            }
        }

        public virtual void PickUpItem(ItemBase item)
        {
            foreach (Cell cell in cells.FindAll(c => c.Item != null && c.Item.GetType() == item.GetType()))
            {
                cell.PickUpItem(item);
                if (item.Amount == 0) return;
            }
            foreach (Cell cell in cells)
            {
                cell.PickUpItem(item);
                if (item.Amount == 0) return;
            }
        }
    }
}
