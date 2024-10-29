using System.Collections.Generic;
using UnityEngine;

public class Inventory_: MonoBehaviour
{
    public List<ItemSO> items;
    private InventoryUI_ inventoryUI;
    public ItemSO testSO;

    public void DebugInvnetory()
    {
        if(testSO != null)
            AddItem(testSO);
    }

    public void Initialize()
    {
        inventoryUI = UIManager_.Instance.inventoryUI;
    }

    public void AddItem(ItemSO item)
    {
        Debug.Log($"인벤토리에 {item.name}아이템이 추가되었습니다!");
        items.Add(item);

        inventoryUI.UpdateUI();
    }

    public void RemoveItem(ItemSO item)
    {
        Debug.Log($"인벤토리에 {item.name}아이템이 제거되었습니다!");
        items.Remove(item);
        inventoryUI.UpdateUI();
        UIManager_.Instance.itemInfoUI.gameObject.SetActive(false);
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
