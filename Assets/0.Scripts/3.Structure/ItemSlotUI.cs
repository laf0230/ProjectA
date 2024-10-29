using UnityEngine;
using UnityEngine.UI;

public enum ItemSlotType
{
    Sell,
    Buy,
    Equip,
    UnEquip
}

public class ItemSlotUI : MonoBehaviour // 아이템 슬롯 UI
{
    Image itemFrame;
    [SerializeField] private Button button;
    [SerializeField] private Image itemImage;

    // Lock
    public bool isLickable;
    public bool isLock { get; set; } = true; // Only Uesd On ProfileUI

    public ItemSlotType slotType;
    
    public ItemSO item { get; set; }
    public bool isInInventory { get; set; }

    public void Start()
    {
        itemFrame = GetComponent<Image>();
    }

    public void SetItem(ItemSO item)
    {
        // 아이템 할당
        this.item = item;

        // 아이템 이미지가 없을 때
        if(item.sprite != null)
            itemImage.sprite = item.sprite;

        // 버튼에 클릭 이벤트 부여
        button.onClick.AddListener(OnButtonClick);
    }

    public void SetEmpty()
    {
        item = null;
        itemImage.sprite = UIManager_.Instance.unLockIcon;
        button.onClick.RemoveAllListeners();
    }

    public void OnButtonClick()
    {
        // 아이템 UI 생성 및 활성화
        var itemInfoUI = UIManager_.Instance.itemInfoUI;
        itemInfoUI.gameObject.SetActive(true);
        itemInfoUI.isInInventory = this.isInInventory;
        itemInfoUI.SetAndActiveInfomation(item);
    }
}
