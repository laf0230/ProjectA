using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvestCalcUI : MonoBehaviour // ���ڱ� ��� UI
{
    [SerializeField] private TextMeshProUGUI totalAmountText;
    [SerializeField] private Button confilmButton;
    private int cost;  // ���� ���� �ݾ�

    public void Start()
    {
        confilmButton.onClick.AddListener(OnConfilmButtonClick);
    }

    // ���� �ݾ��� �����ϰ� ���ϴ� �Լ�
    public void ModifyInvestmentCost(int amount)
    {
        Player_ playerData = GameManager_.instance.player;

        // ���� ���� ����
        if (cost < 0)
        {
            cost = 0;
        }

        // ���� ���� �ݾ׿� amount��ŭ ����
        int newCost = cost + amount;

        // ������ Ĩ���� ���� ������ �� ������ ����
        if (newCost > playerData.Chip + cost)
        {
            newCost = playerData.Chip + cost;
        }

        // ���� �ݾ��� ������ ���
        if (amount < 0)
        {
            // cost�� �پ�� �� �ִ� �ּ����� �ѵ��� 0
            int decreaseAmount = Mathf.Min(cost, Mathf.Abs(amount));  // ������ �� �ִ� ���� ���� cost������

            // �پ�� �ݾ׸�ŭ Chip�� ������
            playerData.Chip += decreaseAmount;
            cost -= decreaseAmount;
        }
        else
        {
            // ���� �ݾ��� �����ϸ� �׸�ŭ Chip���� ����
            int deduction = newCost - cost;
            playerData.Chip -= deduction;
            cost = newCost;
        }

        // UI �ؽ�Ʈ ����
        totalAmountText.text = cost.ToString();

        // �÷��̾��� Chip ����
        UIManager_.Instance.UpdateCurrencyUI();
    }

    // Ȯ�� ��ư
    public void OnConfilmButtonClick()
    {
        // ���� Ȯ�� �� �÷��̾��� Chip���� �ݾ� ����
        // GameManager_.instance.player.InvestCharacter(); // �߰� ���� �ʿ� ��
    }
}
