using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ : MonoBehaviour
{
    public UIGroup characterManageUI;
    public UIGroup shopUI;
    public List<UIGroup> uiGroups = new List<UIGroup>();
}


// 이후에 사용할 UI 부모 클래스
/*
public class UIF
{
    public bool isAcive;
    public virtual void ActiveUI(bool Active) { }
}
*/