
using TMPro;
using UnityEngine;

[System.Serializable]
public class CurrencyUI : MonoBehaviour
{
    public int value;
    public CurrencyType currencyType;
    private TextMeshProUGUI m_TextMeshPro;

    private void Start()
    {
        m_TextMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public void DisplayCurrency(int value)
    {
        if (!gameObject.activeSelf)
            return;
        m_TextMeshPro.text = value.ToString();
    }
}