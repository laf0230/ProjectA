using System.Collections.Generic;
using UnityEngine;

public class InventoryUI_ : MonoBehaviour
{
    private Inventory_ inventory;
    public GameObject itemContainer;
    public List<GameObject> items;
    public GameObject slot;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory_>();
    }

    public void AddItem(ItemSO item)
    {
        var createdSlot = GameObject.Instantiate(slot, itemContainer.transform);
        createdSlot.GetComponent<ItemSlotUI>().CreateItemSlotUI(item, false);
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
        foreach (GameObject framedItem in items)
        {
            Destroy(framedItem);
        }

        foreach (var item in inventory.items)
        {
            var createdSlot = Instantiate(slot, itemContainer.transform);

            createdSlot.GetComponent<ItemSlotUI>().CreateItemSlotUI(item, false);
        }
    }
}

