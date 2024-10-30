using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public List<GameObject> SpawnPoints;
    public GameObject Field;

    public void SpawnCharacters(List<CardSO> characters)
    {
        // ĳ���͸� ���� �ӽ� ����Ʈ (�������� �����ϸ鼭 ����� ����Ʈ)
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
                // �� �̻� ������ ĳ���Ͱ� ������ �ߴ�
                break;
            }

            // ���� ĳ���� ����
            GameObject character = availableCharacters[Random.Range(0, availableCharacters.Count)];

            // ������ ������ ĳ���Ϳ� �ٸ� ĳ������ ��쿡�� ����
            if (character != spawnedCharacter)
            {
                Instantiate(character, spawnPoint.transform.position, Quaternion.identity);
                spawnedCharacter = character;

                // ������ ĳ���͸� ����Ʈ���� �����Ͽ� �ٽ� ���õ��� �ʰ� ��
                availableCharacters.Remove(character);
            }
        }
    }

    public void SetField(Sprite field)
    {
        Field.GetComponent<SpriteRenderer>().sprite = field;
    }
}
