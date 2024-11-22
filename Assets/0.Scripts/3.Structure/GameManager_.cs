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
    public FieldManager fieldManager;
    public CameraDrag playerView;
    public bool isBattleStarted = false;

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
        player.chip.amount = 10000;

        // UI 업데이트
        UIManager_.Instance.UpdateCurrencyUI();

        inventory.Initialize();
        GameStart();
        UIManager_.Instance.battleStartButton.onClick.AddListener(StartBattle);
        // StartBattle();
    }

    private void Update()
    {
        #region 배속

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = 0.1f;
        }
        
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha2))
        {
            Time.timeScale = 0.5f;
        }

        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha3))
        {
            Time.timeScale = 1f;
        }

        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha4))
        {
            Time.timeScale = 1.5f;
        }

        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha5))
        {
            Time.timeScale = 2f;
        }

        #endregion

        if(isBattleStarted && GameObject.FindGameObjectsWithTag("Character").Length == 1)
        {
            UIManager_.Instance.ActiveGameEndUI(true);

            isBattleStarted = false;
        }
    }

    // 전투 시작 메서드
    public void StartBattle()
    {
        // 'Character' 태그를 가진 모든 오브젝트를 활성화
        // player.inventory.Initialize();

        foreach (var item in selectedCharacters)
        {
            item.gameObject.SetActive(true);
        }

        UIManager_.Instance.GetUIGroup(UIType_.ManagementUI).Close();
        fieldManager.ActiveCharactersOnField();
        UIManager_.Instance.onFieldUI.gameObject.SetActive(true);
        UIManager_.Instance.onFieldUI.ActiveCharacterUI();

        isBattleStarted = true;
    }

    public void GameStart()
    {
        selectedCards = SelectRandomCharacters();
        // UIManager_.Instance.cardContainer.gameObject.SetActive(true);
        player.gold.CurrencyUIUpdate();
        UIManager_.Instance.cardContainer.SetCards(selectedCards);
        UIManager_.Instance.onFieldUI.CreateAndGetNewCharacterUIs(selectedCards);
        UIManager_.Instance.rankingUI.DisplayDeathRanking();
        fieldManager.SpawnCharacters(selectedCards);
    }

    public void EndBattle()
    {
        foreach (CardSO card in selectedCards)
        {
            card.character.investData.isInvested = false;
        }
    }

    public List<CardSO> SelectRandomCharacters()
    {
        var selectedCards = new List<CardSO>();

        // 카드 리스트를 섞습니다.
        var shuffledCards = new List<CardSO>(cards);
        for (int i = 0; i < shuffledCards.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffledCards.Count);
            var temp = shuffledCards[i];
            shuffledCards[i] = shuffledCards[randomIndex];
            shuffledCards[randomIndex] = temp;
        }

        // characterCount만큼 카드를 선택합니다.
        for (int i = 0; i < Mathf.Min(characterCount, shuffledCards.Count); i++)
        {
            selectedCards.Add(shuffledCards[i]);
        }

        return selectedCards;
    }
    public void InstantiateCharacter(CardSO cardSO, Vector3 position)
    {
        // 캐릭터의 생성 및 배치 코드 작성
    }

    public CardSO GetCardFromSelectedCard(CharacterInfoSO characterInfo)
    {
        foreach (var item in selectedCards)
        {
            if (item.profile.name == characterInfo.Profile.Name)
            {
                return item;
            }
        }
        return null;
    }
}

