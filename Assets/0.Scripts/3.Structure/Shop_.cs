using System.Collections.Generic;
using UnityEngine;

public class Shop_ : MonoBehaviour
{
    public List<ItemSO> items;
    private Inventory_ inventory;

    private void Start()
    {
        // inventory = FindObjectOfType<Inventory_>();
    }

    public void Initialize(Inventory_ inventory)
    {
        if (inventory == null) { Debug.Log("Inventory is null"); };
        this.inventory = inventory;
            if(UIManager_.Instance.shopUI == null) { Debug.Log("ShopUI is null"); };
        UIManager_.Instance.shopUI.UpdateUI();
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
        if (GameManager_.instance.player.gold.amount < selectedItem.BuyPrise)
        {
            Debug.Log($"아이템의 가격이 보유 골드보다 가격이 높아서 구매가 불가능합니다." +
                $" 보유량: {GameManager_.instance.player.gold.amount} 가격: {selectedItem.BuyPrise}");
            return;
        }
        // 아이템 정보창 비활성화
        UIManager_.Instance.itemInfoUI.gameObject.SetActive(false);

        // 보유 가격 변동
        GameManager_.instance.player.gold.SpendCurrency(selectedItem.BuyPrise);

        // 인벤토리에 아이템 추가
        inventory.AddItem(selectedItem);

        UIManager_.Instance.shopUI.UpdateUI();
    }

    public void SellItem(ItemSO selectedItem)
    {
        GameManager_.instance.player.gold.amount += selectedItem.SellPrise;
        // 아이템 정보창 비활성화
        
        // 인벤토리에서 아이템 제거
        inventory.RemoveItem(selectedItem);

        UIManager_.Instance.shopUI.UpdateUI();
    }
}
