using System.Collections.Generic;
using UnityEngine;

public class InventoryUI_ : MonoBehaviour
{
    private Inventory_ inventory;
    public GameObject inventoryUI;
    public GameObject itemContainer;
    public List<GameObject> items;
    public GameObject slot;

    private void Start()
    {
        inventory = GameManager_.instance.inventory;
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

        // �������� ���� ���
        Debug.Log($"{item}�������� �������� �ʽ��ϴ�");
        return null;
    }

    public void UpdateUI()
    {
        var items = inventory.items;

        if(this.items != null)
        {
            // ������ ��� �ʱ�ȭ
            foreach (GameObject framedItem in this.items)
            {
                Destroy(framedItem);
            }
        }

        foreach (var item in items)
        {
            // ������ �߰�
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

    public void Open()
    {
        inventoryUI.SetActive(true);
    }

    public void Close()
    {
        inventoryUI.SetActive(false);
    }
}
