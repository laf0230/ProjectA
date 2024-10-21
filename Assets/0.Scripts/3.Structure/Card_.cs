using UnityEngine;
using UnityEngine.UI;

public class Card_ : MonoBehaviour
{
    [SerializeField] private Button button;
    public CardSO character; // 캐릭터 정보

    private void Start()
    {
        button.onClick.AddListener(OnClickCard);
    }

    // 카드 클릭 시 실행되는 메서드
    public void OnClickCard()
    {
        // 캐릭터 투자 창으로 전환하는 로직 추가
        // Debug.Log("Character investment screen for: " + character.name);
        // UI 전환이나 추가 로직을 작성할 수 있음
    }
}
