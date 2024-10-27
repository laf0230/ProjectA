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
