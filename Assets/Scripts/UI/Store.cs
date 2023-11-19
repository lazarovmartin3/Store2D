using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public static Store instance;
    public GameObject store_content;
    public GameObject player_inventory_content;
    public GameObject store_item_prefab;
    public GameObject intentory_item_prefab;

    public GameObject store, inventory;
    public Button close_btn;
    public Button torso_items_btn, head_items_btn;
    private InventoryItem.BodyPart current_body_part;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        close_btn.onClick.AddListener(CloseStore);
        current_body_part = InventoryItem.BodyPart.torso;
        torso_items_btn.onClick.AddListener(() =>
        {
            current_body_part = InventoryItem.BodyPart.torso;
            FillStoreItems();
        });
        head_items_btn.onClick.AddListener(() =>
        {
            current_body_part = InventoryItem.BodyPart.head;
            FillStoreItems();
        });
    }

    private void FillStoreItems()
    {
        ClearStore();
        for (int i = 0; i < ItemsManager.instance.All_Items.Count; i++)
        {
            GameObject item = Instantiate(store_item_prefab, store_content.transform);
            if (ItemsManager.instance.All_Items[i].body == current_body_part)
            {
                item.GetComponent<Button>().image.sprite = ItemsManager.instance.All_Items[i].sprite;
                item.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = ItemsManager.instance.All_Items[i].cost.ToString();
                InventoryItem item_instance = ItemsManager.instance.All_Items[i];
                item.GetComponent<Button>().onClick.AddListener(() => BuyItem(item_instance));
            }
            else
            {
                Destroy(item);
            }
        }
    }

    private void ClearStore()
    {
        for (int i = 0; i < store_content.transform.childCount; i++)
        {
            Destroy(store_content.transform.GetChild(i).gameObject);
        }
    }

    private void BuyItem(InventoryItem item)
    {
        if (Inventory.instance.gold_coins >= item.cost)
        {
            Inventory.instance.AddToInventory(item);
            inventory.GetComponent<InventoryUI>().ShowPlayerInventory(true);
            Inventory.instance.TakeGold(item.cost);
        }
        
    }

    public void OpenStore()
    {
        inventory.SetActive(true);
        store.gameObject.SetActive(true);
        close_btn.gameObject.SetActive(true);
        FillStoreItems();
        inventory.GetComponent<InventoryUI>().ShowPlayerInventory(true);
        Player.instance.ui_open = true;
    }

    public void CloseStore()
    {
        inventory.SetActive(false);
        store.gameObject.SetActive(false);
        close_btn.gameObject.SetActive(false);
        Player.instance.ui_open = false;
    }

    public void ShowInventory()
    {
        inventory.SetActive(true);
        inventory.GetComponent<InventoryUI>().ShowPlayerInventory(false);
        close_btn.gameObject.SetActive(true);
        Player.instance.ui_open = true;
    }
}
