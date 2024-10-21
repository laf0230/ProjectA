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

        // �������� ���� ���
        Debug.Log($"{item}�������� �������� �ʽ��ϴ�");
        return null;
    }
}
