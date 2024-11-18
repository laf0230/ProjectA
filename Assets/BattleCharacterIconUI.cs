using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleCharacterIconUI : MonoBehaviour
{
    public CardSO card;
    public Image image;
    public Slider healthSlider;

    // 초기값 설정 및 카드 설정
    public void Initialize(CardSO card)
    {
        this.card = card;

        image.sprite = card.profile;
        healthSlider.value = 1;
    }
}
