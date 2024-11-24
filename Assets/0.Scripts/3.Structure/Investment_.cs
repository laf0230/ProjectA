using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Investment_ : MonoBehaviour
{
    public CharacterInfoSO selectedCharacter { get; set; }
    public InvestmentUI_ investmentUI;
    public List<ItemSO> items;

    private Inventory_ inventory;
    public bool isInvested { get; private set; } = false;

    private void Start()
    {
        inventory = GameManager_.instance.inventory;
    }

    public void SetCharacterInvested(bool isInvested)
    {
        // 후원상태일 때 표시하기
        this.selectedCharacter.investData.isInvested = isInvested;
        this.isInvested = isInvested;
        if (isInvested)
        {
            investmentUI.SetInvestCountText("1");
        } 
        else
        {
            investmentUI.SetInvestCountText("0");
        }
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
