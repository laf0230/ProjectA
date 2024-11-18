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

    // 초기값 설정 및 카드 설정
    public void Initialize(CardSO card)
    {
        this.card = card;

        image.sprite = this.card.profile;
        healthSlider.value = 1; // 초기 값 1
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
        // 캐릭터가 사망했을 때 UI의 상호작용을 막고 프로필이 회색 빛으로 변함
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
        // 상세 UI 포시
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 상세 UI 제거
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("안녕하세요!");

        // 카메라 포커싱 변경
        if (character == null)
            Debug.Log("캐릭터를 호출할 수 없습니다");
        GameManager_.instance.playerView.SetFocus(character.gameObject);
    }
}
