using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item")]
public class ItemSO : ScriptableObject
{
    public int BuyPrise;
    public int SellPrise;

    public string Name;
    public Sprite sprite;
    public string description;
    public List<AbilityInfo> Ability = new List<AbilityInfo>();
}
