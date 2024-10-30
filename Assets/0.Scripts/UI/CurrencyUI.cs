
using TMPro;
using UnityEngine;

[System.Serializable]
public class CurrencyUI : MonoBehaviour
{
    public int value;
    public TextMeshProUGUI textMeshPro;
    public CurrencyType type;
    
    public void DisplayCurrency(int value)
    {
        if (!gameObject.activeSelf)
            return;
        textMeshPro.text = value.ToString();
    }
}