using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI_ : MonoBehaviour, Containable // 아이템 상세 정보 UI
{
    public Image image;
    public TextMeshProUGUI text;
    public Button tradeButton;
    public TextMeshProUGUI buttonText; 
    public bool isInInventory { get; set; }

    private Shop_ shop;
    private Investment_ invest;
    ItemSO selectedItem;
    string BuyText = "구매하기";
    string SellText = "판매하기";
    string EquipText = "후원하기";
    string UnEquipText = "회수하기";

    public void Initialize()
    {
        shop = GameManager_.instance.shop;
        invest = GameManager_.instance.investment;
    }

    public void SetAndActiveInfomation(ItemSO item)
    {
        image.sprite = item.sprite;
        text.text = item.description;
        selectedItem = item;
        isInInventory = item.isOwned;

        ActiveButton();
    }

    public void ActiveButton()
    {
        switch (UIManager_.Instance.currentUIType)
        {
            case UIType_.CharacterProfile:
                invest.Initialize();
                if(isInInventory)
                {
                    ActiveEquipButton();
                }
                else
                {
                    ActiveUnEquipeButton();
                }
                break;
            case UIType_.ShopUI:
                shop.Initialize();
                if(isInInventory)
                {
                    ActiveSellButton();
                } 
                else
                {
                    ActiveBuyButton();
                }
                break;
        }
    }

    #region Button Event Handler

    public void OnBuyButtonClick()
    {
        shop.BuyItem(selectedItem);
        Debug.Log("성공적으로 버튼에 구매 기능이 부여되었습니다.");
    }

    public void OnSellButtonClick()
    {
        shop.SellItem(selectedItem);
        Debug.Log("성공적으로 버튼에 판매 기능이 부여되었습니다.");
    }

    public void OnEquipButtonClick()
    {
        invest.InvestItem(selectedItem);
        Debug.Log("성공적으로 버튼에 장비 기능이 부여되었습니다.");
    }

    public void OnUnEquipButtonClick()
    {
        invest.CancelInvestItem(selectedItem);
        Debug.Log("성공적으로 버튼에 해제 기능이 부여되었습니다.");
    }

    #endregion

    public void ActiveBuyButton()
    {
        buttonText.text = BuyText;
        tradeButton.onClick.RemoveAllListeners();
        tradeButton.onClick.AddListener(OnBuyButtonClick);

        Debug.Log("성공적으로 버튼에 '구매'가 할당되었습니다.");
    }

    public void ActiveSellButton()
    {
        buttonText.text = SellText;
        tradeButton.onClick.RemoveAllListeners();
        tradeButton.onClick.AddListener(OnSellButtonClick);
        
        Debug.Log("성공적으로 버튼에 '구매'가 할당되었습니다.");
    }

    public void ActiveEquipButton()
    {
        buttonText.text = EquipText;
        tradeButton.onClick.RemoveAllListeners();
        tradeButton.onClick.AddListener(OnEquipButtonClick);

        Debug.Log("성공적으로 버튼에 '장착'이 할당되었습니다.");
    }

    public void ActiveUnEquipeButton()
    {
        buttonText.text = UnEquipText;
        tradeButton.onClick.RemoveAllListeners();
        tradeButton.onClick.AddListener(OnUnEquipButtonClick);

        Debug.Log("성공적으로 버튼에 '해제'가 할당되었습니다.");
    }
}
