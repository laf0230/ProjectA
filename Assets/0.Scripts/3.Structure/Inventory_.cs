using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Inventory")]
public class Inventory_: ScriptableObject
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
        Debug.Log($"�κ��丮�� {item.name}�������� �߰��Ǿ����ϴ�!");
        items.Add(item);

        inventoryUI.UpdateUI();
    }

    public void RemoveItem(ItemSO item)
    {
        Debug.Log($"�κ��丮�� {item.name}�������� ���ŵǾ����ϴ�!");
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

        // �������� ���� ���
        Debug.Log($"{item}�������� �������� �ʽ��ϴ�");
        return null;
    }
}
