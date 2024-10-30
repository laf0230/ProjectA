using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public List<GameObject> SpawnPoints;
    public GameObject Field;

    public void SpawnCharacters(List<CardSO> characters)
    {
        // 캐릭터를 담을 임시 리스트 (랜덤으로 제거하면서 사용할 리스트)
        List<GameObject> newCharacterList = new List<GameObject>();

        foreach (var item in characters)
        {
            newCharacterList.Add(item.CharacterPrefab);
        }
        List<GameObject> availableCharacters = new List<GameObject>(newCharacterList);

        GameObject spawnedCharacter = null;

        foreach (GameObject spawnPoint in SpawnPoints)
        {
            if (availableCharacters.Count == 0)
            {
                // 더 이상 스폰할 캐릭터가 없으면 중단
                break;
            }

            // 랜덤 캐릭터 선택
            GameObject character = availableCharacters[Random.Range(0, availableCharacters.Count)];

            // 이전에 스폰된 캐릭터와 다른 캐릭터일 경우에만 스폰
            if (character != spawnedCharacter)
            {
                Instantiate(character, spawnPoint.transform.position, Quaternion.identity);
                spawnedCharacter = character;

                // 스폰된 캐릭터를 리스트에서 제거하여 다시 선택되지 않게 함
                availableCharacters.Remove(character);
            }
        }
    }

    public void SetField(Sprite field)
    {
        Field.GetComponent<SpriteRenderer>().sprite = field;
    }
}
