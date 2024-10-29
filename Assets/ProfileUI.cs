using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� �ִ� ĳ���� ��Ͽ��� Ŭ���ϴ� ������
public class ProfileUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image image;
    private CardSO characterCard; // ĳ���� ����

    public void SetCardCharacter(CardSO character)
    {
        characterCard = character;
        image.sprite = character.profile;
        button.onClick.AddListener(OnClickButton);
    }

    // ī�� Ŭ�� �� ����Ǵ� �޼���
    public void OnClickButton()
    {
        // ĳ���� ���� â���� ��ȯ�ϴ� ���� �߰�
        // Debug.Log("Character investment screen for: " + character.name);
        // UI ��ȯ�̳� �߰� ������ �ۼ��� �� ����
        GameManager_.instance.investment.SetCharacter(characterCard.character);

        // ����âUI ����
        UIManager_.Instance.investmentUI.gameObject.SetActive(true);
        
        // ���ٵ�UI ����
        UIManager_.Instance.standingUI.gameObject.SetActive(true);
        UIManager_.Instance.standingUI.SetIllust(characterCard.fullIllust);

        UIManager_.Instance.shopUI.gameObject.SetActive(false);
    }
}
