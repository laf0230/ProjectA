using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public Image itemIcon; // ������ ������
    public Text itemNameText; // ������ �̸� �ؽ�Ʈ
    public Button buyButton; // ���� ��ư
    public Button sellButton; // �Ǹ� ��ư

    private ItemSO currentItem; // ���� ������

    // ������ ���� �޼���
    public void SetItem(ItemSO itemData, BaseShop shop)
    {
        currentItem = itemData;
        itemIcon.sprite = itemData.sprite; // ������ ����
        itemNameText.text = itemData.name; // �̸� ����

        // ���� ��ư Ŭ�� �̺�Ʈ �߰�
        buyButton.onClick.RemoveAllListeners(); // ���� ������ ����
        buyButton.onClick.AddListener(() => shop.BuyItem(currentItem)); // ���� Ŭ�� �� ȣ��

        // �Ǹ� ��ư Ŭ�� �̺�Ʈ �߰�
        sellButton.onClick.RemoveAllListeners(); // ���� ������ ����
        sellButton.onClick.AddListener(() => shop.SellItem(currentItem)); // �Ǹ� Ŭ�� �� ȣ��
    }
}

