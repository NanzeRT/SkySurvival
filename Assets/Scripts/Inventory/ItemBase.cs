using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryItems {
    public class ItemBase
    {
        public ItemBase() {}
        public ItemBase(int a) { amount = a; }
        private int amount = 0;
        public int Amount => amount;
        public ItemBase Split(int withdraw)
        {
            if (withdraw >= amount)
            {
                ItemBase item = (ItemBase) Activator.CreateInstance(this.GetType(), amount);
                amount = 0;
                return item;
            }
            amount -= withdraw;
            return (ItemBase) Activator.CreateInstance(this.GetType(), withdraw);
        }

        public void TakeFromItem (ItemBase item) => _TakeFromItem(item, item.amount);
        public void TakeFromItem (ItemBase item, int withdraw) => _TakeFromItem(item, Math.Min(withdraw, item.amount));

        private void _TakeFromItem (ItemBase item, int withdraw)
        {
            if (item.GetType() != this.GetType()) return;

            item.amount -= withdraw;
            this.amount += withdraw;
        }
        //public void SetAmount(int c) { amount = c; }
        public virtual string Name => "Base Item";
        public virtual string SpritePath => "Sprites/Items/";
        public virtual string SpriteName => "Wheat";
        public virtual Sprite GetSprite()
        {
            return Resources.Load<Sprite>(SpritePath + SpriteName);
        }
    }
}