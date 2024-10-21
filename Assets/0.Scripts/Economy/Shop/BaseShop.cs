using System.Collections.Generic;
using UnityEngine;

public class BaseShop : MonoBehaviour
{
    public List<ItemSO> availableItems = new List<ItemSO>(); // �������� �Ǹ��� ������ ���
    private Inventory inventory; // �÷��̾� �κ��丮 ����

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    public void BuyItem(ItemSO itemData, int quantity = 1)
    {
        if (inventory != null && CanAfford(itemData, quantity))
        {
            inventory.AddItem(itemData, quantity); // ������ �߰�
            Debug.Log($"������ ���ŵ�: {itemData.name}");
        }
        else
        {
            Debug.Log("������ �� ���� �����Դϴ�.");
        }
    }

    public void SellItem(ItemSO itemData, int quantity = 1)
    {
        if (inventory != null && inventory.HasItem(itemData, quantity))
        {
            inventory.RemoveItem(itemData, quantity); // ������ ����
            Debug.Log($"������ �Ǹŵ�: {itemData.name}");
        }
        else
        {
            Debug.Log("�Ǹ��� �� �ִ� �������� �����ϴ�.");
        }
    }

    public bool CanAfford(ItemSO itemData, int quantity)
    {
        // ��ȭ �˻� ������ �߰��մϴ�.
        return true; // ���÷� �׻� true ��ȯ
    }
}

