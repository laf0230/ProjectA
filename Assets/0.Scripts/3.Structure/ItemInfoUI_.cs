using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI_ : MonoBehaviour// 아이템 상세 정보 UI
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
    private Coroutine coroutine;

    public void Start()
    {
        shop = GameManager_.instance.shop;
        invest = GameManager_.instance.investment;
    }

    public void SetAndActiveInfomation(ItemSO item)
    {
        image.sprite = item.sprite;
        text.text = item.description;
        selectedItem = item;

        ActiveButton();
    }

    public void ActiveButton()
    {
        if (UIManager_.Instance.investmentUI.gameObject.activeSelf)
        {
            // 투자 창이 활성화된 경우
            if(isInInventory)
            {
                ActiveEquipButton();
            }
            else
            {
                ActiveUnEquipeButton();
            }
        }
        else if(UIManager_.Instance.shopUI.gameObject.activeSelf)
        {
                if(isInInventory)
                {
                    ActiveSellButton();
                } 
                else
                {
                    ActiveBuyButton();
                }
        }
    }

    #region Button Event Handler

    public void OnBuyButtonClick()
    {
        shop.BuyItem(selectedItem);
        UIManager_.Instance.itemInfoUI.gameObject.SetActive(false);
        Debug.Log("성공적으로 버튼에 구매 기능이 부여되었습니다.");
    }

    public void OnSellButtonClick()
    {
        shop.SellItem(selectedItem);
        UIManager_.Instance.itemInfoUI.gameObject.SetActive(false);
        Debug.Log("성공적으로 버튼에 판매 기능이 부여되었습니다.");
    }

    public void OnEquipButtonClick()
    {
        if(coroutine != null)
            StopCoroutine(coroutine);

        if (GameManager_.instance.investment.selectedCharacter.investData.isInvested)
        {
            // 투자가 된 경우
            invest.InvestItem(selectedItem);
            UIManager_.Instance.itemInfoUI.gameObject.SetActive(false);
        }
        else
        {
            // 투자가 안된 경우
            // UIManager_.Instance.itemInfoUI.gameObject.SetActive(false);
            // 경고창 띄워줌
            coroutine = StartCoroutine(FlipWarning());
        }

        Debug.Log("성공적으로 버튼에 장비 기능이 부여되었습니다.");
    }

    public void OnUnEquipButtonClick()
    {
        invest.CancelInvestItem(selectedItem);
        UIManager_.Instance.itemInfoUI.gameObject.SetActive(false);
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
        
        Debug.Log("성공적으로 버튼에 '판매'가 할당되었습니다.");
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

    public IEnumerator FlipWarning()
    {
        UIManager_.Instance.investWarningUI.SetActive(true);
        yield return new WaitForSeconds(1);
        UIManager_.Instance.investWarningUI.SetActive(false);
    }
}
