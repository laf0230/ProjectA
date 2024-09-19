using UnityEngine;

public class Ability : MonoBehaviour
{
    public string Name {  get; set; }

    public void Use() { }
}

public class Hide : Ability
{
    bool isHide;

}