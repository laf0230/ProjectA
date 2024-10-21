using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterItemSlotUI : MonoBehaviour
{
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void SetSlotUI(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
