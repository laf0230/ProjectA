
using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    public int value;
    public CurrencyType currencyType;
    private TextMeshProUGUI m_TextMeshPro;

    private void Start()
    {
        m_TextMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public void Update()
    {
        value = GameManager.Instance.investManager.GetUserCurrency(currencyType);
        DisplayCurrency(value);
    }

    public void DisplayCurrency(int value)
    {
        m_TextMeshPro.text = value.ToString();
    }
}