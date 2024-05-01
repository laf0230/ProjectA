using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    // 스킬 관리
    // 스킬 쿨타임 관리
    // 스킬 사용 관리
    // 스킬 풀 관리

    /*
     스킬베이스의 변수를 가져옴
    변수를 배열로 정리함
     */
    private static BattleManager instance;

    public List<GameObject> skills = new List<GameObject>();

    public static BattleManager Instance
    {
        get
        {
            if (instance == null)
            {
                // GameObject를 찾아서 인스턴스를 설정
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
