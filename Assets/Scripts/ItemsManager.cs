using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public static ItemsManager instance;

    public List<InventoryItem> All_Items = new List<InventoryItem>();

    private void Awake()
    {
        instance = this;
    }

    public InventoryItem GetItemById(int id)
    {
        foreach (InventoryItem item in All_Items)
        {
            if (item.id == id)
            {
                return item;
            }
        }
        return null;
    }

}
