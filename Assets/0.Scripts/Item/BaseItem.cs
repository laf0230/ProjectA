using UnityEngine;

public class BaseItem
{
    public ItemSO itemData; // ScriptableObject�� ����
    public int quantity; // ������ ����

    public BaseItem(ItemSO itemData, int quantity = 1)
    {
        this.itemData = itemData;
        this.quantity = quantity;
    }

    public string Name => itemData.name; // ������ �̸�
    public string Description => itemData.Description; // ������ ����
    public Sprite Icon => itemData.sprite; // ������ ������
    public int BuyPrice => itemData.BuyPrise; // ���� ����
    public int SellPrice => itemData.SellPrise; // �Ǹ� ����
    public bool IsSellable => itemData.IsSellable; // �Ǹ� ���� ����
}

