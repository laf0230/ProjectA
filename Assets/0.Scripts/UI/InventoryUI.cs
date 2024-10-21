using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab; // 슬롯 프리팹
    public GameObject itemContainer; // 아이템을 담을 공간 (UI의 부모 GameObject)

    private Inventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        UpdateUI();
    }

    public void UpdateUI()
    {
        // 기존 슬롯 제거
        foreach (Transform child in itemContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // 인벤토리 아이템으로 슬롯 생성
        foreach (BaseItem item in inventory.GetItems())
        {
            GameObject newSlot = Instantiate(slotPrefab, itemContainer.transform);
            ItemUI itemUI = newSlot.GetComponent<ItemUI>();
            itemUI.SetItem(item); // ItemUI에 BaseItem 설정
        }
    }
}

