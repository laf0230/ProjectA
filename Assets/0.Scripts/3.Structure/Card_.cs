using UnityEngine;
using UnityEngine.UI;

public class Card_ : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image cardImage;
    private CardSO characterCard; // 캐릭터 정보

    public void SetCardCharacter(CardSO character)
    {
        this.characterCard = character;
        cardImage.sprite = character.fullIllust;
        button.onClick.AddListener(OnClickCard);
        Debug.Log("카드에 클릭 기능이 할당되었습니다.");
    }

    // 카드 클릭 시 실행되는 메서드
    public void OnClickCard()
    {
        Debug.Log("카드 클릭!");
        // 캐릭터 투자 창으로 전환하는 로직 추가
        // Debug.Log("Character investment screen for: " + character.name);
        // UI 전환이나 추가 로직을 작성할 수 있음

        // 캐릭터 셋팅
        GameManager_.instance.investment.SetCharacter(characterCard.character);

        // 메니징 창 활성화
        UIManager_.Instance.GetUIGroup(UIType_.ManagementUI).Open();

        // 프로필 셋팅
        UIManager_.Instance.profileContainer.SetProfiles(UIManager_.Instance.cardContainer.cards);

        // 카드 셋팅
        UIManager_.Instance.cardContainer.gameObject.SetActive(false);

        // 투자창 셋팅
        UIManager_.Instance.investmentUI.gameObject.SetActive(true);
        
        // 스텐딩
        UIManager_.Instance.standingUI.gameObject.SetActive(true);
        UIManager_.Instance.standingUI.SetIllust(characterCard.fullIllust);
    }
}
