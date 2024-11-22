using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemInfoUI : MonoBehaviour
{
    TextMeshProUGUI Infomation;

    private void Start()
    {
        Infomation = GetComponent<TextMeshProUGUI>();
    }

    public void SetInfomation(ItemSO info)
    {
        Infomation.text = info.description;
    }
}
