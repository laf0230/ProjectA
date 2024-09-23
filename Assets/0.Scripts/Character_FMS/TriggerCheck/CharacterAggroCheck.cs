using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAggroCheck : MonoBehaviour
{
    public List<GameObject> Targets { get; set; }
    private Character _character;

    private void Awake()
    {
        _character = GetComponentInParent<Character>();
        Targets = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Targets.Contains(other.gameObject) && 
            other.gameObject != transform.parent &&
            other.enabled &&
            other.gameObject.CompareTag("Character"))
        {
            Targets.Add(other.gameObject);
            _character.SetAggrostatus(true);
            _character.Targets = Targets;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Targets.Contains(other.gameObject))
        {
            Targets.Remove(other.gameObject);
            _character.SetAggrostatus(false);
        }
    }
    
    public GameObject GetTarget()
    {
        return GetTargets()[0];
    }

    public List<GameObject> GetTargets()
    {
        // �߰����� Ÿ�� ���� �ڵ� //
        List<GameObject> target = null;
        foreach(GameObject _target in Targets)
        {
            var enemy = _target.GetComponent<Character>();
            if (_character.ThreatLevel >= enemy.ThreatLevel)
            {
                // �������� �� ���� + ���� ���� ĳ����
                target.Add(_target);
            } else if(_character.ThreatLevel <  enemy.ThreatLevel)
            {
                target.Add(_target);
            }
            else
            {
                Debug.Log("Target is null on AggroCheck");
                break;
            }
        }
        return target;
    }
}
