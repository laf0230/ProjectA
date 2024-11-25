using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RankingUI : MonoBehaviour
{
    [SerializeField] private Image illust; // 캐릭터 일러스트
    [SerializeField] private GameObject rankingElementPrefab; // 랭킹 UI 프리팹
    [SerializeField] private GameObject rankingContainer;     // 랭킹 UI 컨테이너
    [SerializeField] private Button resultButton;     // 랭킹 UI 컨테이너

    public List<CardSO> cards = new List<CardSO>(); // 사망 순서 저장 리스트
    public List<CardSO> deathList = new List<CardSO>();
    public CardSO selectedCard { get; set; }

    // 필드에 있는 모든 캐릭터를 랭킹에 등록
    // gamemanager에서 캐릭터 리스트 받아오기
    // 죽은 순서대로 정렬
    // 가장 늦게 사망 혹은 살아남은 캐릭터가 1위 달성

    public void SetCharacters(List<CardSO> cards)
    {
        this.cards = cards;
    }

    public void SetDeathOrder(CharacterInfoSO characterInfo)
    {
        // 리스트에서 동일한 이름을 가진 캐릭터를 찾아 처리
        int index = cards.FindIndex(card => card.character.Profile.Name == characterInfo.Profile.Name);

        if (index != -1)
        {
            // 리스트에서 해당 캐릭터를 제거한 뒤 마지막에 추가
            var existingCard = cards[index];
            cards.RemoveAt(index); // 해당 인덱스 제거
            cards.Add(existingCard); // 마지막에 추가
            Debug.Log($"{characterInfo.Profile.Name} 캐릭터가 리스트 내에서 재배치되었습니다.");
        }
        else
        {
            Debug.LogError($"{characterInfo.Profile.Name} 캐릭터를 리스트 내에서 찾을 수 없습니다.");
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

            // 투자한 캐릭터 표시
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
        // 게임 결과 화면 출력
        if (cards[0].character.investData.isInvested)
        {
            // 투자한 캐릭터가 1등일 때
            var wishUI = UIManager_.Instance.resultUI;
            wishUI.SetReachedCharacter(selectedCard);
            wishUI.ActiveAsYourWish();
        } else
        {
            // 투자한 캐릭터가 1등이 아닐 때
            UIManager_.Instance.resultFailUI.gameObject.SetActive(true);
        }
    }
}

