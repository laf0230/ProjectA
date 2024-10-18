using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    [System.Serializable]
    public class CharactersSetting
    {
        private List<GameObject> characterPrefabs = new List<GameObject>(); // 캐릭터 목록
        public List<GameObject> selectedCharacters = new List<GameObject>(); // Selected Characters
        public List<GameObject> characterCardList = new List<GameObject>(); // Character Card List

        private GameObject PickRandomCharacter()
        {
            GameObject character = characterPrefabs[Random.Range(0, characterPrefabs.Count)];

            return character;
        }

        public void StartGame(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject character = PickRandomCharacter();

                selectedCharacters.Add(character);
            }
        }
    }

    [System.Serializable]
    public class GameWorldSetting
    {
        private List<GameFieldSO> fieldList = new List<GameFieldSO>();
        public GameFieldSO currentField;

        private GameFieldSO PickRandomField()
        {
            GameFieldSO character = fieldList[Random.Range(0, fieldList.Count)];

            return character;
        }

        public void SetRandomField()
        {
                GameFieldSO randomField = PickRandomField();
                this.currentField = randomField;
        }

    }

    public CharactersSetting characterSetting;
    public GameWorldSetting worldSetting;
}


public class GameFieldSO : ScriptableObject
{
    public Sprite fieldImage;
    public string fieldDescription;
}
