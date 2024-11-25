using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� �ִ� ĳ���� ��Ͽ��� Ŭ���ϴ� ������
public class ProfileUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image image;
    [SerializeField] private Image loockedImage;
    public CardSO characterCard { get; set; } // ĳ���� ����

    public void SetCardCharacter(CardSO character)
    {
        characterCard = character;
        image.sprite = character.profile;
        button.onClick.AddListener(OnClickButton);
    }

    public void SetLookState(bool isLook)
    {
        loockedImage.gameObject.SetActive(isLook);
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
        UIManager_.Instance.investmentUI.UIUpdate();
        // �� ��ųâ
        UIManager_.Instance.skillStatusUI.gameObject.SetActive(false);
        
        // ���ٵ�UI ����
        UIManager_.Instance.standingUI.gameObject.SetActive(true);
        UIManager_.Instance.standingUI.SetIllust(characterCard.fullIllust);

        UIManager_.Instance.shopUI.gameObject.SetActive(false);

        // ������ ����â ����
        UIManager_.Instance.itemInfoUI.gameObject.SetActive(false);

        // ������ ����
        UIManager_.Instance.profileContainer.SetProfileSelected(characterCard);
    }
}
