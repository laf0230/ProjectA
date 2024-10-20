using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemSO itemSO;
    public Button Button;

    public void Start()
    {
        Button.onClick.AddListener(() =>
        {
            GameManager.Instance.UIManager.itemInfo.SetInfomation(itemSO);
        });
    }
}
