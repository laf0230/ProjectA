using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleCharacterIconUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public CardSO card;
    public Image image;
    public Slider healthSlider;
    private bool isInteractable = false;
    private Character character;

    // �ʱⰪ ���� �� ī�� ����
    public void Initialize(CardSO card)
    {
        this.card = card;

        image.sprite = this.card.profile;
        healthSlider.value = 1; // �ʱ� �� 1
    }

    private void Update()
    {
        if (isInteractable)
        {
            UpdateCharacterHealth(character.Info.Status.MaxHealth, character.CurrentHealth);
        } else
        {
            DisableUI();
        }
    }

    public void UpdateCharacterHealth(float maxHealth, float health)
    {
        healthSlider.value = health / maxHealth;
    }

    public void DisableUI()
    {
        // ĳ���Ͱ� ������� �� UI�� ��ȣ�ۿ��� ���� �������� ȸ�� ������ ����
        image.color = Color.gray;
        isInteractable = false;
        gameObject.SetActive(false);
    }

    public void ActiveUI()
    {
        image.color = Color.white;
        isInteractable = true;

        character = GameManager_.instance.fieldManager.GetCharacter(card.character);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // �� UI ����
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // �� UI ����
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("�ȳ��ϼ���!");

        // ī�޶� ��Ŀ�� ����
        if (character == null)
            Debug.Log("ĳ���͸� ȣ���� �� �����ϴ�");
        GameManager_.instance.playerView.SetFocus(character.gameObject);
    }
}
