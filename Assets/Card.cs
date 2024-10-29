using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class Card
{
    public CharacterInfoSO characterData;
}

public class CharacterDeck : MonoBehaviour
{
    public List<CardSO> cards = new List<CardSO>();

    public CharacterInfoSO SelectCharacter(int cardIndex)
    {
        if (cardIndex > 0 || cardIndex >= cards.Count)
        {
            return cards[cardIndex].character;
        } else
        {
            return null;
        }
    }
}

public class CharacterCardUI : MonoBehaviour
{
    public Sprite characterImage;
    public string characterNameText;
    public CardSO card;

    public void SetCard(CardSO card)
    {
        this.card = card;
        characterNameText = card.character.Profile.Name;
        characterImage = card.profile;
    }
}

public class CharacterSelectionManager : MonoBehaviour
{
    public CharacterDeck characterDeck;
    public CharacterCardUI cardUI;

    public void OnSelectCard(int cardIndex)
    {
        CharacterInfoSO selectedCharacter = characterDeck.SelectCharacter(cardIndex);
        if (selectedCharacter != null)
        {
            // 캐릭터 데이터를 UI에 연결
            cardUI.SetCard(characterDeck.cards[cardIndex]);
        }
    }
}
