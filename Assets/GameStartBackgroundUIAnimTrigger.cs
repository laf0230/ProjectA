using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartBackgroundUIAnimTrigger : MonoBehaviour
{
    public GameStartUI GameStartUI;

    // 애니메이션 트리거
    public void ActiveGameStartUI()
    {
        GameStartUI.ActiveMainUI(true);
    }
}
