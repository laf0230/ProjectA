using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Investment_ : MonoBehaviour
{
    public CharacterInfoSO selectedCharacter;
    public InvestmentUI_ investmentUI;
    public List<ItemSO> items;

    private Inventory_ inventory;

    public void Initialize()
    {
        items = selectedCharacter.investedItems;
        // 초기화를 진행할때마다 UI의 타입을 바꿈 -> 현재 UI가 어떤 UI인지 알기 위한 코드
        UIManager_.Instance.currentUIType = UIType.Invest;
    }

    public void InvestItem(ItemSO item)
    {
        if(selectedCharacter.investedItems.Count < 3)
        {
            // Inventory
            selectedCharacter.investedItems.Add(item);
            inventory.RemoveItem(item);

            // UI
            investmentUI.UIUpdate();
        }
    }

    public void CancelInvestItem(ItemSO item)
    {
        // Inventory
        selectedCharacter.investedItems.Remove(item);
        inventory.AddItem(item);

        // UI
        investmentUI.UIUpdate();
    }

    public void InvestGold()
    {

    }
}
