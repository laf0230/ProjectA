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
            Debug.Log($"�������� ������ ���� ��庸�� ������ ���Ƽ� ���Ű� �Ұ����մϴ�." +
                $" ������: {GameManager_.instance.player.gold.amount} ����: {selectedItem.BuyPrise}");
            return;
        }
        // ������ ����â ��Ȱ��ȭ
        UIManager_.Instance.itemInfoUI.gameObject.SetActive(false);

        // ���� ���� ����
        GameManager_.instance.player.gold.SpendCurrency(selectedItem.BuyPrise);

        // �κ��丮�� ������ �߰�
        inventory.AddItem(selectedItem);

        UIManager_.Instance.shopUI.UpdateUI();
    }

    public void SellItem(ItemSO selectedItem)
    {
        GameManager_.instance.player.gold.amount += selectedItem.SellPrise;
        // ������ ����â ��Ȱ��ȭ
        
        // �κ��丮���� ������ ����
        inventory.RemoveItem(selectedItem);

        UIManager_.Instance.shopUI.UpdateUI();
    }
}
