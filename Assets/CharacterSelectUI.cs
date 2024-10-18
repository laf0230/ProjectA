using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    /// <summary>
    /// 캐릭터 선택 창
    /// 
    /// </summary>
    [System.Serializable]
    public class CardFair
    {
        public int id;
        public CharacterInfoSO characterInfo;
    }

    public List<GameObject> Cards = new List<GameObject>();
    public List<CardFair> CardIDList = new List<CardFair>();

    public void AddCard(GameObject card)
    {
        GameObject createdCard = Instantiate(card);
        Cards.Add(Instantiate(createdCard));
    }

    public void RemoveCard(GameObject card)
    {
        Sprite cardSprite = card.GetComponent<Image>().sprite;
        foreach (GameObject c in Cards)
        {
            if (c.GetComponent<Image>().sprite == cardSprite)
            {
                Cards.Remove(c);
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

        foreach (GameObject c in Cards)
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
