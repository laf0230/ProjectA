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

[SerializeField]
public enum UIType_
{
    ManagementUI,
    ShopUI,
    CharacterProfile,
    CharacterStanding,
    ItemInfo,
    Calculation,
    CharacterSelect,
}

public class UIManager_ : MonoBehaviour
{
    public static UIManager_ Instance { get; private set; }

    public List<UIGroup> uIGroups = new List<UIGroup>();
    public List<CurrencyUI> currencyUIs;
    public GameStartUI gameStartUI;
    public MenuSettingUI menuSettingUI;
    public RectTransform rect;
    public Button gameStartButton;
    public InventoryUI_ inventoryUI;
    public ShopUI_ shopUI;
    public ItemInfoUI_ itemInfoUI;
    public InvestmentUI_ investmentUI;
    public InvestCalcUI investCalcUI;
    public StandingUI standingUI;
    public ProfileContainer profileContainer;
    public CardContainer cardContainer;
    public RankingUI rankingUI;
    public GameObject WorldUI;
    public GameObject investWarningUI;
    public OnFieldUI onFieldUI;
    public AsYourWishUI resultUI;
    public AsYourWishFailUI resultFailUI;
    public GameObject dontInvest;
    public Button battleStartButton;
    public Image StandingImage;
    public Sprite lockIcon;
    public Sprite unLockIcon;
    public UIType_ currentUIType { get; set; }

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
        }
    }

    private void Start()
    {
        gameStartButton.onClick.AddListener(GameStart);
        battleStartButton.onClick.RemoveAllListeners();
        battleStartButton.onClick.AddListener(OnBattleStartButtonClick);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            menuSettingUI.gameObject.SetActive(!menuSettingUI.gameObject.activeSelf);
    }

    private void FixedUpdate()
    {
        UpdateCurrencyUI();
    }

    public void OnBattleStartButtonClick()
    {
        if (GameManager_.instance.investment.isInvested)
        {
            Debug.Log("������");
            GameManager_.instance.StartBattle();
            return;
        }
        else
        {
            Debug.Log("���� ����");
            StartCoroutine(IEFlickDontInvest());
            return;
        }
    }

    public IEnumerator IEFlickDontInvest()
    {
        dontInvest.SetActive(true);
        yield return new WaitForSeconds(1);
        dontInvest.SetActive(false);
    }

    // ���� ���� UI���� ��ư �̺�Ʈ�� ����
    public void GameStart()
    {
        gameStartUI.gameObject.SetActive(false);
        StartCoroutine(IEStart());
    }

    public IEnumerator IEStart()
    {
        // rankingUI.ResetKillCount();

        uIGroups.ForEach((ui) => ui.Close());

        WorldUI.SetActive(true);
        yield return new WaitForSeconds(3);
        WorldUI.SetActive(false);


        foreach (UIGroup UI in uIGroups)
        {

            // Ŭ���� ��ư��
            foreach (var btn in UI.disableButtons)
            {
                btn.onClick.AddListener(UI.Close);
            }

            // ���� ��ư��
            foreach (var btn in UI.enableButtons)
            {
                btn.onClick.AddListener(UI.Open);
            }

            if (UI.type_ == UIType_.CharacterSelect)
                UI.Open();
        }
    }

    public UIGroup GetUIGroup(UIType_ uiType)
    {
        // UIGroups_�� uiType�� ���� Ÿ���� ���� ������Ʈ�� �ִٸ� �ش� ������Ʈ�� �ִ� UIList �� ������Ʈ�� ���� ų �� �ִ� ������Ʈ�� �Ҵ��� ��
        foreach (UIGroup item in uIGroups)
        {
            if(item.type_ == uiType)
            {
                return item;
            }
        }

        Debug.LogError("�ش� UI�׷��� �������� �ʽ��ϴ�.");
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

    public void ActiveGameEndUI(bool active)
    {
        rankingUI.DesplayRanking(active);
    }

    public void UpdateCurrencyUI()
    {
        // ���� ������ ǥ��
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



public class InvestManager_ : MonoBehaviour // ĳ���� ���� â
{
    public static InvestManager_ instance;
}

public class Invest_
{
    public CharacterInfoSO characterInfo; // ���� Ÿ��
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

public enum CurrencyType
{
    Gold,
    Chip,
}

[SerializeField]
public class Currency
{
    public CurrencyType type;
    public int amount = 0;

    public Currency(CurrencyType type)
    {
        this.type = type;
    }

    public void AddCurrency(int value)
    {
        amount += value;
        CurrencyUIUpdate();
    }

    public void SpendCurrency(int value)
    {
        amount -= value;
        CurrencyUIUpdate();
    }

    public int GetCurrency()
    {
        return amount;
    }

    public void CurrencyUIUpdate()
    {
        foreach (var item in UIManager_.Instance.currencyUIs)
        {
            switch (item.type)
            {
                case CurrencyType.Gold:
                    item.DisplayCurrency(GameManager_.instance.player.gold.GetCurrency());
                    break;
                case CurrencyType.Chip:
                    item.DisplayCurrency(GameManager_.instance.player.chip.GetCurrency());
                    break;
                    // ���Ŀ� ���� Ƚ������ currency�� ���
            }
        }
    }
}

[field: System.Serializable]
public class Invest
{
    public Player_ Player; // ������
    public CharacterInfoSO characterInfo; // ���� ���
    public List<ItemSO> fundedItem; // ���� ������
}
