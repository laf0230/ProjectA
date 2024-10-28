using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum UIType
{
    Map,
    Select,
    Invest,
    Shop,
}

public enum UIType_
{
    ManagementUI,
    ShopUI,
    CharacterProfile,
    CharacterStanding,
    ItemInfo,
    Calculation,
    CharacterSelect
}

public class UIManager_ : MonoBehaviour
{
    public static UIManager_ Instance { get; private set; }

    public List<UIGroup> uIGroups = new List<UIGroup>();
    public InventoryUI_ inventoryUI;
    public ShopUI_ shopUI;
    public ItemInfoUI_ itemInfoUI;
    public InvestmentUI_ investmentUI;
    public InvestCalcUI investCalcUI;
    public StandingUI standingUI;
    public GameObject WorldUI;
    public Image StandingImage;
    public Sprite RedBisonFI;
    public Sprite DemoFI;
    public Sprite FlaFlaFI;
    public UIType currentUIType { get; set; }

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
        shopUI.Initialize();
    }

    private void FixedUpdate()
    {

        UpdateCurrencyUI();
    }

    public void GameStart()
    {
        StartCoroutine(IEStart());
    }

    public IEnumerator IEStart()
    {
        uIGroups.ForEach((ui) => ui.Close());

         WorldUI.SetActive(true);
         yield return new WaitForSeconds(3);
         WorldUI.SetActive(false);


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

            if (UI.type_ == UIType_.charactersele)
                UI.Open();
        }
    }

    public UIGroup GetUIGroup(UIType_ uiType)
    {
        // UIGroups_에 uiType과 같은 타입을 가진 오브젝트가 있다면 해당 오브젝트에 있는 UIList 속 오브젝트를 껏다 킬 수 있는 오브젝트를 할당할 것
        foreach (UIGroup item in uIGroups)
        {
            if(item.type_ == uiType)
            {
                return item;
            }
        }

        Debug.LogError("해당 UI그룹은 존재하지 않습니다.");
        return null;
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
        // 보유 소지금 표시
        /*
        foreach (var item in currencyUis)
        {
            if (item == null)
                return;

            if (item.currencyType == CurrencyType.Gold)
            {
                item.DisplayCurrency(GameManager_.instance.player.gold.amount);
            }
            else
            {
                item.DisplayCurrency(GameManager_.instance.player.gold.amount);
            }
        }
        */
    }
}


[System.Serializable]
public class UIGroup
{
    public string uIName;
    public UIType type;
    public UIType_ type_;
    public List<GameObject> uiList;
    public List<Button> enableButtons;
    public List<Button> disableButtons;
    public bool isActive { get; set; }

    public void Open()
    {
        foreach (GameObject ui in uiList)
        {
            ui.SetActive(true);
        }
        isActive = true;
    }

    public void Close()
    {
        foreach (GameObject ui in uiList)
        {
            ui.SetActive(false);
        }
        isActive = false;
    }

    public void Active(bool active)
    {
        isActive = active;

        if (active)
            Open();
        else
            Close();
    }

    public void AddButton(Button button, bool active)
    {
        if (active)
        {
            button.onClick.AddListener(Open);
        } else
        {
            button.onClick.AddListener(Close);
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

public class Invest_
{
    Player player; // 투자자
    public CharacterInfoSO characterInfo; // 투자 타겟
    

}

[System.Serializable]
public class Player_
{
    public string name;
    public Sprite profileImage;
    public List<Invest_> invests;
    public Currency chip = new Currency(CurrencyType.Chip);
    public Currency gold = new Currency(CurrencyType.Gold);
    public Inventory_ inventory;

    private Invest currentInvest;
}

[field: System.Serializable]
public class Invest
{
    public Player_ Player; // 투자자
    public CharacterInfoSO characterInfo; // 투자 대상
    public Currency fundedGold = new Currency(CurrencyType.Gold); // 투자금
    public List<ItemSO> fundedItem; // 투자 아이템
}
