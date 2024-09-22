using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float totalCoolTime = 10f;
    public float currentCoolTime;
    public float Speed = 5f;
    public bool isUseable = false;
    public bool isCooling = true;

    public GameObject bulletPrefab;
    private BulletInfo combatInfo;
    private SkillInfo skillInfo;
    public string Name;
    public string Damage;
    public string Rank;
    public int RangeType;
    public int TargetType;
    public float CollTime;
    public List<AbilityInfo> Abilities;


    
    private void Update()
    {
        // 쿨타임 감소 코드
        if(currentCoolTime > 0)
        {
            currentCoolTime -= Time.deltaTime;
        } else
        {
            isUseable = true;
            isCooling = false;
        }
    }

    public void Use()
    {
        isCooling = true;
        isUseable = false;
        ResetCoolTime();

        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();

        bullet.SetSpeed();
        bullet.SetDamage(10f);
        bullet.SetCombatInfo(combatInfo);
        bullet.Shoot();

    }

    public void SetCombatInfo(Transform user, float speed, float damage)
    {
        combatInfo = new BulletInfo(damage, speed, user);
    }

    public bool GetIsUseable() { return isUseable; }
    public bool GetIsCooling() { return isCooling; }

    public void ResetCoolTime()
    {
        currentCoolTime = totalCoolTime;
    }
}
