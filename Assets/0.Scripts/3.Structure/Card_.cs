using UnityEngine;
using UnityEngine.UI;

public class Card_ : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image cardImage;
    private CardSO characterCard; // ĳ���� ����

    public void SetCardCharacter(CardSO character)
    {
        this.characterCard = character;
        cardImage.sprite = character.fullIllust;
        button.onClick.AddListener(OnClickCard);
        Debug.Log("ī�忡 Ŭ�� ����� �Ҵ�Ǿ����ϴ�.");
    }

    // ī�� Ŭ�� �� ����Ǵ� �޼���
    public void OnClickCard()
    {
        Debug.Log("ī�� Ŭ��!");
        // ĳ���� ���� â���� ��ȯ�ϴ� ���� �߰�
        // Debug.Log("Character investment screen for: " + character.name);
        // UI ��ȯ�̳� �߰� ������ �ۼ��� �� ����

        // ĳ���� ����
        GameManager_.instance.investment.SetCharacter(characterCard.character);

        // �޴�¡ â Ȱ��ȭ
        UIManager_.Instance.GetUIGroup(UIType_.ManagementUI).Open();

        // ������ ����
        UIManager_.Instance.profileContainer.SetProfiles(UIManager_.Instance.cardContainer.cards);

        // ī�� ����
        UIManager_.Instance.cardContainer.gameObject.SetActive(false);

        // ����â ����
        UIManager_.Instance.investmentUI.gameObject.SetActive(true);
        
        // ���ٵ�
        UIManager_.Instance.standingUI.gameObject.SetActive(true);
        UIManager_.Instance.standingUI.SetIllust(characterCard.fullIllust);
    }
}
