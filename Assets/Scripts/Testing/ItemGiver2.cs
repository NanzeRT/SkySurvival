using System.Collections;
using System.Collections.Generic;
using InventoryItems;
using UnityEngine;

public class ItemGiver2 : ItemGiver
{
    protected override ItemBase GetItem => new StickI(new System.Random().Next(20));
}
