using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum TradeBtnType
{
    sell,
    buy
}

public class ItemBuyBtn : MonoBehaviour
{
    [SerializeField] private Button Button;
    [SerializeField] private Button ShopItem;
    [SerializeField] private GameObject Item1;
    [SerializeField] private GameObject infomation;
    [SerializeField] private GameObject Inventory;
    [SerializeField] private GameObject CharacterEquipement;
    private bool isShop = true;
    private Button onInventoryButton;
    private Button onCharacterEquipement;
    public TradeBtnType Type;

    private void Start()
    {
            SetButtonSetting();
            ShopItem.onClick.AddListener(OnInfoActive);
    }

    public void SetButtonSetting()
    {
        if(Type == TradeBtnType.buy)
            Button.onClick.AddListener(OnBuyButtonClick);
        else if(Type == TradeBtnType.sell)
            Button.onClick.AddListener(OnSellButtonClick);
    }

    public void OnBuyButtonClick()
    {
        Item1.SetActive(true);
    }

    public void OnSellButtonClick()
    {
        Item1.SetActive(false);
    }

    public void OnInfoActive()
    {
        infomation.SetActive(true);
        onInventoryButton = Inventory.GetComponent<Button>();
        onInventoryButton.onClick.AddListener(OnCharacterEquipmentActive);
    }

    public void OnCharacterEquipmentActive()
    {
        CharacterEquipement.SetActive(true);
        onCharacterEquipement = CharacterEquipement.GetComponent<Button>();
        onCharacterEquipement.onClick.AddListener(OnCharacterEquipmentDeActie);
        Item1.SetActive(false);
    }

    public void OnCharacterEquipmentDeActie()
    {
        CharacterEquipement.SetActive(false);
        Inventory.SetActive(true);
    }
}
