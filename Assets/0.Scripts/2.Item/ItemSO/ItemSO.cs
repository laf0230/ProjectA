using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    public int BuyPrise { get; set; }
    public int SellPrise {  get; set; }

    public string Name {  get; set; }
    public Sprite sprite {  get; set; }
    public string description {  get; set; }
    public List<AbilityInfo> Ability { get; set; }
}

[CreateAssetMenu(fileName = "New Item", menuName = "New Item")]
public class ItemSO : ScriptableObject
{
    public int BuyPrise;
    public int SellPrise;

    public string Name;
    public Sprite sprite;
    public string description;
    public List<AbilityInfo> Ability = new List<AbilityInfo>();
    public bool isOwned { get; set; } = false;
}

public class Item
{
    public int BuyPrise;
    public int SellPrise;

    public string Name;
    public Sprite sprite;
    public string description;
    public List<AbilityInfo> Ability = new List<AbilityInfo>();
    public bool isOwned { get; set; } = false;
}
