using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestmentUI_ : MonoBehaviour
{
    public List<GameObject> investedItemSlot;
    public GameObject slot;
    public GameObject container;
    private Investment_ investment;

    public void Initialize()
    {
        foreach (var item in investedItemSlot)
        {
            Destroy(item);
        }
    }

    public void UIUpdate()
    {
        Initialize();

        foreach (var item in investment.items)
        {
            GameObject createdItemUI = Instantiate(slot, container.transform);
            var slotUI = createdItemUI.GetComponent<ItemSlotUI>();

            createdItemUI.GetComponent<ItemSlotUI>().CreateItemSlotUI(item);
        }
    }

    public void OnInventoryItemSlotUI()
    {
        switch (UIManager_.Instance.currentUIType)
        {
            case UIType.Invest:
                break;
            case UIType.Shop:
                break;
        }
    }
}
