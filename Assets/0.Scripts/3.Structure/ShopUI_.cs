using System.Collections.Generic;
using UnityEngine;

public class ShopUI_ : MonoBehaviour
{
    [SerializeField] private GameObject slot;
    [SerializeField] private GameObject itemContainer;
    private List<GameObject> items = new List<GameObject>();
    private Shop_ shop;

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
            var createdSlot = Instantiate(slot, itemContainer.transform);
            createdSlot.GetComponent<ItemSlotUI>().CreateItemSlotUI(item);
            
            items.Add(createdSlot);
        }
    }
}

