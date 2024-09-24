using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillCheck : MonoBehaviour
{
    /// <summary>
    ///  공격, 스킬, 궁극기를 나누어서 범위를 정하려면 어느 범위가 어느 기술을 사용하는지 정해야한다.
    /// </summary>

    public Character Character { get; private set; }

    public enum TriggerType
    {
        Attack,
        Skill,
        Ultimate
    }

    public TriggerType attackType;

    private void Awake()
    {
        Character = GetComponentInParent<Character>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(
            other.CompareTag("Character") &&
            other.gameObject.activeSelf &&
            other.transform != Character.transform &&
            Character.Targets.Contains(Character.gameObject)
            )
        {
            Character.IsWithinstrikingDistance = true;
            Character.Targets.Add(other.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
    }
}
