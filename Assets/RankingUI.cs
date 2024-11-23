using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RankingUI : MonoBehaviour
{
    [SerializeField] private Image illust; // ĳ���� �Ϸ���Ʈ
    [SerializeField] private GameObject rankingElementPrefab; // ��ŷ UI ������
    [SerializeField] private GameObject rankingContainer;     // ��ŷ UI �����̳�

    public List<CardSO> cards = new List<CardSO>(); // ��� ���� ���� ����Ʈ
    public List<CardSO> deathList = new List<CardSO>();

    // �ʵ忡 �ִ� ��� ĳ���͸� ��ŷ�� ���
    // gamemanager���� ĳ���� ����Ʈ �޾ƿ���
    // ���� ������� ����
    // ���� �ʰ� ��� Ȥ�� ��Ƴ��� ĳ���Ͱ� 1�� �޼�

    public void SetCharacters(List<CardSO> cards)
    {
        this.cards = cards;
    }

    public void SetDeathOrder(CharacterInfoSO characterInfo)
    {
        // CharacterInfoSO�� ������� CardSO�� ������
        CardSO characterCard = GameManager_.instance.GetCardFromSelectedCard(characterInfo);

        // ����Ʈ���� ������ �̸��� ���� ĳ���͸� ã�� ó��
        var existingCard = cards.FirstOrDefault(card => card.character.Profile.Name == characterInfo.Profile.Name);
        if (existingCard != null)
        {
            cards.Remove(existingCard); // ����Ʈ���� ����
            cards.Add(existingCard);   // ����Ʈ�� �������� �߰�
            Debug.Log($"{characterInfo.Profile.Name} ĳ���Ͱ� ����Ʈ ������ ���ġ�Ǿ����ϴ�.");
        }
        else
        {
            Debug.LogError($"{characterInfo.Profile.Name} ĳ���͸� ����Ʈ ������ ã�� �� �����ϴ�.");
        }
    }

    public void DesplayRanking(bool isActive)
    {
        if (!isActive)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);

        foreach (CardSO card in cards)
        {
            var rankingElementObj = Instantiate(rankingElementPrefab, rankingContainer.transform).GetComponent<RankingElement>();

            rankingElementObj.Initialize(
                new RankData
                {
                    characterCard = card, 
                },
                cards.FindIndex(c => c.character.Profile.Name == card.character.Profile.Name) + 1
                );
        }
        illust.sprite = cards[0].fullIllust;
    }

    public void SetIllust(Sprite sprite)
    {
        illust.sprite = sprite;
    }
}

