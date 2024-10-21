using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image itemIcon;
    private BaseItem currentItem;

    // 슬롯에 아이템 설정
    public void SetItem(BaseItem item)
    {
        currentItem = item;
        itemIcon.sprite = item.Icon; // 아이콘을 UI에 표시
    }

    // 현재 슬롯의 아이템 반환
    public BaseItem GetItem()
    {
        return currentItem;
    }
}
