using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventoryItems;


namespace InventorySystem
{
    public class Cell
    {
        protected ItemBase item;

        public ItemBase Item => item;
        public int Amount => item is null? 0 : item.Amount;

        protected List<CellBehaviour> activeBehaviors = new List<CellBehaviour>();
        public void AddBehaviour(CellBehaviour bh) => activeBehaviors.Add(bh);
        /*
                // trying to give item to other cell
                public virtual bool GiveItem(Cell cell)
                {
                    if (item is null) return false;
                    if (cell.TrySetItem(item))
                    {
                        RemoveItem();
                        return true;
                    }
                    return false;
                }

                // try set item in this cell
                protected virtual bool TrySetItem(ItemBase it)
                {
                    if (item is null)
                    {
                        SetItem(it);
                        return true;
                    }
                    return false;
                }
        */
        public virtual void PickUpItem(ItemBase it) => TakeFromItem(it);
        /*
                public virtual ItemBase SplitTtem(int withdraw)
                {
                    ItemBase it = item.Split(withdraw);
                    RefreshAmount();
                    return item;
                }

                // trying to take item from other cell
                public virtual bool TakeItem(Cell cell)
                {
                    if (item is null)
                    {
                        var it = cell.TryPullItem();
                        if (it is null) return false;
                        item = it;
                        return true;
                    }
                    return false;
                }

                // try get item from this cell and remove it
                public virtual ItemBase TryPullItem()
                {
                    if (item is null) return null;
                    var it = item;
                    RemoveItem();
                    return it;
                }
        */
        protected virtual void RefreshAmount()
        {
            if (item.Amount == 0)
            {
                RemoveItem();
                return;
            }
            foreach (var bh in activeBehaviors)
            {
                bh.SetAmount(item.Amount);
            }
        }

        // set item to this cell and it's behaviors
        protected virtual void SetItem(ItemBase it)
        {
            item = it;
            foreach (var bh in activeBehaviors)
            {
                bh.SetItem(item);
            }
        }

        // remove item from this cell and it's behaviors
        protected virtual void RemoveItem()
        {
            foreach (var bh in activeBehaviors)
            {
                bh.Reset();
            }
            item = null;
        }

        public virtual void TakeFromCell(Cell cell)
        {
            if (!cell.CanGive()) return;

            TakeFromItem(cell.item);
            cell.RefreshAmount();
        }

        public virtual void TakeFromCell(Cell cell, int amount)
        {
            if (!cell.CanGive()) return;

            TakeFromItem(cell.item, amount);
            cell.RefreshAmount();
        }

        public virtual void GiveToCell(Cell cell)
        {
            cell.TakeFromCell(this);
        }

        public virtual void GiveToCell(Cell cell, int amount)
        {
            cell.TakeFromCell(this, amount);
        }

        // takes maximum amount from given item
        protected virtual void TakeFromItem(ItemBase it)
        {
            if (!CanTake(it)) return;

            if (item is null)
                SetItem((ItemBase)Activator.CreateInstance(it.GetType()));

            item.TakeFromItem(it);
            RefreshAmount();
        }

        protected virtual void TakeFromItem(ItemBase it, int amount)
        {
            if (!CanTake(it)) return;

            if (item is null)
                SetItem((ItemBase)Activator.CreateInstance(it.GetType()));

            item.TakeFromItem(it, amount);
            RefreshAmount();
        }

        public virtual void SwapItems(Cell cell)
        {
            if (!CanSwap(cell.item) || !cell.CanSwap(item))
                return;
            ItemBase it = item;
            SetItem(cell.item);
            cell.SetItem(it);
        }

        public virtual bool CanTake(ItemBase it) => it != null && (item is null || item.GetType() == it.GetType());
        public virtual bool CanGive() => item != null;
        public virtual bool CanSwap(ItemBase it) => true;
        public virtual bool IsFull() => item != null && item.IsFull();

        public virtual void OnLeftClick()
        {
            HandsLogic.InteractWithCell(this);
        }
        public virtual void OnRightClick()
        { 
            PickUpItem(new ItemBase(new System.Random().Next(20)));
        }
    }
}