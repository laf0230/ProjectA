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
            // �κ��丮���� ������ ���� �� ���� ���� ����
            selectedCharacter.investedItems.Add(item);
            inventory.RemoveItem(item);
            item.isOwned = false;

            // UI
            investmentUI.UIUpdate();
        }
    }

    public void CancelInvestItem(ItemSO item)
    {
            // �κ��丮���� ������ �߰� �� ���� ���� ����
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
