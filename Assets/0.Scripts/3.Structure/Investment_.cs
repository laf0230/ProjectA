using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Investment_ : MonoBehaviour
{
    public CharacterInfoSO selectedCharacter { get; set; }
    public InvestmentUI_ investmentUI;
    public List<ItemSO> items;
    public bool isInvested = false; // ��� ���� ����� ����

    private Inventory_ inventory;

    private void Start()
    {
        inventory = GameManager_.instance.inventory;
    }

    public void SetCharacterInvested(bool isInvested)
    {
        this.isInvested = isInvested;
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
        items = selectedCharacter.investedItems;
    }

    public void InvestItem(ItemSO item)
    {
        if(selectedCharacter.investedItems.Count < 3)
        {
            // �κ��丮���� ������ ���� �� ���� ���� ����
            selectedCharacter.investedItems.Add(item);
            inventory.RemoveItem(item);

            // UI
            investmentUI.UIUpdate();
        }
    }

    public void CancelInvestItem(ItemSO item)
    {
        // �κ��丮���� ������ �߰� �� ���� ���� ����
        selectedCharacter.investedItems.Remove(item);
        inventory.AddItem(item);

        // UI
        investmentUI.UIUpdate();
    }
}
