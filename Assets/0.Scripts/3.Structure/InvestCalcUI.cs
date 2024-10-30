using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvestCalcUI : MonoBehaviour // 투자금 계산 UI
{
    [SerializeField] private TextMeshProUGUI totalAmountText;
    [SerializeField] private Button confilmButton;
    private int cost;  // 현재 투자 금액

    public void Start()
    {
        confilmButton.onClick.AddListener(OnConfilmButtonClick);
    }

    // 투자 금액을 수정하고 더하는 함수
    public void ModifyInvestmentCost(int amount)
    {
        Player_ playerData = GameManager_.instance.player;

        // 음수 값을 방지
        if (cost < 0)
        {
            cost = 0;
        }

        // 현재 투자 금액에 amount만큼 더함
        int newCost = cost + amount;

        // 보유한 칩보다 많이 투자할 수 없도록 조정
        if (newCost > playerData.chip.amount + cost)
        {
            newCost = playerData.chip.amount + cost;
        }

        // 투자 금액이 감소할 경우
        if (amount < 0)
        {
            // cost가 줄어들 수 있는 최소한의 한도는 0
            int decreaseAmount = Mathf.Min(cost, Mathf.Abs(amount));  // 감소할 수 있는 양은 현재 cost까지만

            // 줄어든 금액만큼 Chip에 돌려줌
            playerData.chip.AddCurrency(decreaseAmount);
            cost -= decreaseAmount;
        }
        else
        {
            // 투자 금액이 증가하면 그만큼 Chip에서 차감
            int deduction = newCost - cost;
            playerData.chip.SpendCurrency(deduction);
            cost = newCost;
        }

        // UI 텍스트 갱신
        totalAmountText.text = cost.ToString();

        // 플레이어의 Chip 갱신
        UIManager_.Instance.UpdateCurrencyUI();
    }

    // 확정 버튼
    public void OnConfilmButtonClick()
    {
        // 투자 확정 후 플레이어의 Chip에서 금액 차감
        // GameManager_.instance.player.InvestCharacter(); // 추가 로직 필요 시

        if(cost > 0)
        {
            // 투자를 한 경우 아이템 투자가 가능
            GameManager_.instance.investment.SetCharacterInvested(true);
        }

        // UI 업데이트
        UIManager_.Instance.investmentUI.UIUpdate();
        UIManager_.Instance.investCalcUI.gameObject.SetActive(false);
    }
}

