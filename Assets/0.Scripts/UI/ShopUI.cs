using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject itemSlotPrefab; // 슬롯 프리팹
    public GameObject itemContainer; // 아이템을 담을 공간 (UI의 부모 GameObject)
    private BaseShop shop; // Shop 참조

    private void Start()
    {
        shop = FindObjectOfType<BaseShop>();
        UpdateShopUI();
    }

    public void UpdateShopUI()
    {
        // 기존 슬롯 제거
        foreach (Transform child in itemContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // 상점에서 판매 가능한 아이템으로 슬롯 생성
        foreach (ItemSO itemData in shop.availableItems)
        {
            GameObject newSlot = Instantiate(itemSlotPrefab, itemContainer.transform);
            SlotUI itemSlotUI = newSlot.GetComponent<SlotUI>();
            itemSlotUI.SetItem(itemData, this); // ItemSlotUI에 ItemSO 설정
        }
    }

    public void BuyItem(ItemSO itemData, int quantity = 1)
    {
        shop.BuyItem(itemData, quantity);
        UpdateShopUI(); // 상점 UI 업데이트
    }
}

