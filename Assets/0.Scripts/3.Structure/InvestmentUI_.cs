using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvestmentUI_ : MonoBehaviour
{
    public List<GameObject> investedItemSlot; // 아이템을 넣을 슬롯 목록
    private Investment_ investment;
    [field: SerializeField] private TextMeshProUGUI investButtonText;

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

        int slotCount = investedItemSlot.Count;

        for (int i = 0; i < slotCount; i++)
        {
            ItemSlotUI slotUI = investedItemSlot[i].GetComponent<ItemSlotUI>();

            if (!investment.selectedCharacter.investData.isInvested)
            {
                Debug.Log("투자는 진행되지 않았습니다.");
                // 잠긴 경우
                slotUI.isLock = true;
                slotUI.SetLock();
            }
            else
            {
                Debug.Log("투자가 진행되었습니다.");
                slotUI.isLock = false;
                // 아이템이 있는 경우에만 설정
                if (i < investment.items.Count)
                {
                    slotUI.SetItem(investment.items[i]);
                }
                else
                {
                    slotUI.SetEmpty();
                }
            }
        }
    }

    public void SetInvestBtnCurrencyAmount(int amount)
    {
        investButtonText.text = $"{amount} Chip";
    }
}
