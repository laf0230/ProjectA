using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="", menuName = "New Card")]
public class CardSO : ScriptableObject
{
    public CharacterInfoSO character;
    public Sprite fullIllust;
    public Sprite StandIllust;
    public GameObject Card;
    public GameObject SubProfile;
}
