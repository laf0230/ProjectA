using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartBackgroundUIAnimTrigger : MonoBehaviour
{
    public GameStartUI GameStartUI;

    // �ִϸ��̼� Ʈ����
    public void ActiveGameStartUI()
    {
        GameStartUI.ActiveMainUI(true);
    }
}
