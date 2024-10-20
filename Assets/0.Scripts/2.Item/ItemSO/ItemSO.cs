using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item")]
public class ItemSO : ScriptableObject
{
    public int BuyPrise;
    public int SellPrise;

    public Sprite sprite;
    public string Description;
    public List<AbilityInfo> Ability = new List<AbilityInfo>();
    public bool IsSellable;
}
