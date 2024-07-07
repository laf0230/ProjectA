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
        if (other.gameObject != _character && other.gameObject.CompareTag("Character"))
        {
            if (!other.enabled) return;
            Targets.Add(other.gameObject);
            _character.SetAggrostatus(true);
            _character.SetTarget(GetTarget());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != _character && other.gameObject.CompareTag("Character"))
        {
            Targets.Remove(other.gameObject);
            _character.SetAggrostatus(false);
        }
    }
    
    public GameObject GetTarget()
    {
        // 추가적인 타겟 지정 코드 //
        GameObject target = null;
        foreach(GameObject _target in Targets)
        {
            var enemy = _target.GetComponent<Character>();
            if (_character.ThreatLevel >= enemy.ThreatLevel)
            {
                // 위협도가 더 낮음 + 먼저 들어온 캐릭터
                target = _target;
            } else if(_character.ThreatLevel <  enemy.ThreatLevel)
            {
                target = _target;
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
