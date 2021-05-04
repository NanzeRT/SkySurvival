using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class InventoryBehaviour : MonoBehaviour
    {
        [SerializeField]
        private GameObject grid;
        [SerializeField]
        private GameObject cellPrefab;

        public void AddCell(Cell cell)
        {
            CellBehaviour cellB = Instantiate(cellPrefab, grid.transform).GetComponent<CellBehaviour>();
            cellB.SetCell(cell);
            cell.AddBehaviour(cellB);
        }
    }
}