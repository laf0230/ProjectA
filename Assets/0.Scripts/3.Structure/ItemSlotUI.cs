using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour // 아이템 슬롯 UI
{
    Image itemFrame;
    [SerializeField] private Button button;
    [SerializeField] private Image itemImage;
    public ItemSO item { get; set; }
    private bool isBuyable;

    public void Start()
    {
        itemFrame = GetComponent<Image>();
    }

    public void CreateItemSlotUI(ItemSO item, bool isBuyable)
    {
        if(item.sprite != null)
            itemImage.sprite = item.sprite;

        this.item = item;
        this.isBuyable = isBuyable;

        button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        var itemInfoUI = UIManager_.Instance.itemInfoUI;
        itemInfoUI.isBuy = isBuyable;
        itemInfoUI.SetAndActiveInfomation(item);
    }
}
