using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestManager : MonoBehaviour
{
    // ������: �÷��̾�
    // �ں�: ���, Ĩ
    // 
    public List<Player> m_Players = new List<Player>();

    public int GetUserCurrency(CurrencyType type)
    {
        if(type == CurrencyType.Chip)
        {
            return GetUserCharacter().chip.amount;
        }
        else if(type == CurrencyType.Gold)
        {
            return GetUserCharacter().gold.amount;
        }
        return -1;
    }

    public Player GetUserCharacter()
    {
        foreach (Player player in m_Players)
        {
            if (player.isUser)
                return player;
        }
        return null;
    }
}

[System.Serializable]
public class Player
{
    public string name;
    public bool isUser = false;
    public Currency gold = new Currency(CurrencyType.Gold);
    public Currency chip = new Currency(CurrencyType.Chip);
    // ������ ������ ���
    // ������ ĳ���� ��ϰ� ��ȭ��
}

public enum CurrencyType
{
    Gold,
    Chip
}

[SerializeField]
public class Currency
{
    public CurrencyType type;
    public int amount;

    public Currency(CurrencyType type)
    {
        this.type = type;
    }

    public void AddCurrency(int value)
    {
        amount += value;
    }

    public void SpendCurrency(int value)
    {
        amount -= value;
    }

    public int GetCurrency()
    {
        return amount;
    }
}

public class PlayerData : ScriptableObject
{
    Profile profile;
    public Currency gold = new Currency(CurrencyType.Gold);
    public Currency chip = new Currency(CurrencyType.Chip);
    public Dictionary<Character, Currency> Investment = new Dictionary<Character, Currency>();
    public List<BaseItem> Items = new List<BaseItem>();
}

public class CharacterInfoUI : UI
{
    public BaseItem item;

}
