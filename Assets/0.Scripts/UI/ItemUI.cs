using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Image itemIcon; // ������ ������
    public Text itemNameText; // ������ �̸� �ؽ�Ʈ
    public Text itemDescriptionText; // ������ ���� �ؽ�Ʈ
    public Text itemQuantityText; // ������ ���� �ؽ�Ʈ
    private BaseItem currentItem; // ���� ������

    public void SetItem(BaseItem item)
    {
        currentItem = item;
        itemIcon.sprite = item.Icon; // ������ ����
        itemNameText.text = item.Name; // �̸� ����
        itemDescriptionText.text = item.description; // ���� ����
        itemQuantityText.text = $"x{item.quantity}"; // ���� ����
    }

    public BaseItem GetItem()
    {
        return currentItem;
    }
}

