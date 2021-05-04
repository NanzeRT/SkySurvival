using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InventorySystem
{
    public class HandsLogic : MonoBehaviour
    {
        [SerializeField]
        private CellBehaviour[] rightBs;
        [SerializeField]
        private CellBehaviour[] leftBs;

        private static HandCell selected;

        private RightHandCell right = new RightHandCell();
        private LeftHandCell left = new LeftHandCell();

        private void Start()
        {
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
        private HandCell pair;
        public void Link(HandCell cell)
        {
            pair = cell;
            cell.pair = this;
        }

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

    public class RightHandCell : HandCell
    {
        public override void InteractWithCell(Cell cell)
        {
            if (item is null && cell.Item != null)
                TakeFromCell(cell, (cell.Item.Amount + 1) / 2);
            else
                GiveToCell(cell);
        }
    }

    public class LeftHandCell : HandCell
    {
        public override void InteractWithCell(Cell cell)
        {
            if (item is null && cell.Item != null)
                TakeFromCell(cell);
            else
                GiveToCell(cell);
        }
    }
}