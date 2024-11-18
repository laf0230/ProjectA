using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFieldUI : MonoBehaviour
{
    public GameObject battleCharUIContainer;
    public GameObject battleCharUIPrefab;
    public List<BattleCharacterIconUI> battleCharUIList;

    public List<BattleCharacterIconUI> CreateAndGetNewCharacterUIs(List<CardSO> cards)
    {
        var uis = new List<BattleCharacterIconUI>();
        foreach (CardSO card in cards)
        {
            uis.Add(CreateAndGetNewCharacterUI(card));
        }
        return uis;
    }

    public BattleCharacterIconUI CreateAndGetNewCharacterUI(CardSO cardSO)
    {
        var charUIObj = Instantiate(battleCharUIPrefab, battleCharUIContainer.transform);
        BattleCharacterIconUI characterUI = charUIObj.GetComponent<BattleCharacterIconUI>();

        characterUI.Initialize(cardSO);

        battleCharUIList.Add(characterUI);

        return characterUI;
    }

    public void ActiveCharacterUI()
    {
        foreach (var item in battleCharUIList)
        {
            item.ActiveUI();
        }
    }
}
