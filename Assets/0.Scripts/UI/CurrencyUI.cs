
using TMPro;
using UnityEngine;

[System.Serializable]
public class CurrencyUI : MonoBehaviour
{
    public int value;
    private TextMeshProUGUI m_TextMeshPro;

    public void DisplayCurrency(int value)
    {
        m_TextMeshPro = GetComponent<TextMeshProUGUI>();
        
        if (!gameObject.activeSelf)
            return;
        m_TextMeshPro.text = value.ToString();
    }
}