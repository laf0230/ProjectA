using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    public Image itemIcon; // 아이템 아이콘
    public Text itemNameText; // 아이템 이름 텍스트
    public Button buyButton; // 구매 버튼
    private ItemSO currentItem; // 현재 아이템

    public void SetItem(ItemSO itemData, ShopUI shopUI)
    {
        currentItem = itemData;
        itemIcon.sprite = itemData.sprite; // 아이콘 설정
        itemNameText.text = itemData.name; // 이름 설정

        // 구매 버튼 클릭 이벤트 추가
        buyButton.onClick.RemoveAllListeners(); // 기존 리스너 제거
        buyButton.onClick.AddListener(() => shopUI.BuyItem(currentItem)); // 구매 클릭 시 호출
    }
}

