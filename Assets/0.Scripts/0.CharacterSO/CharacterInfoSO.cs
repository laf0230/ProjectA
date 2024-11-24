using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Profile
{
    public Sprite ProfileImg;
    public string Name;
    public string Description;
}

[System.Serializable]
public class CharacterStatus
{
    public float MaxHealth;
    public float ChaseSpeed = 1.75f;
    public float ArmorValue;
    public int ThreatLevel;
}

[CreateAssetMenu(fileName = "Test Stat of Character", menuName = "Test SO")]
public class CharacterInfoSO : ScriptableObject
{
    public Profile Profile;
    public CharacterStatus Status;
    public List<SkillSO> Skills;
    public InvestData investData = new InvestData();
}

public class InvestData
{
    public Currency investedChip { get; private set; } = new Currency(CurrencyType.Chip);
    public List<ItemSO> investedItems = new List<ItemSO>();
    public bool isInvested { get; set; } = false;

    public void AddInvestItem(ItemSO item) { investedItems.Add(item); }
    public void RemoveInvestItem(ItemSO item) { investedItems.Remove(item); }

    public void InvestChip(int investedChip)
    {
            this.investedChip.AddCurrency(investedChip);
    }
}
