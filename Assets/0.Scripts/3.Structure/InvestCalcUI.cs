using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvestCalcUI : MonoBehaviour // ���ڱ� ��� UI
{
    [SerializeField] private TextMeshProUGUI totalAmountText;
    [SerializeField] private Button confilmButton;

    private Investment_ investment;
    private InvestmentUI_ investUI;
    // ĳ���Ϳ��� ������ �� �� ĳ������ investidata�� ���
    // ȸ���� �� player�� chip�� ���

    public void Start()
    {
        confilmButton.onClick.AddListener(OnConfilmButtonClick);
        investment = GameManager_.instance.investment;
        investUI = UIManager_.Instance.investmentUI;
    }

    public void OnEnable()
    {
        totalAmountText.text = GameManager_.instance.investment.selectedCharacter.investData.investedChip.amount.ToString();
    }

    // ���� �ݾ��� �����ϰ� ���ϴ� �Լ�
    // ���� �ݾ��� �����ϰ� ���ϴ� �Լ�
    public void ModifyInvestmentCost(int amount)
    {
        Player_ playerData = GameManager_.instance.player;
        Currency investedChip = investment.selectedCharacter.investData.investedChip;

        // ���ο� ���� �ݾ��� ����ϰ�, ���� Ĩ���� ���� �������� �ʵ��� ����
        int newInvestedAmount = Mathf.Clamp(investedChip.amount + amount, 0, playerData.chip.amount + investedChip.amount);

        if (amount < 0)
        {
            // ������ �� �ִ� �ݾ��� ����ϰ� �÷��̾� Ĩ�� �߰�
            int decreaseAmount = Mathf.Min(investedChip.amount, Mathf.Abs(amount));
            RemoveInvest(playerData, decreaseAmount);
            investedChip.SpendCurrency(decreaseAmount);
        }
        else
        {
            // ������ �� �ִ� �ݾ��� ����ϰ� �÷��̾� Ĩ���� ����
            int increaseAmount = newInvestedAmount - investedChip.amount;
            AddInvest(playerData, increaseAmount);
            investedChip.AddCurrency(increaseAmount);
        }

        // UI �ؽ�Ʈ ����
        totalAmountText.text = investedChip.amount.ToString();

        // �÷��̾��� Chip UI ����
        UIManager_.Instance.UpdateCurrencyUI();
        investUI.SetInvestBtnCurrencyAmount(investedChip.amount);
    }


    private void AddInvest(Player_ player, int amount)
    {
        player.chip.SpendCurrency(amount);
    }

    private void RemoveInvest(Player_ player, int amount)
    {
        player.chip.AddCurrency(amount);
    }

    // Ȯ�� ��ư
    public void OnConfilmButtonClick()
    {
        // ���� Ȯ�� �� �÷��̾��� Chip���� �ݾ� ����
        // GameManager_.instance.player.InvestCharacter(); // �߰� ���� �ʿ� ��

        if(investment.selectedCharacter.investData.investedChip.amount > 0)
        {
            // ���ڸ� �� ��� ������ ���ڰ� ����
            // ���� Ƚ�� ���� 1ȸ
            GameManager_.instance.investment.SetCharacterInvested(true);
        } else
        {
            // �������� �ʾ��� ��� ���
            GameManager_.instance.investment.SetCharacterInvested(false);
        }

        // UI ������Ʈ
        UIManager_.Instance.investmentUI.UIUpdate();
        UIManager_.Instance.investCalcUI.gameObject.SetActive(false);
    }

    public void OnAllinButtonClick()
    {
        ModifyInvestmentCost(GameManager_.instance.player.chip.amount);
    }

    public void OnInitButtonClick()
    {
        ModifyInvestmentCost(-investment.selectedCharacter.investData.investedChip.amount);
    }
}

