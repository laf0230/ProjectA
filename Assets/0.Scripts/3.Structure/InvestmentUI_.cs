using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestmentUI_ : MonoBehaviour
{
    public List<GameObject> investedItemSlot; // �������� ���� ���� ���
    private Investment_ investment;

    public void Start()
    {
        investment = GameManager_.instance.investment;
    }

    public void Initialize()
    {
        foreach (var item in investedItemSlot)
        {
            item.GetComponent<ItemSlotUI>().SetEmpty();
        }
    }

    public void UIUpdate()
    {
        Initialize();

        int itemCount = investment.items.Count;
        int slotCount = investedItemSlot.Count;
        int minCount = Mathf.Min(itemCount, slotCount);

        for (int i = 0; i < minCount; i++)
        {
            ItemSlotUI slotUI = investedItemSlot[i].GetComponent<ItemSlotUI>();
            if (false)
            {
                // ��� ���
                slotUI.SetEmpty();
            } 
            else
            {
                slotUI.SetItem(investment.items[i]);
            }
        }

        // minCount������ ������ �����
        for(int i = minCount; i < slotCount; i++)
        {
            ItemSlotUI slotUI = investedItemSlot[i].GetComponent<ItemSlotUI>();
            slotUI.SetEmpty();
        }
    }
}
