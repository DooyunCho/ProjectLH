using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIItem : MonoBehaviour {
    public Item item;
    public Image itemImage;
    public Text itemText;

    public void SetItem(Item item)
    {
        this.item = item;
        itemText = this.transform.Find("Item_Name").GetComponent<Text>();
        itemImage = this.transform.Find("Item_Icon").GetComponent<Image>();

        SetupItemValues();
    }

    void SetupItemValues()
    {
        this.itemText.text = item.ItemName;
        this.itemImage.sprite = Resources.Load<Sprite>("UI/Icons/Items/" + item.ObjectSlug);
    }

    public void OnSelectItemButton()
    {
        InventoryController.Instance.SetItemDetails(item, GetComponent<Button>());
    }
}
