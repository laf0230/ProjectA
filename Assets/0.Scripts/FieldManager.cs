using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public List<GameObject> SpawnPoints;
    public GameObject Field;
    public List<GameObject> onFieldCharacters;
    public List<GameObject> fields;

    public void ActiveRandomField()
    {
        var length = Random.Range(0, fields.Count - 1);

        fields[length].SetActive(true);
    }

    public void SpawnCharacters(List<CardSO> characters)
    {
        // 캐릭터 프리팹을 담을 임시 리스트 (캐릭터 데이터로부터 생성)
        List<GameObject> newCharacterList = new List<GameObject>();

        // 캐릭터 데이터를 기반으로 캐릭터 프리팹 리스트 생성
        foreach (var item in characters)
        {
            if (item.CharacterPrefab != null)
            {
                newCharacterList.Add(item.CharacterPrefab);
            }
        }

        // 사용 가능한 캐릭터 리스트 복사본
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
                onFieldCharacters.Add(Instantiate(character, spawnPoint.transform.position, Quaternion.identity));
                spawnedCharacter = character;
                spawnedCharacter.SetActive(false);

                // 스폰된 캐릭터를 리스트에서 제거하여 다시 선택되지 않게 함
                availableCharacters.Remove(character);
                Debug.Log($"{character.name} 캐릭터가 스폰되었습니다");
            }
        }
    }

    public void ActiveCharactersOnField()
    {
        foreach (var character in onFieldCharacters)
        {
            character.SetActive(true);
        }
    }

    public void SetField(Sprite field)
    {
        Field.GetComponent<SpriteRenderer>().sprite = field;
    }

    public Character GetCharacter(CharacterInfoSO status)
    {
        foreach (var item in onFieldCharacters)
        {
            Character character = item.GetComponent<Character>();

            if(character.Info == status)
                return item.GetComponent<Character>();
        }

        return null;
    }
}
