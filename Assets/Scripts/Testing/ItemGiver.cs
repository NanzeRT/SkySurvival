using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using InventorySystem;
using InventoryItems;

public class ItemGiver : MonoBehaviour, IPointerClickHandler
{
    public MonoCellHandler inventory;
    protected virtual ItemBase GetItem => new ItemBase(new System.Random().Next(20));
    public void OnPointerClick(PointerEventData eventData)
    {
        inventory.PickUpItem(GetItem);
    }
}
