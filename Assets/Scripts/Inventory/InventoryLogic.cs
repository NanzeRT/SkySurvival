using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventoryItems;

namespace InventorySystem
{
    public class InventoryLogic : MonoCellHandler
    {
        [SerializeField]
        private InventoryBehaviour[] behaviours;
        [SerializeField]
        private int cellsCount = 1;
        private List<Cell> cells = new List<Cell>();
        public override List<Cell> Cells => cells;

        protected virtual void Start()
        {
            for (int i=0; i<cellsCount; i++)
            {
                AddNewCell();
            }
        }

        private void AddNewCell()
        {
            Cell cell = new Cell();
            AddCell(cell);
        }

        protected void AddCell(Cell cell)
        {
            cells.Add(cell);
            foreach (var behaviour in behaviours)
            {
                behaviour.AddCell(cell);
            }
            cell.AddInventory(this);
        }

        public void RemoveCell(Cell cell)
        {
            cells.Remove(cell);
            foreach (var behaviour in behaviours)
            {
                behaviour.RemoveCell(cell);
            }
            cell.RemoveInventory(this);
        }

        public virtual void Trash(Cell cell)
        {
            cell.Trash();
        }

        public virtual void Trash(ItemBase item) { }
    }
}
