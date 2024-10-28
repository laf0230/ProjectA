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
        // UI �׷��� �Ҵ���
        allocatedUIGroup = UIManager_.Instance.GetUIGroup(uiType);
        // UI�� �Ѱ� �� �� �ִ� ����ġ ���
        button.onClick.AddListener(() => { allocatedUIGroup.Active(!allocatedUIGroup.isActive); });
    }
}
