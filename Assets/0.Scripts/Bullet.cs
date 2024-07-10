using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 100f;
    public Vector3 direction;
    public float damage { get; set; }
    public GameObject target;
    Rigidbody rigid;
    public float currentDuaration { get; set; }
    public float maxDuration;
    public bool isShoot;

    private void Start()
    {
        gameObject.SetActive(true);
        rigid = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        currentDuaration = maxDuration;
        if (target == null)
            return;

        direction = target.transform.position - this.transform.position;
        
        Shoot();
    }

    private void Update()
    {
        if (currentDuaration > 0)
        {
            currentDuaration -= Time.deltaTime;
        }
        else if (currentDuaration < 0)
        {
            Disable(true);
        }
    }

    public void Shoot()
    {
        rigid.AddForce(direction * speed, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character") && other.gameObject == target)
        {
            other.gameObject.GetComponent<Character>().Damage(damage);
            Disable(true);
        }
    }

    public void Disable(bool isDisable)
    {
        gameObject.SetActive(!isDisable);
    }

    public void SetTarget(GameObject taregt)
    {
        this.target = taregt;
    }

    public GameObject GetTarget()
    {
        return target;
    }

}
