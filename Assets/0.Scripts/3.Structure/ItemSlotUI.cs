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
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnButtonClick);
    }

    public void SetEmpty()
    {
        item = null;
        itemImage.sprite = UIManager_.Instance.unLockIcon;
        button.onClick.RemoveAllListeners();
    }

    public void SetLock()
    {
        itemImage.sprite = UIManager_.Instance.lockIcon;
        button.onClick.RemoveAllListeners();
        Debug.Log("Item Icon Locked in Investment UI");
    }

    public void OnButtonClick()
    {
        var itemInfoUI = UIManager_.Instance.itemInfoUI;
        itemInfoUI.gameObject.SetActive(true);
        itemInfoUI.isInInventory = this.isInInventory;
        itemInfoUI.SetAndActiveInfomation(item);
    }
}
