using UnityEngine;
using UnityEngine.UI;

public interface Containable
{
    public bool isInInventory {  get; set; }
}

public class ItemSlotUI : MonoBehaviour, Containable // 아이템 슬롯 UI
{
    Image itemFrame;
    [SerializeField] private Button button;
    [SerializeField] private Image itemImage;
    
    public ItemSO item { get; set; }
    public bool isInInventory { get; set; }

    public void Start()
    {
        itemFrame = GetComponent<Image>();
    }

    public void CreateItemSlotUI(ItemSO item)
    {
        // 아이템 할당
        this.item = item;

        // 아이템 이미지가 없을 때
        if(item.sprite != null)
            itemImage.sprite = item.sprite;

        // 아이템 UI 생성 및 활성화
        var itemInfoUI = UIManager_.Instance.itemInfoUI;
        itemInfoUI.SetAndActiveInfomation(item, UIManager_.Instance.currentUIType);
        itemInfoUI.gameObject.SetActive(true);

        // 버튼에 클릭 이벤트 부여
        var buttonE = button.onClick;
    }

    public void OnButtonClick()
    {
    }

    // 버튼 이벤트 할당 코드
    public void SetButtonAction(UnityEngine.Events.UnityAction action, bool isOnce = true)
    {
        button.onClick.AddListener(action);
        if(!isOnce)
            button.onClick.RemoveAllListeners();
    }
}
