using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvestmentUI_ : MonoBehaviour
{
    public List<GameObject> investedItemSlot; // �������� ���� ���� ���
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
                Debug.Log("���ڴ� ������� �ʾҽ��ϴ�.");
                // ��� ���
                slotUI.isLock = true;
                slotUI.SetLock();
            }
            else
            {
                Debug.Log("���ڰ� ����Ǿ����ϴ�.");
                slotUI.isLock = false;
                // �������� �ִ� ��쿡�� ����
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
