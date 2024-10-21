using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab; // ���� ������
    public GameObject itemContainer; // �������� ���� ���� (UI�� �θ� GameObject)

    private Inventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        UpdateUI();
    }

    public void UpdateUI()
    {
        // ���� ���� ����
        foreach (Transform child in itemContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // �κ��丮 ���������� ���� ����
        foreach (BaseItem item in inventory.GetItems())
        {
            GameObject newSlot = Instantiate(slotPrefab, itemContainer.transform);
            ItemUI itemUI = newSlot.GetComponent<ItemUI>();
            itemUI.SetItem(item); // ItemUI�� BaseItem ����
        }
    }
}

