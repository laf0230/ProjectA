using System.Collections.Generic;
using UnityEngine;

public enum States
{
    Idle, Attack, Track
}

public class EnemyController : MonoBehaviour
{
    /* Log01 
        State Machine Controller has required
     */

    public float moveSpeed = 5f;
    private List<GameObject> multipleObjects = new List<GameObject>();
    public GameObject singleObject;
    SpriteRenderer spriteRenderer;
    States states = new States();
    public bool isAttacked = false;

    // 스테이터스 변수
    public EnemyStats stats;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        FindObjects();
        stats.weapon = GetComponentInChildren<Weapon>();
    }

    void Update()
    {
        if (states == States.Idle)
            CharacterStateMachine("Idle");

        if (states == States.Attack)
            CharacterStateMachine("Idle");
        CharacterStateMachine("Attack");

        if (states == States.Track)
            CharacterStateMachine("Idle");
        CharacterStateMachine("Track");
    }
    void CharacterStateMachine(string state)
    {
        // Idle -> Track -> Idle -> Attack
        switch (state)
        {
            case "Idle":
                TrackSingleObject(true);
                break;
            case "Attack":
                AttackTarget();
                break;
            case "Track":
                TrackSingleObject();
                MoveTowardsClosest();
                break;
        }
    }

    void FindObjects()
    {
        FindObjectsWithTag("Player", multipleObjects);
        FindObjectWithTag("Player", out singleObject);
    }

    void FindObjectsWithTag(string tag, List<GameObject> objectsList)
    {
        objectsList.Clear();

        GameObject[] allObjects = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject obj in allObjects)
        {
            if (obj != gameObject)
            {
                objectsList.Add(obj);
            }
        }
    }
    #region Track
    void FindObjectWithTag(string tag, out GameObject foundObject)
    {
        foundObject = null;

        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);

        if (objects.Length > 0)
        {
            foundObject = objects[0];
        }
    }

    void MoveTowardsClosest()
    {
        GameObject closestObject = FindClosestObject(multipleObjects);
        Vector3 dirction = new Vector3();

        if (closestObject != null)
        {
            Vector3 direction = (closestObject.transform.position - transform.position).normalized;
            transform.Translate(direction * stats.moveSpeed * Time.deltaTime);
            dirction = transform.position - closestObject.transform.position;
            if (dirction.x > 0)
            {
                flip(false);
            }
            else
            {
                flip(true);
            }
        }
    }
    void flip(bool flipX)
    {

        if (flipX == true)
        {
            spriteRenderer.flipX = true;
            stats.weapon.transform.rotation = Quaternion.Euler(0, 180, 0);
            stats.weapon.transform.localPosition = new Vector3(0.5f, stats.weapon.transform.localPosition.y, stats.weapon.transform.localPosition.z);
        }
        else
        {
            spriteRenderer.flipX = false;
            stats.weapon.transform.rotation = Quaternion.identity;
            stats.weapon.transform.localPosition = new Vector3(-0.5f, stats.weapon.transform.localPosition.y, stats.weapon.transform.localPosition.z);
        }
    }

    GameObject FindClosestObject(List<GameObject> objects)
    {
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject obj in objects)
        {
            float distance = Vector3.Distance(currentPosition, obj.transform.position);
            if (distance < closestDistance)
            {
                closestObject = obj;
                closestDistance = distance;
            }
        }

        return closestObject;
    }

void TrackSingleObject()
    {
        if (singleObject != null)
        {
            Vector3 direction = (singleObject.transform.position - transform.position).normalized;
            transform.Translate(direction * stats.moveSpeed * Time.deltaTime);
        }
    }

    void TrackSingleObject(bool stopTrack)
    {
        if (singleObject != null)
        {
            Vector3 direction = Vector3.zero; 
            transform.Translate(direction * stats.moveSpeed * Time.deltaTime);
        }
    }
    #endregion

    void OnCollisionEnter(Collision collision)
    {
        // When Character Take Damage
        if (collision.gameObject.CompareTag("Weapon") && collision.collider != stats.weapon.GetComponent<Collider>())
        {
            // Assuming you want to apply knockback to the object with the "Weapon" tag, excluding self
            // Calculate the direction from the enemy to the player
            Vector3 knockbackDirection = (collision.transform.position - transform.position).normalized;

            // Apply impulse force to the object's Rigidbody
            Rigidbody objectRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (objectRigidbody != null)
            {
                objectRigidbody.AddForce(knockbackDirection * stats.weapon.weaponStats.knockbackForce, ForceMode.Impulse);
            }
        }
    }
    #region Attack
    GameObject SetAttackTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, stats.weapon.weaponStats.attackRange);

        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Player"))
            {
                return collider.gameObject;
            }
        }

        return null;
    } 

    public void TakeDamage(float damage)
    {
        // using in Weapon class
        if (stats.health > 0)
        {
            stats.health -= damage;
            // Handle other logic when taking damage
        }
    }

    void AttackTarget()
    {
        GameObject target = SetAttackTarget();

        if (target != null && stats.weapon != null)
        {
            stats.weapon.gameObject.SetActive(true);
            stats.weapon.SetTarget(target);

        }
    }
    #endregion
}

[System.Serializable]
public class EnemyStats
{
    public float moveSpeed = 5f;
    public float health = 100;
    public Weapon weapon;
    // 추가적인 스테이터스 변수들을 필요에 따라 추가할 수 있습니다.
}
