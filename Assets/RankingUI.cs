using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RankingUI : MonoBehaviour
{
    [SerializeField] private Image illust; // ĳ���� �Ϸ���Ʈ
    [SerializeField] private GameObject rankingElementPrefab; // ��ŷ UI ������
    [SerializeField] private GameObject rankingContainer;     // ��ŷ UI �����̳�
    [SerializeField] private Button resultButton;     // ��ŷ UI �����̳�

    public List<CardSO> cards = new List<CardSO>(); // ��� ���� ���� ����Ʈ
    public List<CardSO> deathList = new List<CardSO>();
    public CardSO selectedCard { get; set; }

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
        // ����Ʈ���� ������ �̸��� ���� ĳ���͸� ã�� ó��
        int index = cards.FindIndex(card => card.character.Profile.Name == characterInfo.Profile.Name);

        if (index != -1)
        {
            // ����Ʈ���� �ش� ĳ���͸� ������ �� �������� �߰�
            var existingCard = cards[index];
            cards.RemoveAt(index); // �ش� �ε��� ����
            cards.Add(existingCard); // �������� �߰�
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

            // ������ ĳ���� ǥ��
            if(card.character.investData.isInvested)
            {
                rankingElementObj.SetIsSelected(true);
            }
        }
        illust.sprite = cards[0].fullIllust;

        selectedCard = cards[0];
        resultButton.onClick.RemoveAllListeners();
        resultButton.onClick.AddListener(OnResultButtonClick);
    }

    public void SetIllust(Sprite sprite)
    {
        illust.sprite = sprite;
    }

    public void OnResultButtonClick()
    {
        // ���� ��� ȭ�� ���
        if (cards[0].character.investData.isInvested)
        {
            // ������ ĳ���Ͱ� 1���� ��
            var wishUI = UIManager_.Instance.resultUI;
            wishUI.SetReachedCharacter(selectedCard);
            wishUI.ActiveAsYourWish();
        } else
        {
            // ������ ĳ���Ͱ� 1���� �ƴ� ��
            UIManager_.Instance.resultFailUI.gameObject.SetActive(true);
        }
    }
}

