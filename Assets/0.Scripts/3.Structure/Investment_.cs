using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Investment_ : MonoBehaviour
{
    public CharacterInfoSO selectedCharacter { get; set; }
    public InvestmentUI_ investmentUI;
    public List<ItemSO> items;

    private Inventory_ inventory;

    private void Start()
    {
        inventory = GameManager_.instance.inventory;
    }

    public void SetCharacterInvested(bool isInvested)
    {
        this.selectedCharacter.investData.isInvested = isInvested;
    }

    public void SetCharacter(CharacterInfoSO selectedCharacter)
    {
        this.selectedCharacter = selectedCharacter;
        SetItemsFromSelectedCharacter();

        // UI
        investmentUI.UIUpdate();
    }
    public void SetItemsFromSelectedCharacter()
    {
        items = selectedCharacter.investData.investedItems;
    }

    public void InvestItem(ItemSO item)
    {
        if(selectedCharacter.investData.investedItems.Count < 3)
        {
            // 인벤토리에서 아이템 제거 및 보유 여부 결정
            selectedCharacter.investData.investedItems.Add(item);
            inventory.RemoveItem(item);

            // UI
            investmentUI.UIUpdate();
        }
    }

    public void CancelInvestItem(ItemSO item)
    {
        // 인벤토리에서 아이템 추가 및 보유 여부 결정
        selectedCharacter.investData.investedItems.Remove(item);
        inventory.AddItem(item);

        // UI
        investmentUI.UIUpdate();
    }
}
