using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    public SpriteRenderer torso_armor;
    public SpriteRenderer helmet_armor;
    public Sprite default_helmet, default_torso;

    private InventoryItem current_torso_item;
    private InventoryItem current_head_item;


    private void Start()
    {
        Invoke("SetUpBasicArmor", 1);
    }

    public void SetUpBasicArmor()
    {
        current_torso_item = ItemsManager.instance.GetItemById(6);
        ChangeTorsoArmor(current_torso_item);
        current_head_item = ItemsManager.instance.GetItemById(8);
        ChangeHelmet(current_head_item);
    }

    public void ChangeTorsoArmor(InventoryItem item)
    {
        torso_armor.sprite = item.sprite;
        Inventory.instance.AddToInventory(current_torso_item);
        current_torso_item = item;
    }

    public void ChangeHelmet(InventoryItem item)
    {
        helmet_armor.sprite = item.sprite;
        Inventory.instance.AddToInventory(current_head_item);
        current_head_item = item;
    }

    public void RemoveHelmet()
    {
        current_head_item = null;
        helmet_armor.sprite = default_helmet;
    }

    public void RemoveTorso()
    {
        current_torso_item = null;
        torso_armor.sprite = default_torso;
    }
}
