using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    // ��ų ����
    // ��ų ��Ÿ�� ����
    // ��ų ��� ����
    // ��ų Ǯ ����

    /*
     ��ų���̽��� ������ ������
    ������ �迭�� ������
     */
    public static BattleManager instance;

    public List<GameObject> skills = new List<GameObject>();

    public GameObject GetAttack(GameObject _prefab)
    {
        if (skills.Contains(_prefab))
        {
            return skills.Find(gameObject => gameObject == _prefab.gameObject);
        }
        else
        {
            var skillInstance = Instantiate(_prefab, transform);
            skills.Add(_prefab.gameObject);
            return skillInstance;
        }
    }
    public SkillBase GetAttack(SkillBase _prefab)
    {
        if (skills.Contains(_prefab.gameObject))
        {
            return skills.Find(gameObject => gameObject == _prefab.gameObject).GetComponent<SkillBase>();
        }
        else
        {
            var skillInstance = Instantiate(_prefab, transform);
            skills.Add(_prefab.gameObject);
            return skillInstance;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        
    }
}
