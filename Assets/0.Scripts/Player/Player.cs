using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Currency gold;
    public Currency chip;
    public Profile profile;
    // 보유한 아이템 목록
    // 투자한 캐릭터 목록과 재화량
}

public enum CurrencyType
{
    Gold,
    Chip
}

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
        amount+= value;
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