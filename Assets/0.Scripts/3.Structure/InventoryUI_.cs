using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI_ : MonoBehaviour
{
    private Inventory_ inventory;
    public GameObject itemContainer;
    public List<GameObject> items;
    public GameObject slot;
    public Button shopButton;

    private void Start()
    {
        inventory = GameManager_.instance.inventory;
        shopButton.onClick.AddListener(OnShopButtonClick);
    }

    public void OnShopButtonClick()
    {
        UIManager_.Instance.shopUI.gameObject.SetActive(false);
        UIManager_.Instance.itemInfoUI.gameObject.SetActive(false);
        UIManager_.Instance.standingUI.gameObject.SetActive(false);
        // UIManager_.Instance.itemInfoUI.
    }

    public void AddItem(ItemSO item)
    {
        GameObject createdSlot = Instantiate(slot, itemContainer.transform);
        createdSlot.GetComponent<ItemSlotUI>().SetItem(item);
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
            var slotObject = Instantiate(slot, itemContainer.transform);

            var slotUI = slotObject.GetComponent<ItemSlotUI>();

            slotUI.isInInventory = true;
            slotUI.SetItem(item);

            slotUI.slotType = ItemSlotType.Sell;

            this.items.Add(slotObject);
        }
    }

     public void SetItemSlotType(ItemSlotType slotType)
    {
        foreach (GameObject item in items)
        {
            item.GetComponent<ItemSlotUI>().slotType = slotType;
        }
    }
}
