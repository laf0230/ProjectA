using System.Collections.Generic;
using UnityEngine;

public class ShopUI_ : MonoBehaviour
{
    [SerializeField] private GameObject slot;
    [SerializeField] private GameObject itemContainer;
    private List<GameObject> items;
    private Shop_ shop;

    private void Start()
    {
        shop = FindObjectOfType<Shop_>();
    }

    public void UpdateUI()
    {

        foreach (GameObject framedItem in items)
        {
            Destroy(framedItem);
        }

        foreach (var item in shop.items)
        {
            var createdSlot = Instantiate(slot, itemContainer.transform);

            createdSlot.GetComponent<ItemSlotUI>().CreateItemSlotUI(item, true);
        }
    }
}

