using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_ : MonoBehaviour
{
    // 싱글턴 인스턴스
    public static GameManager_ instance;

    // 플레이어 데이터
    public Player_ player = new Player_();
    public List<CardSO> cards = new List<CardSO>();
    public List<Character> selectedCharacters;
    public List<CardSO> selectedCards;
    public Inventory_ inventory;
    public Shop_ shop;
    public Investment_ investment;

    [Header("게임 정보")]
    public int characterCount = 1;

    // 싱글턴 초기화
    private void Awake()
    {
        Debug.Log(shop);
        // 인스턴스가 존재하지 않는 경우 현재 인스턴스로 설정
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 파괴
        }
    }

    private void Start()
    {
        player.gold.amount = 10000;
        player.gold.amount = 10000;

        // UI 업데이트
        UIManager_.Instance.UpdateCurrencyUI();

        inventory.Initialize();
        GameStart();
    }

    // 전투 시작 메서드
    public void StartBattle()
    {
        // 'Character' 태그를 가진 모든 오브젝트를 활성화
        player.inventory.Initialize();

        foreach (var item in selectedCharacters)
        {
            item.gameObject.SetActive(true);
        }
    }

    public void GameStart()
    {
        selectedCards = SelectRandomCharacters();
        // UIManager_.Instance.cardContainer.gameObject.SetActive(true);
        UIManager_.Instance.cardContainer.SetCards(selectedCards);
    }

    public List<CardSO> SelectRandomCharacters()
    {
        var selectedCards = new List<CardSO>();
        for(int i = 0; i < characterCount; i++)
        {
            var selectedCard = cards[Random.Range(0, cards.Count - 1)];

            selectedCards.Add(selectedCard);
        }
        return selectedCards;
    }

    public void InstantiateCharacter(CardSO cardSO, Vector3 position)
    {
        // 캐릭터의 생성 및 배치 코드 작성
    }
}

