using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillCheck : MonoBehaviour
{
    /// <summary>
    ///  ����, ��ų, �ñر⸦ ����� ������ ���Ϸ��� ��� ������ ��� ����� ����ϴ��� ���ؾ��Ѵ�.
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
