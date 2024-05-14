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

    private void Awake()
    {
        if (instance != null)
        {
            instance = this;
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

    private void Update()
    {

    }

    #region Buff / Debuff List

    public void Invisible(Character target, float duration)
    {
        
    }

    public void Heal(Character target, float healAmount)
    {
        target.CurrentHealth += healAmount;
    }

    public void EnhacedAttackSpeed(Character target, float amount)
    {

    }

    public void EnhancedMoveSpeed(Character target, float amount)
    {

    }

    public void Stun(Character target, float duaration)
    {

    }

    public void Poison(Character target, float duration, float amount)
    {

    }

    public void Silence(Character target, float duration)
    {
        // ��ų ��� �Ұ�
    }

    #endregion
}
