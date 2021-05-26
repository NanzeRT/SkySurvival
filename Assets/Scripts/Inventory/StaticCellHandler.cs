using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InventorySystem
{
    public class StaticCellHandler : MonoCellHandler
    {
        [SerializeField]
        private CellBehaviour[] behaviours;

        private List<Cell> cells;
        public override List<Cell> Cells => cells;

        void Start()
        {
            cells = new List<Cell>();
            foreach (var bh in behaviours)
            {
                Cell cell = new Cell();
                bh.SetCell(cell);
                cell.AddBehaviour(bh);
                cells.Add(cell);
            }
        }
    }
}