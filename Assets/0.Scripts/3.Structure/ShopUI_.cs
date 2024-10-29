using System.Collections.Generic;
using UnityEngine;

public class ShopUI_ : MonoBehaviour
{
    [SerializeField] private GameObject slot;
    [SerializeField] private GameObject itemContainer;
    private List<GameObject> items = new List<GameObject>();
    private Shop_ shop;

    private void Start()
    {
        UpdateUI();
    }

    public void Initialize()
    {
        if (items != null)
        {
            shop = GameManager_.instance.shop;

            foreach (GameObject framedItem in items)
            {
                Destroy(framedItem);
            }
        }
    }

    public void UpdateUI()
    {
        Initialize();

        foreach (var item in GameManager_.instance.shop.items)
        {
            var createdSlotObject = Instantiate(slot, itemContainer.transform);
            ItemSlotUI slotUI = createdSlotObject.GetComponent<ItemSlotUI>();

            slotUI.isInInventory = false;
            slotUI.SetItem(item);

            items.Add(createdSlotObject);
        }
    }
}
