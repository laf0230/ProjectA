using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 위에 있는 캐릭터 목록에서 클릭하는 아이콘
public class ProfileUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image image;
    private CardSO characterCard; // 캐릭터 정보

    public void SetCardCharacter(CardSO character)
    {
        characterCard = character;
        image.sprite = character.profile;
        button.onClick.AddListener(OnClickButton);
    }

    // 카드 클릭 시 실행되는 메서드
    public void OnClickButton()
    {
        // 캐릭터 투자 창으로 전환하는 로직 추가
        // Debug.Log("Character investment screen for: " + character.name);
        // UI 전환이나 추가 로직을 작성할 수 있음
        GameManager_.instance.investment.SetCharacter(characterCard.character);

        // 투자창UI 셋팅
        UIManager_.Instance.investmentUI.gameObject.SetActive(true);
        
        // 스텐딩UI 셋팅
        UIManager_.Instance.standingUI.gameObject.SetActive(true);
        UIManager_.Instance.standingUI.SetIllust(characterCard.fullIllust);

        UIManager_.Instance.shopUI.gameObject.SetActive(false);
    }
}
