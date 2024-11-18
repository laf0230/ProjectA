using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleCharacterIconUI : MonoBehaviour
{
    public CardSO card;
    public Image image;
    public Slider healthSlider;

    // �ʱⰪ ���� �� ī�� ����
    public void Initialize(CardSO card)
    {
        this.card = card;

        image.sprite = card.profile;
        healthSlider.value = 1;
    }
}
