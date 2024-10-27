using UnityEngine;

public class BaseItem
{
    public ItemSO itemData; // ScriptableObject를 참조
    public int quantity; // 아이템 수량

    public BaseItem(ItemSO itemData, int quantity = 1)
    {
        this.itemData = itemData;
        this.quantity = quantity;
    }

    public string Name => itemData.name; // 아이템 이름
    public string description => itemData.description; // 아이템 설명
    public Sprite Icon => itemData.sprite; // 아이템 아이콘
    public int BuyPrice => itemData.BuyPrise; // 구매 가격
    public int SellPrice => itemData.SellPrise; // 판매 가격
}

