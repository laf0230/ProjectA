using System.Collections.Generic;
using UnityEngine;

public class Shop_ : MonoBehaviour
{
    public List<ItemSO> items = new List<ItemSO>();
    private Inventory_ inventory;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory_>();
    }

    public void AddItem(ItemSO item)
    {
        items.Add(item);
        UIManager_.Instance.shopUI.UpdateUI();
    }

    public void RemoveItem(ItemSO item)
    {
        items.Remove(item);
    }

    public void BuyItem(ItemSO selectedItem)
    {
        if (GameManager_.instance.player.Gold > selectedItem.BuyPrise)
            return;
        // 아이템 정보창 비활성화

        UIManager_.Instance.shopUI.UpdateUI();
    }

    public void SellItem(ItemSO selectedItem)
    {
        GameManager_.instance.player.Gold += selectedItem.SellPrise;
        // 아이템 정보창 비활성화
        
        // 인벤토리에서 아이템 제거
        inventory.RemoveItem(selectedItem);

        UIManager_.Instance.shopUI.UpdateUI();
    }
}
