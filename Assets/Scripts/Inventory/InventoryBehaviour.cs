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
        private List<CellBehaviour> behaviours = new List<CellBehaviour>();

        public void AddCell(Cell cell)
        {
            CellBehaviour cellB = Instantiate(cellPrefab, grid.transform).GetComponent<CellBehaviour>();
            cellB.SetCell(cell);
            cell.AddBehaviour(cellB);
            behaviours.Add(cellB);
        }

        public void RemoveCell(Cell cell)
        {
            CellBehaviour bh = behaviours.Find(bh => bh.Cell == cell);
            behaviours.Remove(bh);
            bh.Destroy();
        }
    }
}