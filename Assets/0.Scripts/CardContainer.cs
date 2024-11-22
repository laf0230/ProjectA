using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardContainer : MonoBehaviour
{
    /// <summary>
    /// 캐릭터 선택 창
    /// 
    /// </summary>

    public List<GameObject> cardObjects = new List<GameObject>();
    public List<CardSO> cards;
    public GameObject cardPrefab;
    public Transform container;

    public void AddCard(CardSO card)
    {
        cards.Add(card);
        GameObject cardObject = Instantiate(cardPrefab, container);
        cardObject.GetComponent<Card_>().SetCardCharacter(card);
    }

    public void SetCards(List<CardSO> cards)
    {
        foreach (var item in cards)
        {
            AddCard(item);
        }
    }

    public void RemoveCard(GameObject card)
    {
        Sprite cardSprite = card.GetComponent<Image>().sprite;
        foreach (GameObject c in cardObjects)
        {
            if (c.GetComponent<Image>().sprite == cardSprite)
            {
                cardObjects.Remove(c);
            }
            else
            {
                Debug.Log(card + " Card is Already Removed");
            }
        }
    }

    public GameObject GetCard(GameObject card)
    {
        GameObject selectedCard = null;

        foreach (GameObject c in cardObjects)
        {
            if(c == card)
            {
                selectedCard = c;
            }
        }

        if (selectedCard == null)
        {
            Debug.Log($"{card} is not contained");
            return null;
        }

        return selectedCard;
    }

    public void OnClickCard()
    {
        GameObject selectedCard = GetCard(gameObject);
    }
}
