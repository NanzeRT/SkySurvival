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

        public virtual ItemBase Item => item;
        public int Amount => item is null? 0 : item.Amount;
        protected List<InventoryLogic> inventories = new List<InventoryLogic>();

        public void AddInventory(InventoryLogic inv) => inventories.Add(inv);
        public void RemoveInventory(InventoryLogic inv) => inventories.Remove(inv);

        protected List<CellBehaviour> activeBehaviors = new List<CellBehaviour>();
        public void AddBehaviour(CellBehaviour bh) 
        {
            activeBehaviors.Add(bh);
            bh.SetItem(item);
        }
        public void RemoveBehaviour(CellBehaviour bh) => activeBehaviors.Remove(bh);

        public virtual void PickUpItem(ItemBase it) => TakeFromItem(it);
        
        public virtual void RefreshAmount()
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

        protected virtual void Destroy()
        {
            foreach (var inv in inventories.ToArray())
            {
                inv.RemoveCell(this);
            }
            foreach (var bh in activeBehaviors.ToArray())
            {
                bh.Destroy();
            }
        }

        public void Trash() => RemoveItem();

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

            if (IsEmpty())
                SetItem((ItemBase)Activator.CreateInstance(it.GetType()));

            item.TakeFromItem(it);
            RefreshAmount();
        }

        protected virtual void TakeFromItem(ItemBase it, int amount)
        {
            if (!CanTake(it)) return;

            if (IsEmpty())
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

        public virtual bool CanTake(ItemBase it) => IsEmpty() || item.GetType() == it?.GetType();
        public virtual bool CanGive() => !IsEmpty();
        public virtual bool CanSwap(ItemBase it) => true;
        public virtual bool IsFull() => item?.IsFull() == true;
        public virtual bool IsEmpty()  => item is null;

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