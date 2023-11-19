using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<InventoryItem> items = new List<InventoryItem>();
    public int gold_coins = 0;

    private void Awake()
    {
        instance = this;
        AddGold(100);
    }

    private void Start()
    {
        GameplayUI.instance.UpdateMoneyAmount(gold_coins);
    }

    public void AddToInventory(InventoryItem new_item)
    {
        if(!items.Contains(new_item)) items.Add(new_item);
    }

    public void RemoveFromInventory(InventoryItem item)
    {
        items.Remove(item);
    }

    public void TakeGold(int amount)
    {
        gold_coins -= amount;
        GameplayUI.instance.UpdateMoneyAmount(gold_coins);
    }

    public void AddGold(int amount)
    {
        gold_coins += amount;
        GameplayUI.instance.UpdateMoneyAmount(gold_coins);
    }

    public void PlayerIsDead()
    {
        gold_coins = 0;
        items.Clear();
    }
}
