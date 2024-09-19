using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "New Ability")]
public class AbilitySO : ScriptableObject
{
    public int ID;
    public float Value;
    public string Description;
}