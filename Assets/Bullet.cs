using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 100f;
    public Vector3 direction;
    public float damage;
    public Character targetCharacter;
    public GameObject target;
    Rigidbody rigid;
    public float currentDuaration;
    public float maxDuration;
    public bool isShoot;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        currentDuaration = maxDuration;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            collision.gameObject.GetComponent<Character>().Damage(damage);
            Disable(true);
        }
    }

    public void Disable(bool isDisable)
    {
        gameObject.SetActive(!isDisable);
    }
}
