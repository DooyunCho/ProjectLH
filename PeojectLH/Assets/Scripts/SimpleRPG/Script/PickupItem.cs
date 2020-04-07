using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : Interactable {
    public Item itemDrop { get; set; }

    public void Start()
    {
        myName = itemDrop.ItemName;
    }

    public override void Interact()
    {
        //Debug.Log("PickupItem: " + itemDrop.ItemName);
        //InventoryController.Instance.GiveItem("sword");
        InventoryController.Instance.GiveItem(itemDrop);
        Destroy(gameObject);
    }
}
