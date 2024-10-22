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

            if (UI.uIName == "ĳ���� ����â")
                UI.Open();
            else if (UI.uIName == "����â")
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



public class InvestManager_ : MonoBehaviour // ĳ���� ���� â
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
        // TODO: ���ڱ� ��� UI Ȱ��ȭ
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

    // �÷��̾ ������ ������ ���
    public List<ItemSO> ownedItems;

    // ĳ���Ϳ� �����ϴ� �޼���
    public void InvestCharacter(Character character, int cost)
    {
        // ���� �����Ϳ��� �ش� ĳ���Ϳ� ��ġ�ϴ� ī�带 ã��
        InvestmentData investData = FindInvestmentData(character);

        if (investData != null)
        {
            // ������ �ݾ��� ����, ���� Ĩ���� Ŭ ��� ������ ��ŭ�� ����
            if (cost > Chip)
            {
                cost = Chip;
            }

            // �ش� ī���� InvestmentData �� cost.amount�� �� ����
            investData.cost.amount = cost;
            Debug.Log("Invested " + cost + " into character: " + character.name);

            // ���� �߰� ���� �ۼ� ����
        }
        else
        {
            Debug.LogWarning("No matching character found for investment.");
        }
    }

    // ĳ���Ϳ��� �������� �����ϴ� �޼���
    public void InvestItem(Character character, ItemSO item)
    {
        // ���� �����Ϳ��� �ش� ĳ���Ϳ� ��ġ�ϴ� ī�带 ã��
        InvestmentData investData = FindInvestmentData(character);

        if (investData != null)
        {
            // �������� InvestmentData�� items ����Ʈ�� �߰�
            investData.items.Add(item);
            Debug.Log("Added item to character: " + character.name);
        }
        else
        {
            Debug.LogWarning("No matching character found for item investment.");
        }
    }

    // ĳ���Ϳ� �´� InvestmentData�� ã�� �޼���
    private InvestmentData FindInvestmentData(Character character)
    {
        // InvestmentDatas ����Ʈ���� ĳ���Ϳ� ��ġ�ϴ� ī�带 ã��
        foreach (InvestmentData investData in InvestmentDatas)
        {
            if (investData.Card.character.character == character.Info)
            {
                return investData;
            }
        }
        return null; // ��ġ�ϴ� ī�尡 ������ null ��ȯ
    }
}

// ���� �����͸� ��� Ŭ����
public class InvestmentData
{
    public Card_ Card; // ī��� ������ ������
    public Currency cost = new Currency(CurrencyType.Chip, 0);
    public List<ItemSO> items = new List<ItemSO>(); // ������ ������ ���
}

