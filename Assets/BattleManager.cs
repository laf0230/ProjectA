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
    private static BattleManager instance;

    public List<GameObject> skills = new List<GameObject>();

    public static BattleManager Instance
    {
        get
        {
            if (instance == null)
            {
                // GameObject�� ã�Ƽ� �ν��Ͻ��� ����
                GameObject managerObject = GameObject.Find("BattleManager");
                if (managerObject == null)
                {
                    managerObject = new GameObject("BattleManager");
                    instance = managerObject.AddComponent<BattleManager>();
                }
                else
                {
                    instance = managerObject.GetComponent<BattleManager>();
                    if (instance == null)
                    {
                        instance = managerObject.AddComponent<BattleManager>();
                    }
                }
            }
            return instance;
        }
    }

    public GameObject GetAttack(GameObject _prefab)
    {
        if (skills.Contains(_prefab))
        {
            return _prefab;
        }
        else
        {
            var skillInstance = Instantiate(_prefab, transform);
            skills.Add(skillInstance);
            skillInstance.gameObject.SetActive(false);
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
            skillInstance.gameObject.SetActive(false);
            skills.Add(skillInstance.gameObject);
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
