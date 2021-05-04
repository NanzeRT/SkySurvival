using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryItems
{
    public class StickI : ItemBase
    {
        public StickI() { }
        public StickI(int a) : base(a) { }
        public override string Name => "Stick";
        public override string SpriteName => "Stick";
    }
}