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
    public static BattleManager instance;

    public List<GameObject> skills = new List<GameObject>();

    public Skill GetAttack(Skill _prefab)
    {
        if (skills.Contains(_prefab.gameObject))
        {
            return skills.Find(gameObject => gameObject == _prefab.gameObject).GetComponent<Skill>();
        }
        else
        {
            var skillInstance = Instantiate(_prefab, transform);
            skills.Add(_prefab.gameObject);
            return skillInstance;
        }
    }

    public Attack GetAttack(Attack _prefab)
    {
        if (skills.Contains(_prefab.gameObject))
        {
            return skills.Find(gameObject => gameObject == _prefab.gameObject).GetComponent<Attack>();
        }
        else
        {
            var skillInstance = Instantiate(_prefab, transform);
            skills.Add(_prefab.gameObject);
            return skillInstance;
        }
    }

    public SpecialSkill GetAttack(SpecialSkill _prefab)
    {
        if (skills.Contains(_prefab.gameObject))
        {
            return skills.Find(gameObject => gameObject == _prefab.gameObject).GetComponent<SpecialSkill>();
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
