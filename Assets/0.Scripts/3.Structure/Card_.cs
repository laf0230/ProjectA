using UnityEngine;
using UnityEngine.UI;

public class Card_ : MonoBehaviour
{
    [SerializeField] private Button button;
    public CardSO character; // ĳ���� ����

    private void Start()
    {
        button.onClick.AddListener(OnClickCard);
    }

    // ī�� Ŭ�� �� ����Ǵ� �޼���
    public void OnClickCard()
    {
        // ĳ���� ���� â���� ��ȯ�ϴ� ���� �߰�
        // Debug.Log("Character investment screen for: " + character.name);
        // UI ��ȯ�̳� �߰� ������ �ۼ��� �� ����
    }
}
