using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
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
    public List<ItemSO> investedItems;
}
