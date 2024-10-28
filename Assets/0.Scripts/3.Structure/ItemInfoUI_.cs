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
    public UIType UIType;
    public bool isInInventory { get; set; }

    private Shop_ shop;
    private Investment_ invest;
    ItemSO selectedItem;
    string BuyText = "구매하기";
    string SellText = "판매하기";
    string EquipText = "후원하기";
    string UnEquipText = "회수하기";

    private void Start()
    {
        shop = FindObjectOfType<Shop_>();

    }

    public void SetAndActiveInfomation(ItemSO item, UIType uitype)
    {
        image.sprite = item.sprite;
        text.text = item.description;
        selectedItem = item;
        UIType = uitype;

        ActiveButton();
    }

    public void ActiveButton()
    {
        switch (UIType)
        {
            case UIType.Invest:
                if(isInInventory)
                {
                    ActiveEquipButton();
                }
                else
                {
                    ActiveUnEquipeButton();
                }
                break;
            case UIType.Shop:
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
    }

    public void OnSellButtonClick()
    {
        shop.SellItem(selectedItem);
    }

    public void OnEquipButtonClick()
    {
        invest.InvestItem(selectedItem);
    }

    public void OnUnEquipButtonClick()
    {
        invest.CancelInvestItem(selectedItem);
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
