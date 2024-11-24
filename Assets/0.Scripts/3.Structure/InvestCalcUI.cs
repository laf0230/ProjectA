using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvestCalcUI : MonoBehaviour // 투자금 계산 UI
{
    [SerializeField] private TextMeshProUGUI totalAmountText;
    [SerializeField] private Button confilmButton;

    private Investment_ investment;
    private InvestmentUI_ investUI;
    // 캐릭터에게 투자할 땐 각 캐릭터의 investidata를 사용
    // 회수할 땐 player의 chip을 사용

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

    // 투자 금액을 수정하고 더하는 함수
    // 투자 금액을 수정하고 더하는 함수
    public void ModifyInvestmentCost(int amount)
    {
        Player_ playerData = GameManager_.instance.player;
        Currency investedChip = investment.selectedCharacter.investData.investedChip;

        // 새로운 투자 금액을 계산하고, 보유 칩보다 많이 투자하지 않도록 조정
        int newInvestedAmount = Mathf.Clamp(investedChip.amount + amount, 0, playerData.chip.amount + investedChip.amount);

        if (amount < 0)
        {
            // 감소할 수 있는 금액을 계산하고 플레이어 칩에 추가
            int decreaseAmount = Mathf.Min(investedChip.amount, Mathf.Abs(amount));
            RemoveInvest(playerData, decreaseAmount);
            investedChip.SpendCurrency(decreaseAmount);
        }
        else
        {
            // 증가할 수 있는 금액을 계산하고 플레이어 칩에서 차감
            int increaseAmount = newInvestedAmount - investedChip.amount;
            AddInvest(playerData, increaseAmount);
            investedChip.AddCurrency(increaseAmount);
        }

        // UI 텍스트 갱신
        totalAmountText.text = investedChip.amount.ToString();

        // 플레이어의 Chip UI 갱신
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

    // 확정 버튼
    public void OnConfilmButtonClick()
    {
        // 투자 확정 후 플레이어의 Chip에서 금액 차감
        // GameManager_.instance.player.InvestCharacter(); // 추가 로직 필요 시

        if(investment.selectedCharacter.investData.investedChip.amount > 0)
        {
            // 투자를 한 경우 아이템 투자가 가능
            // 투자 횟수 제한 1회
            GameManager_.instance.investment.SetCharacterInvested(true);
        } else
        {
            // 투자하지 않았을 경우 취소
            GameManager_.instance.investment.SetCharacterInvested(false);
        }

        // UI 업데이트
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

