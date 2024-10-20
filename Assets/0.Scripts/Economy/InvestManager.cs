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
    public Currency gold = new Currency(CurrencyType.Gold, 10);
    public Currency chip = new Currency(CurrencyType.Chip, 10);
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

    public Currency(CurrencyType type, int initialAmount)
    {
        this.type = type;
        this.amount = initialAmount;
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

public class Item
{
    public string name;
    public string description;
    public Currency buyPrise;
    public Currency sellPrise;
    public List<AbilityInfo> abilityInfo = new List<AbilityInfo>();
}

public class PlayerData : ScriptableObject
{
    Profile profile;
    public Currency gold = new Currency(CurrencyType.Gold, 0);
    public Currency chip = new Currency(CurrencyType.Chip, 0);
    public Dictionary<Character, Currency> Investment = new Dictionary<Character, Currency>();
    public List<Item> Items = new List<Item>();
}

public class CharacterInfoUI : UI
{
    public Item item;

}
