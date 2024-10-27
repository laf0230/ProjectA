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
            var createdSlot = Instantiate(slot, itemContainer.transform);

            createdSlot.GetComponent<ItemSlotUI>().CreateItemSlotUI(item);
            this.items.Add(createdSlot);
        }
    }

    // ���� â������ �κ��丮 �� ������ ��ȣ�ۿ�
    public void OnInvestUIButtonClick()
    {

    }

    // ���������� �κ��丮 �� ������ ��ȣ�ۿ�
    public void OnShopUIButtonClick()
    {

    }
}

