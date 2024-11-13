using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float totalCoolTime {get;set;} = 10;
    public float currentCoolTime{get;set;}
    public float Speed { get; set; } = 5f;
    public bool isUseable { get; set; } = false;
    public bool isCooling { get; set; } = true;

    public GameObject bulletPrefab{get;set;}
    private BulletProperties combatInfo{get;set;}
    private SkillProperties skillInfo{get;set;}
    public string Name{get;set;}
    public string Damage{get;set;}
    public string Rank{get;set;}
    public int RangeType{get;set;}
    public int TargetType{get;set;}
    public float CollTime{get;set;}
    public List<AbilityInfo> Abilities{get;set;}


    
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

        bullet.Shoot();

    }

    public void SetCombatInfo(Transform user, ProjectileType projectileType, float speed, float damage, float reach, bool isOwnedPlace, int duration)
    {
        combatInfo = new BulletProperties(projectileType, damage, speed, user, reach, isOwnedPlace, duration);
    }

    public bool GetIsUseable() { return isUseable; }
    public bool GetIsCooling() { return isCooling; }

    public void ResetCoolTime()
    {
        currentCoolTime = totalCoolTime;
    }
}
