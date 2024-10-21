using System.Collections.Generic;
using UnityEngine;

public class Inventory_ : MonoBehaviour
{
    public List<ItemSO> items;
    private InventoryUI_ inventoryUI;

    private void Start()
    {
        inventoryUI = UIManager_.Instance.inventoryUI;
    }

    public void AddItem(ItemSO item)
    {
        items.Add(item);

        inventoryUI.UpdateUI();
    }

    public void RemoveItem(ItemSO item)
    {
        items.Remove(item);
        inventoryUI.UpdateUI();
    }

    public ItemSO GetItem(ItemSO item)
    {
        foreach (ItemSO containedItem in items)
        {
            if (containedItem == item)
                return containedItem;
        }

        // 아이템이 없을 경우
        Debug.Log($"{item}아이템이 존재하지 않습니다");
        return null;
    }
}
