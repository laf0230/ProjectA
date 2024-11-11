using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FieldEffect : MonoBehaviour
{
    // ������ ĳ���Ͱ� ���� ������ �������� �������� ���� �ð����� ���������� �ִ� �ڵ�
    public float effectDuratioon; // ȿ�� �����ð�
    public int intervalSec;
    public Dictionary<GameObject, Coroutine> characters = new Dictionary<GameObject, Coroutine>();
    private BulletProperties properties;

    public void Initilize(BulletProperties bulletProperties)
    {
        properties = bulletProperties;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character") && properties.User != other.transform)
        {
            Debug.Log("1�� ���ǹ� ���");
            if (!characters.ContainsKey(other.gameObject))
            {
                Coroutine iEpoision = StartCoroutine(OnImpact(other.gameObject));
                characters.Add(other.gameObject, iEpoision);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            characters.Remove(other.gameObject);
        }
    }


    private IEnumerator OnImpact(GameObject character)
    {
        float totalTime = 0f;

        while (totalTime < effectDuratioon)
        {
            ApplyDamage(character, totalTime);

            yield return new WaitForSeconds(intervalSec);

            totalTime += intervalSec;
        }

        characters.Remove(character);
    }

    private void ApplyDamage(GameObject target, float damage)
    {
        var target_ = target.GetComponent<Character>();

        if (target_ == null)
            Debug.Log(target.name + " ������Ʈ���� Character�� ã�� �� �����ϴ�.");

        target_.Damage(damage);
    }
}
