using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public Image itemIcon; // 아이템 아이콘
    public Text itemNameText; // 아이템 이름 텍스트
    public Button buyButton; // 구매 버튼
    public Button sellButton; // 판매 버튼

    private ItemSO currentItem; // 현재 아이템

    // 아이템 설정 메서드
    public void SetItem(ItemSO itemData, BaseShop shop)
    {
        currentItem = itemData;
        itemIcon.sprite = itemData.sprite; // 아이콘 설정
        itemNameText.text = itemData.name; // 이름 설정

        // 구매 버튼 클릭 이벤트 추가
        buyButton.onClick.RemoveAllListeners(); // 기존 리스너 제거
        buyButton.onClick.AddListener(() => shop.BuyItem(currentItem)); // 구매 클릭 시 호출

        // 판매 버튼 클릭 이벤트 추가
        sellButton.onClick.RemoveAllListeners(); // 기존 리스너 제거
        sellButton.onClick.AddListener(() => shop.SellItem(currentItem)); // 판매 클릭 시 호출
    }
}

