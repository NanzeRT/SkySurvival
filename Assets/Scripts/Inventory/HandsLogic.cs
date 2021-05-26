using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InventoryItems;


namespace InventorySystem
{
    public class HandsLogic : MonoBehaviour, ICellHandler
    {
        [SerializeField]
        private CellBehaviour[] rightBs;
        [SerializeField]
        private CellBehaviour[] leftBs;
        [SerializeField]
        private Slider slider;

        public List<Cell> Cells => new List<Cell> { left, right };

        private static HandCell selected;

        private RightHandCell right = new RightHandCell();
        private LeftHandCell left = new LeftHandCell();

        private void Start()
        {
            right.SetHandler(this);
            left.SetHandler(this);
            right.Link(left);
            foreach (var b in rightBs)
            {
                right.AddBehaviour(b);
                b.SetCell(right);
            }
            foreach (var b in leftBs)
            {
                left.AddBehaviour(b);
                b.SetCell(left);
            }

            if (selected is null)
                left.Select();
        }

        public void UpdateSlider()
        {
            slider.value = right.Amount / (right.Amount + left.Amount + .0001f);
        }

        public void OnSliderChange()
        {
            int summary = right.Amount + left.Amount;
            left.TakeFromCell(right);
            left.GiveToCell(right, Mathf.CeilToInt(summary * slider.value) - right.Amount);
        }

        public static void Select(HandCell cell)
        {
            if (selected != null)
                selected.Unselect();
            selected = cell;
        }

        public static void InteractWithCell(Cell cell)
        {
            selected.InteractWithCell(cell);
        }
    }

    public abstract class HandCell : Cell
    {
        protected HandsLogic handler;
        protected HandCell pair;
        public void Link(HandCell cell)
        {
            pair = cell;
            cell.pair = this;
        }

        public ItemBase HandItem => item ?? pair.item;

        public override bool CanTake(ItemBase it) => HandItem is null || it?.GetType() == HandItem.GetType();

        public bool CanTakeSingle(ItemBase it) => base.CanTake(it);

        public void SetHandler(HandsLogic h) => handler = h;

        public override void OnLeftClick()
        {
            Select();
        }

        public void Select()
        {
            HandsLogic.Select(this);

            foreach (var bh in activeBehaviors)
                if (bh is SelectableCellBehaviour sbh)
                    sbh.Select();
        }

        public void Unselect()
        {
            foreach (var bh in activeBehaviors)
                if (bh is SelectableCellBehaviour sbh)
                    sbh.Unselect();
        }

        public abstract void InteractWithCell(Cell cell);
    }

    public class LeftHandCell : HandCell
    {
        public override void InteractWithCell(Cell cell)
        {
            if ((item is null || cell.IsFull()) && CanTake(cell.Item))
                TakeFromCell(cell);
            else if (!cell.CanTake(item) && pair.CanTakeSingle(cell.Item))
                SwapItems(cell);
            else
                GiveToCell(cell);

            handler.UpdateSlider();
        }
    }

    public class RightHandCell : HandCell
    {
        public override void InteractWithCell(Cell cell)
        {
            if (item is null && pair.Item is null && cell.Item != null)
                TakeFromCell(cell, (cell.Item.Amount + 1) / 2);
            else if (!cell.CanTake(HandItem))
                return;
            else if (item != null)
            {
                int amount = this.item.Amount;
                GiveToCell(cell);
                TakeFromCell(pair, amount - (this.item is null ? 0 : this.item.Amount));
            }
            else
            {
                TakeFromCell(pair, 1);
                GiveToCell(cell);
            }

            handler.UpdateSlider();
        }
    }
}