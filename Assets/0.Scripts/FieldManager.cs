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
        // ĳ���� �������� ���� �ӽ� ����Ʈ (ĳ���� �����ͷκ��� ����)
        List<GameObject> newCharacterList = new List<GameObject>();

        // ĳ���� �����͸� ������� ĳ���� ������ ����Ʈ ����
        foreach (var item in characters)
        {
            if (item.CharacterPrefab != null)
            {
                newCharacterList.Add(item.CharacterPrefab);
            }
        }

        // ��� ������ ĳ���� ����Ʈ ���纻
        List<GameObject> availableCharacters = new List<GameObject>(newCharacterList);
        GameObject spawnedCharacter = null;

        foreach (GameObject spawnPoint in SpawnPoints)
        {
            if (availableCharacters.Count == 0)
            {
                // �� �̻� ������ ĳ���Ͱ� ������ �ߴ�
                break;
            }

            // ���� ĳ���� ����
            GameObject character = availableCharacters[Random.Range(0, availableCharacters.Count)];

            // ������ ������ ĳ���Ϳ� �ٸ� ĳ������ ��쿡�� ����
            if (character != spawnedCharacter)
            {
                onFieldCharacters.Add(Instantiate(character, spawnPoint.transform.position, Quaternion.identity));
                spawnedCharacter = character;
                spawnedCharacter.SetActive(false);

                // ������ ĳ���͸� ����Ʈ���� �����Ͽ� �ٽ� ���õ��� �ʰ� ��
                availableCharacters.Remove(character);
                Debug.Log($"{character.name} ĳ���Ͱ� �����Ǿ����ϴ�");
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
