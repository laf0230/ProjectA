using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI_ : MonoBehaviour // 아이템 상세 정보 UI
{
    public Image image;
    public TextMeshProUGUI text;
    public Button tradeButton;
    public TextMeshProUGUI buttonText; 
    public bool isBuy { get; set; } // 판매 여부

    private Shop_ shop;
    ItemSO selectedItem;
    string BuyText = "구매하기";
    string SellText = "판매하기";

    private void Start()
    {
        shop = FindObjectOfType<Shop_>();
    }

    public void SetAndActiveInfomation(ItemSO item)
    {
        image.sprite = item.sprite;
        text.text = item.description;
        selectedItem = item;
    }

    public void ActiveButton()
    {
        if(isBuy)
        {
            ActiveBuyButton();
        }
        else
        {
            ActiveSellButton();
        }
    }

    #region Button Event

    public void OnBuyButtonClick()
    {
        shop.BuyItem(selectedItem);
    }

    public void OnSellButtonClick()
    {
        shop.SellItem(selectedItem);
    }

    #endregion

    public void ActiveBuyButton()
    {
        buttonText.text = BuyText;
        tradeButton.onClick.RemoveAllListeners();
        tradeButton.onClick.AddListener(OnBuyButtonClick);
    }

    public void ActiveSellButton()
    {
        buttonText.text = SellText;
        tradeButton.onClick.RemoveAllListeners();
        tradeButton.onClick.AddListener(OnSellButtonClick);
    }
}
