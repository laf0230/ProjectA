using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingUI : MonoBehaviour
{
    [SerializeField] private Image illust; // 캐릭터 일러스트
    [SerializeField] private GameObject rankingElementPrefab; // 랭킹 UI 프리팹
    [SerializeField] private GameObject rankingContainer;     // 랭킹 UI 컨테이너

    public List<CardSO> deathOrder = new List<CardSO>(); // 사망 순서 저장 리스트

        // 필드에 있는 모든 캐릭터를 랭킹에 등록
        // gamemanager에서 캐릭터 리스트 받아오기
        // 죽은 순서대로 정렬
        // 가장 늦게 사망 혹은 살아남은 캐릭터가 1위 달성

    public void SetCharacters()
    {
        deathOrder = GameManager_.instance.selectedCards;
    }

    public void AddToDeathOrder(CardSO card)
    {
        // 죽은 캐릭터를 순서대로 리스트에 추가
        if (!deathOrder.Contains(card))
        {
            deathOrder.Add(card);
        }
    }

    public void DisplayDeathRanking()
    {
        // UI 컨테이너 초기화
        foreach (Transform child in rankingContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // 사망 순서를 기반으로 랭킹 UI 생성
        for (int i = 0; i < deathOrder.Count; i++)
        {
            // 리스트의 마지막부터 순위를 매김
            var card = deathOrder[deathOrder.Count - 1 - i];
            int rank = i + 1; // 순위는 1부터 시작

            // UI 요소 생성
            var rankingObject = Instantiate(rankingElementPrefab, rankingContainer.transform);
            var rankingElement = rankingObject.GetComponent<RankingElement>();

            // RankData를 생성하여 초기화
            var rankData = new RankData
            {
                characterCard = card
            };
            rankingElement.Initialize(rankData, rank);
        }
    }
}

