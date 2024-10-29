using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ : MonoBehaviour
{
    public virtual void Open() { }
    public virtual void Close() { }
}


// 이후에 사용할 UI 부모 클래스
/*
public class UIF
{
    public bool isAcive;
    public virtual void ActiveUI(bool Active) { }
}
*/