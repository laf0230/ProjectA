using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject itemSlotPrefab; // ���� ������
    public GameObject itemContainer; // �������� ���� ���� (UI�� �θ� GameObject)
    private BaseShop shop; // Shop ����

    private void Start()
    {
        shop = FindObjectOfType<BaseShop>();
        UpdateShopUI();
    }

    public void UpdateShopUI()
    {
        // ���� ���� ����
        foreach (Transform child in itemContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // �������� �Ǹ� ������ ���������� ���� ����
        foreach (ItemSO itemData in shop.availableItems)
        {
            GameObject newSlot = Instantiate(itemSlotPrefab, itemContainer.transform);
            SlotUI itemSlotUI = newSlot.GetComponent<SlotUI>();
            itemSlotUI.SetItem(itemData, this); // ItemSlotUI�� ItemSO ����
        }
    }

    public void BuyItem(ItemSO itemData, int quantity = 1)
    {
        shop.BuyItem(itemData, quantity);
        UpdateShopUI(); // ���� UI ������Ʈ
    }
}

