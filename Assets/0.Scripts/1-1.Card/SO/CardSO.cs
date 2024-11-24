using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="", menuName = "New Card")]
public class CardSO : ScriptableObject
{
    public CharacterInfoSO character;
    public GameObject CharacterPrefab;
    public Sprite fullIllust;
    public Sprite standIllust;
    public Sprite cardIllust;
    public Sprite profile;
    [TextArea(4, 10)]
    public string reachingWishText;
}
