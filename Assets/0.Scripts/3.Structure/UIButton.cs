using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public UIType_ uiType;
    private UIGroup allocatedUIGroup;
    private Button button;

    void Initialize(UIType_ uIType_)
    {
        this.uiType = uIType_;

        button = GetComponent<Button>();
        // UI 그룹을 할당함
        allocatedUIGroup = UIManager_.Instance.GetUIGroup(uiType);
        // UI를 켜고 끌 수 있는 스위치 등록
        button.onClick.AddListener(() => { allocatedUIGroup.Active(!allocatedUIGroup.isActive); });
    }
}
