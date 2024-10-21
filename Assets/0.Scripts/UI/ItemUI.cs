using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Image itemIcon; // 아이템 아이콘
    public Text itemNameText; // 아이템 이름 텍스트
    public Text itemDescriptionText; // 아이템 설명 텍스트
    public Text itemQuantityText; // 아이템 수량 텍스트
    private BaseItem currentItem; // 현재 아이템

    public void SetItem(BaseItem item)
    {
        currentItem = item;
        itemIcon.sprite = item.Icon; // 아이콘 설정
        itemNameText.text = item.Name; // 이름 설정
        itemDescriptionText.text = item.description; // 설명 설정
        itemQuantityText.text = $"x{item.quantity}"; // 수량 설정
    }

    public BaseItem GetItem()
    {
        return currentItem;
    }
}

