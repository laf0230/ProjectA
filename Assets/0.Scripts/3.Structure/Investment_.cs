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
    }

    public void InvestItem(ItemSO item)
    {
        if(selectedCharacter.investedItems.Count < 3)
        {
            // 인벤토리에서 아이템 제거 및 보유 여부 결정
            selectedCharacter.investedItems.Add(item);
            inventory.RemoveItem(item);
            item.isOwned = false;

            // UI
            investmentUI.UIUpdate();
        }
    }

    public void CancelInvestItem(ItemSO item)
    {
            // 인벤토리에서 아이템 추가 및 보유 여부 결정
        selectedCharacter.investedItems.Remove(item);
        inventory.AddItem(item);
        item.isOwned = true;

        // UI
        investmentUI.UIUpdate();
    }

    public void InvestGold()
    {

    }
}
