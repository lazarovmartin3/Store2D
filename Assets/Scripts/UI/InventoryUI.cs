using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject player_inventory_content;
    public GameObject intentory_item_prefab;
    public Button equip_item_btn;
    public Button sell_btn;

    private InventoryItem selected_item = null;

    private void Start()
    {
        equip_item_btn.onClick.AddListener(EquipItem);
        sell_btn.onClick.AddListener(SellItem);
    }

    public void ShowPlayerInventory(bool sell_option)
    {
        for (int i = 0; i < player_inventory_content.transform.childCount; i++)
        {
            Destroy(player_inventory_content.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < Inventory.instance.items.Count; i++)
        {
            GameObject item = Instantiate(intentory_item_prefab, player_inventory_content.transform);
            item.GetComponent<Button>().image.sprite = Inventory.instance.items[i].sprite;
            item.name = Inventory.instance.items[i].name;
            InventoryItem new_item = Inventory.instance.items[i];
            item.GetComponent<Button>().onClick.AddListener(() => SelectItem(new_item));
            item.GetComponentInChildren<TextMeshProUGUI>().text = new_item.description;
        }
        sell_btn.gameObject.SetActive(sell_option);
    }

    private void SelectItem(InventoryItem new_item)
    {
        selected_item = new_item;
    }

    private void EquipItem()
    {
        Player.instance.ChangeArmor(selected_item);
        Destroy(player_inventory_content.transform.Find(selected_item.name).gameObject);
    }

    private void SellItem()
    {
        Destroy(player_inventory_content.transform.Find(selected_item.name).gameObject);
        Inventory.instance.RemoveFromInventory(selected_item);
        Inventory.instance.AddGold(selected_item.cost);
        ShowPlayerInventory(true);
    }
}
