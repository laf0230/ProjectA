using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    public Image itemIcon; // ������ ������
    public Text itemNameText; // ������ �̸� �ؽ�Ʈ
    public Button buyButton; // ���� ��ư
    private ItemSO currentItem; // ���� ������

    public void SetItem(ItemSO itemData, ShopUI shopUI)
    {
        currentItem = itemData;
        itemIcon.sprite = itemData.sprite; // ������ ����
        itemNameText.text = itemData.name; // �̸� ����

        // ���� ��ư Ŭ�� �̺�Ʈ �߰�
        buyButton.onClick.RemoveAllListeners(); // ���� ������ ����
        buyButton.onClick.AddListener(() => shopUI.BuyItem(currentItem)); // ���� Ŭ�� �� ȣ��
    }
}

