using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour {
    public static InventoryController Instance { get; set; }
    public PlayerWeaponController playerWeaponController;
    public ConsumableController consumableController;
    public InventoryUIDetails inventoryUIDetailsPanel;
    public List<Item> playerItems = new List<Item>();

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        playerWeaponController = GetComponent<PlayerWeaponController>();
        consumableController = GetComponent<ConsumableController>();

        GiveItem("sword");
        GiveItem("staff");
        GiveItem("potion_log");
    }

    public void GiveItem(string itemSlug)
    {
        Item item = ItemDatabase.Instance.GetItem(itemSlug);
        playerItems.Add(item);
        //Debug.Log(playerItems.Count + " items in inventory. Added: " + item.ObjectSlug);
        UIEventHandler.ItemAddedToInventory(item);
    }

    public void GiveItem(Item item)
    {
        playerItems.Add(item);
        UIEventHandler.ItemAddedToInventory(item);
    }

    public void SetItemDetails(Item item, Button selectedButton)
    {
        inventoryUIDetailsPanel.SetItem(item, selectedButton);
    }

    public void UseItem(Item item)
    {
        if (item.itemType == Item.ItemTypes.Consumable)
        {
            consumableController.ConsumeItem(item);
        }
        else if (item.itemType == Item.ItemTypes.Equipment)
        {
            playerWeaponController.EquipWeapon(item);
        }
    }
}
