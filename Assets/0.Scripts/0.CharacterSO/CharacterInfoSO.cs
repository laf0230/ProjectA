using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[System.Serializable]
public class Profile
{
    public Sprite ProfileImg;
    public string Name;
    public string Description;
}

[System.Serializable]
public class CharacterStatus
{
    public float MaxHealth;
    public float ChaseSpeed = 1.75f;
    public float ArmorValue;
    public int ThreatLevel;
}

[CreateAssetMenu(fileName = "Test Stat of Character", menuName = "Test SO")]
public class CharacterInfoSO : ScriptableObject
{
    public Profile Profile;
    public CharacterStatus Status;
    public List<SkillSO> Skills;

    public List<ItemSO> investedItems { get; set; }

    public void fd()
    {
        
    }
}

/*
[CustomEditor(typeof(CharacterInfoSO))]
public class CharacterInfoSOCustomEditor : Editor
{
    private bool showProfileFoldout = true; // 프로필 섹션의 Foldout 상태
    private bool showStatusFoldout = true;  // 상태 섹션의 Foldout 상태
    private ReorderableList skillList;        // 스킬 데이터의 ReorderableList

    private void OnEnable()
    {
        // ReorderableList 초기화
        skillList = new ReorderableList(serializedObject, serializedObject.FindProperty("Skills"), true, true, true, true);

        skillList.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Skills");
        };

        skillList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = skillList.serializedProperty.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(rect, element, new GUIContent($"{element.objectReferenceValue}"), true);
        };

        skillList.onAddCallback = (ReorderableList list) =>
        {
            // 새로운 스킬을 추가할 때
            list.serializedProperty.arraySize++;
            var newElement = list.serializedProperty.GetArrayElementAtIndex(list.serializedProperty.arraySize - 1);
            newElement.objectReferenceValue = null;
        };

        skillList.onRemoveCallback = (ReorderableList list) =>
        {
            // 스킬을 제거할 때
            if (list.index >= 0)
            {
                list.serializedProperty.DeleteArrayElementAtIndex(list.index);
            }
        };
    }

    public override void OnInspectorGUI()
    {
        // CharacterInfoSO 객체를 가져옴
        CharacterInfoSO characterInfo = (CharacterInfoSO)target;

        // 프로필 섹션의 Foldout
        showProfileFoldout = EditorGUILayout.Foldout(showProfileFoldout, "Profile");

        if (showProfileFoldout)
        {
            // 프로필 이미지 필드
            characterInfo.Profile.ProfileImg = EditorGUILayout.ObjectField("Profile Image", characterInfo.Profile.ProfileImg, typeof(Sprite), false) as Sprite;

            // 프로필 이름 입력 필드
            characterInfo.Profile.Name = EditorGUILayout.TextField("Name", characterInfo.Profile.Name);

            // 프로필 설명 입력 필드
            characterInfo.Profile.Description = EditorGUILayout.TextArea(characterInfo.Profile.Description, GUILayout.Height(60));
        }

        EditorGUILayout.Space(10f);

        // 상태 섹션의 Foldout
        showStatusFoldout = EditorGUILayout.Foldout(showStatusFoldout, "Character Information");
        if (showStatusFoldout)
        {
            characterInfo.Status.MaxHealth = EditorGUILayout.FloatField("Max Health", characterInfo.Status.MaxHealth);
            characterInfo.Status.ChaseSpeed = EditorGUILayout.FloatField("Chase Speed", characterInfo.Status.ChaseSpeed);
            characterInfo.Status.ArmorValue = EditorGUILayout.FloatField("Armor Value", characterInfo.Status.ArmorValue);
            characterInfo.Status.ThreatLevel = EditorGUILayout.IntField("Threat Level", characterInfo.Status.ThreatLevel);
        }

        EditorGUILayout.Space(10f);

        // 스킬 리스트 그리기
        skillList.DoLayoutList();

        // 변경 사항 저장
        if (GUI.changed)
        {
            EditorUtility.SetDirty(characterInfo);
        }
    }
}
*/
