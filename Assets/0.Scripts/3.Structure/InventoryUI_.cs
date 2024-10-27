using System.Collections.Generic;
using UnityEngine;

public class InventoryUI_ : MonoBehaviour
{
    private Inventory_ inventory;
    public GameObject itemContainer;
    public List<GameObject> items;
    public GameObject slot;

    public void Initialize(Inventory_ inventory)
    {
        this.inventory = inventory;
    }

    public void AddItem(ItemSO item)
    {
        GameObject createdSlot = Instantiate(slot, itemContainer.transform);
        createdSlot.GetComponent<ItemSlotUI>().CreateItemSlotUI(item);
    }

    public void RemoveItem(ItemSO item)
    {
        foreach (GameObject containedItemUI in items)
        {
            if (containedItemUI.GetComponent<ItemSlotUI>().item == item)
                Destroy(containedItemUI);
        }
    }

    public GameObject GetItem(ItemSO item)
    {
        foreach (GameObject containedItemUI in items)
        {
            if (containedItemUI.GetComponent<ItemSlotUI>().item == item)
                return containedItemUI;
        }

        // 아이템이 없을 경우
        Debug.Log($"{item}아이템이 존재하지 않습니다");
        return null;
    }

    public void UpdateUI()
    {
        var items = inventory.items;

        if(this.items != null)
        {
            // 아이템 목록 초기화
            foreach (GameObject framedItem in this.items)
            {
                Destroy(framedItem);
            }
        }

        foreach (var item in items)
        {
            // 아이템 추가
            var createdSlot = Instantiate(slot, itemContainer.transform);

            createdSlot.GetComponent<ItemSlotUI>().CreateItemSlotUI(item);
            this.items.Add(createdSlot);
        }
    }

    // 투자 창에서의 인벤토리 속 아이템 상호작용
    public void OnInvestUIButtonClick()
    {

    }

    // 상점에서의 인벤토리 속 아이템 상호작용
    public void OnShopUIButtonClick()
    {

    }
}

