using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager_ : MonoBehaviour
{
    public static UIManager_ Instance { get; private set; }

    public List<UIGroup> uIGroups = new List<UIGroup>();
    public InventoryUI_ inventoryUI;
    public ShopUI_ shopUI;
    public ItemInfoUI_ itemInfoUI;
    public InvestmentUI investmentUI;
    public InvestCalcUI investCalcUI;
    public StandingUI standingUI;
    public List<CurrencyUI> currencyUIs;

    public GameObject WorldUI;
    public Image StandingImage;
    public Sprite RedBisonFI;
    public Sprite DemoFI;
    public Sprite FlaFlaFI;

    private void Awake()
    {
        // Check if the instance already exists and if it's not this one
        if (Instance != null && Instance != this)
        {
            // Destroy this instance as it is a duplicate
            Destroy(gameObject);
        }
        else
        {
            // Assign the instance to this object
            Instance = this;
            // Make sure this instance persists across scene loads
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        GameStart();
    }

    private void FixedUpdate()
    {

        if (currencyUIs[0].gameObject.activeSelf && currencyUIs[1].gameObject.activeSelf)
        {
            UpdateCurrencyUI();
        }
    }

    public void GameStart()
    {
        StartCoroutine(IEStart());
    }

    public IEnumerator IEStart()
    {

        foreach (UIGroup UI in uIGroups)
        {

            // 클로즈 버튼들
            foreach (var btn in UI.disableButtons)
            {
                btn.onClick.AddListener(UI.Close);
            }

            // 오픈 버튼들
            foreach (var btn in UI.enableButtons)
            {
                btn.onClick.AddListener(UI.Open);
            }

            if (UI.uIName == "캐릭터 선택창")
                UI.Open();
            else if (UI.uIName == "관리창")
                UI.Open();
            else UI.Close();
        }
        
        WorldUI.SetActive(true);
        yield return new WaitForSeconds(3);
        WorldUI.SetActive(false);
    }

    public void OnClickBattleStart()
    {
        var characters = GameObject.FindGameObjectsWithTag("Character");

        foreach (GameObject character in characters)
        {
            character.SetActive(true);
        }
    }

    public void SetImage(string characterName)
    {
        switch (characterName)
        {
            case "Flafla":
                standingUI.SetIllust(FlaFlaFI);
                break;
            case "Demo":
                standingUI.SetIllust(DemoFI);
                break;
            case "RedBison":
                standingUI.SetIllust(RedBisonFI);
                break;
        }
    }

    public void UpdateCurrencyUI()
    {
        foreach (var item in currencyUIs)
        {
            if (item.currencyType == CurrencyType.Gold)
            {
                item.DisplayCurrency(GameManager_.instance.player.Gold);
            }
            else
            {
                item.DisplayCurrency(GameManager_.instance.player.Chip);
            }
        }
    }
}


[System.Serializable]
public class UIGroup
{
    public string uIName;
    public List<GameObject> uiList;
    public List<Button> enableButtons;
    public List<Button> disableButtons;

    public void Open()
    {
        foreach (GameObject ui in uiList)
        {
            ui.SetActive(true);
        }
    }

    public void Close()
    {
        foreach (GameObject ui in uiList)
        {
            ui.SetActive(false);
        }
    }
}

public class EntityManager : MonoBehaviour
{
    public List<ItemSO> items;
    public Shop_ shop;

    public void Start()
    {
        foreach(ItemSO item in items)
        {
            shop.AddItem(item);
        }
    }
}



public class InvestManager_ : MonoBehaviour // 캐릭터 투자 창
{
    public static InvestManager_ instance;
}

public class InvestmentUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameSpace;
    [SerializeField] private TextMeshProUGUI descriptionSpace;
    [SerializeField] private Button investButton;

    private void Start()
    {
        investButton.onClick.AddListener(OnInvestButtonClick);
    }

    public void OnInvestButtonClick()
    {
        // TODO: 투자금 계산 UI 활성화
        // UIManager_.Instance.investCalcUI
    }
}

[System.Serializable]
public class Player_
{
    public string Name;
    public Sprite ProfileImage;
    // public Currency Gold = new Currency(CurrencyType.Gold, 0);
    // public Currency Chip = new Currency(CurrencyType.Chip, 0);
    public int Gold;
    public int Chip;
    public List<InvestmentData> InvestmentDatas;

    // 플레이어가 소유한 아이템 목록
    public List<ItemSO> ownedItems;

    // 캐릭터에 투자하는 메서드
    public void InvestCharacter(Character character, int cost)
    {
        // 투자 데이터에서 해당 캐릭터와 일치하는 카드를 찾기
        InvestmentData investData = FindInvestmentData(character);

        if (investData != null)
        {
            // 투자할 금액을 설정, 보유 칩보다 클 경우 보유한 만큼만 투자
            if (cost > Chip)
            {
                cost = Chip;
            }

            // 해당 카드의 InvestmentData 속 cost.amount에 값 대입
            investData.cost.amount = cost;
            Debug.Log("Invested " + cost + " into character: " + character.name);

            // 이후 추가 로직 작성 가능
        }
        else
        {
            Debug.LogWarning("No matching character found for investment.");
        }
    }

    // 캐릭터에게 아이템을 투자하는 메서드
    public void InvestItem(Character character, ItemSO item)
    {
        // 투자 데이터에서 해당 캐릭터와 일치하는 카드를 찾기
        InvestmentData investData = FindInvestmentData(character);

        if (investData != null)
        {
            // 아이템을 InvestmentData의 items 리스트에 추가
            investData.items.Add(item);
            Debug.Log("Added item to character: " + character.name);
        }
        else
        {
            Debug.LogWarning("No matching character found for item investment.");
        }
    }

    // 캐릭터에 맞는 InvestmentData를 찾는 메서드
    private InvestmentData FindInvestmentData(Character character)
    {
        // InvestmentDatas 리스트에서 캐릭터와 일치하는 카드를 찾음
        foreach (InvestmentData investData in InvestmentDatas)
        {
            if (investData.Card.character.character == character.Info)
            {
                return investData;
            }
        }
        return null; // 일치하는 카드가 없으면 null 반환
    }
}

// 투자 데이터를 담는 클래스
public class InvestmentData
{
    public Card_ Card; // 카드와 연동된 데이터
    public Currency cost = new Currency(CurrencyType.Chip, 0);
    public List<ItemSO> items = new List<ItemSO>(); // 투자한 아이템 목록
}

