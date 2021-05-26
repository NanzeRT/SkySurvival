using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventoryItems;

namespace InventorySystem
{
    public class GroundInventoryLogic : InventoryLogic
    {
        [SerializeField]
        private ItemDetector detector;
        [SerializeField]
        private Transform dropperTr;
        [SerializeField]
        private GameObject DropedItemPrefab;

        protected override void Start()
        {
            AddCell(new TrashCell(this));
            detector.OnItemEnter += AddCell;
            detector.OnItemExit += RemoveCell;
        }

        public void DropItem(ItemBase item)
        {
            DropedItemBehaviour bh = Instantiate(DropedItemPrefab,
                                                 dropperTr.position,
                                                 Random.rotation)
                                                 .GetComponent<DropedItemBehaviour>();
            
            ShortLifeCell cell = new ShortLifeCell();
            cell.AddBehaviour(bh);
            bh.SetCell(cell);
            cell.PickUpItem(item);
        }

        public override void Trash(Cell cell) => DropItem(cell.Item);

        public override void Trash(ItemBase item) => DropItem(item);
    }
}
