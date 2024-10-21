using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<BaseItem> items = new List<BaseItem>(); // 아이템 목록

    // 아이템 추가 메서드
    public void AddItem(ItemSO itemData, int quantity = 1)
    {
        // 아이템이 이미 존재하는 경우 수량만 증가
        foreach (var item in items)
        {
            if (item.itemData == itemData)
            {
                item.quantity += quantity;
                return;
            }
        }

        // 새 아이템 추가
        BaseItem newItem = new BaseItem(itemData, quantity);
        items.Add(newItem);
    }

    // 아이템 제거 메서드
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

    // 특정 아이템이 있는지 확인하는 메서드
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

