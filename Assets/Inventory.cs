using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<BaseItem> items = new List<BaseItem>(); // ������ ���

    // ������ �߰� �޼���
    public void AddItem(ItemSO itemData, int quantity = 1)
    {
        // �������� �̹� �����ϴ� ��� ������ ����
        foreach (var item in items)
        {
            if (item.itemData == itemData)
            {
                item.quantity += quantity;
                return;
            }
        }

        // �� ������ �߰�
        BaseItem newItem = new BaseItem(itemData, quantity);
        items.Add(newItem);
    }

    // ������ ���� �޼���
    public void RemoveItem(ItemSO itemData, int quantity = 1)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemData == itemData)
            {
                items[i].quantity -= quantity;
                if (items[i].quantity <= 0)
                {
                    items.RemoveAt(i);
                }
                return;
            }
        }
    }

    // Ư�� �������� �ִ��� Ȯ���ϴ� �޼���
    public bool HasItem(ItemSO itemData, int quantity)
    {
        foreach (var item in items)
        {
            if (item.itemData == itemData && item.quantity >= quantity)
            {
                return true;
            }
        }
        return false;
    }

    public List<BaseItem> GetItems()
    {
        return items; 
    }
}

