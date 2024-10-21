using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image itemIcon;
    private BaseItem currentItem;

    // ���Կ� ������ ����
    public void SetItem(BaseItem item)
    {
        currentItem = item;
        itemIcon.sprite = item.Icon; // �������� UI�� ǥ��
    }

    // ���� ������ ������ ��ȯ
    public BaseItem GetItem()
    {
        return currentItem;
    }
}
