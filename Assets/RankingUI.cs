using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingUI : MonoBehaviour
{
    [SerializeField] private Image illust; // ĳ���� �Ϸ���Ʈ
    [SerializeField] private GameObject rankingElementPrefab; // ��ŷ UI ������
    [SerializeField] private GameObject rankingContainer;     // ��ŷ UI �����̳�

    public List<CardSO> deathOrder = new List<CardSO>(); // ��� ���� ���� ����Ʈ

        // �ʵ忡 �ִ� ��� ĳ���͸� ��ŷ�� ���
        // gamemanager���� ĳ���� ����Ʈ �޾ƿ���
        // ���� ������� ����
        // ���� �ʰ� ��� Ȥ�� ��Ƴ��� ĳ���Ͱ� 1�� �޼�

    public void SetCharacters()
    {
        deathOrder = GameManager_.instance.selectedCards;
    }

    public void AddToDeathOrder(CardSO card)
    {
        // ���� ĳ���͸� ������� ����Ʈ�� �߰�
        if (!deathOrder.Contains(card))
        {
            deathOrder.Add(card);
        }
    }

    public void DisplayDeathRanking()
    {
        // UI �����̳� �ʱ�ȭ
        foreach (Transform child in rankingContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // ��� ������ ������� ��ŷ UI ����
        for (int i = 0; i < deathOrder.Count; i++)
        {
            // ����Ʈ�� ���������� ������ �ű�
            var card = deathOrder[deathOrder.Count - 1 - i];
            int rank = i + 1; // ������ 1���� ����

            // UI ��� ����
            var rankingObject = Instantiate(rankingElementPrefab, rankingContainer.transform);
            var rankingElement = rankingObject.GetComponent<RankingElement>();

            // RankData�� �����Ͽ� �ʱ�ȭ
            var rankData = new RankData
            {
                characterCard = card
            };
            rankingElement.Initialize(rankData, rank);
        }
    }
}

