using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InventoryItems;
using UnityEngine;

namespace InventorySystem
{
    public class MergedCellHandler : MonoCellHandler
    {
        [SerializeField]
        private GameObject[] _cellHandlers;
        private List<ICellHandler> cellHandlers;

        void Awake()
        {
            cellHandlers = _cellHandlers.Select(x => x.GetComponent<ICellHandler>()).ToList();
            cellHandlers.ForEach(x => Debug.Log(x));
        }

        public override List<Cell> Cells => cellHandlers.SelectMany(x => x.Cells).ToList();
    }
}