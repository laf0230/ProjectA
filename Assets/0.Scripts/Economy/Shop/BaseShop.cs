using System.Collections.Generic;
using UnityEngine;

public class BaseShop : MonoBehaviour
{
    public List<ItemSO> availableItems = new List<ItemSO>(); // 상점에서 판매할 아이템 목록
    private Inventory inventory; // 플레이어 인벤토리 참조

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    public void BuyItem(ItemSO itemData, int quantity = 1)
    {
        if (inventory != null && CanAfford(itemData, quantity))
        {
            inventory.AddItem(itemData, quantity); // 아이템 추가
            Debug.Log($"아이템 구매됨: {itemData.name}");
        }
        else
        {
            Debug.Log("구매할 수 없는 상태입니다.");
        }
    }

    public void SellItem(ItemSO itemData, int quantity = 1)
    {
        if (inventory != null && inventory.HasItem(itemData, quantity))
        {
            inventory.RemoveItem(itemData, quantity); // 아이템 제거
            Debug.Log($"아이템 판매됨: {itemData.name}");
        }
        else
        {
            Debug.Log("판매할 수 있는 아이템이 없습니다.");
        }
    }

    public bool CanAfford(ItemSO itemData, int quantity)
    {
        // 통화 검사 로직을 추가합니다.
        return true; // 예시로 항상 true 반환
    }
}

