using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FieldEffect : MonoBehaviour
{
    // 각각의 캐릭터가 들어온 순간을 기준으로 데미지를 일정 시간동안 지속적으로 주는 코드
    public float effectDuratioon; // 효과 유지시간
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
            Debug.Log("1차 조건문 통과");
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
            Debug.Log(target.name + " 오브젝트에서 Character를 찾을 수 없습니다.");

        target_.Damage(damage);
    }
}
