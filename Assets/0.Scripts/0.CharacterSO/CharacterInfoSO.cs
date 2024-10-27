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
    private bool showProfileFoldout = true; // ������ ������ Foldout ����
    private bool showStatusFoldout = true;  // ���� ������ Foldout ����
    private ReorderableList skillList;        // ��ų �������� ReorderableList

    private void OnEnable()
    {
        // ReorderableList �ʱ�ȭ
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
            // ���ο� ��ų�� �߰��� ��
            list.serializedProperty.arraySize++;
            var newElement = list.serializedProperty.GetArrayElementAtIndex(list.serializedProperty.arraySize - 1);
            newElement.objectReferenceValue = null;
        };

        skillList.onRemoveCallback = (ReorderableList list) =>
        {
            // ��ų�� ������ ��
            if (list.index >= 0)
            {
                list.serializedProperty.DeleteArrayElementAtIndex(list.index);
            }
        };
    }

    public override void OnInspectorGUI()
    {
        // CharacterInfoSO ��ü�� ������
        CharacterInfoSO characterInfo = (CharacterInfoSO)target;

        // ������ ������ Foldout
        showProfileFoldout = EditorGUILayout.Foldout(showProfileFoldout, "Profile");

        if (showProfileFoldout)
        {
            // ������ �̹��� �ʵ�
            characterInfo.Profile.ProfileImg = EditorGUILayout.ObjectField("Profile Image", characterInfo.Profile.ProfileImg, typeof(Sprite), false) as Sprite;

            // ������ �̸� �Է� �ʵ�
            characterInfo.Profile.Name = EditorGUILayout.TextField("Name", characterInfo.Profile.Name);

            // ������ ���� �Է� �ʵ�
            characterInfo.Profile.Description = EditorGUILayout.TextArea(characterInfo.Profile.Description, GUILayout.Height(60));
        }

        EditorGUILayout.Space(10f);

        // ���� ������ Foldout
        showStatusFoldout = EditorGUILayout.Foldout(showStatusFoldout, "Character Information");
        if (showStatusFoldout)
        {
            characterInfo.Status.MaxHealth = EditorGUILayout.FloatField("Max Health", characterInfo.Status.MaxHealth);
            characterInfo.Status.ChaseSpeed = EditorGUILayout.FloatField("Chase Speed", characterInfo.Status.ChaseSpeed);
            characterInfo.Status.ArmorValue = EditorGUILayout.FloatField("Armor Value", characterInfo.Status.ArmorValue);
            characterInfo.Status.ThreatLevel = EditorGUILayout.IntField("Threat Level", characterInfo.Status.ThreatLevel);
        }

        EditorGUILayout.Space(10f);

        // ��ų ����Ʈ �׸���
        skillList.DoLayoutList();

        // ���� ���� ����
        if (GUI.changed)
        {
            EditorUtility.SetDirty(characterInfo);
        }
    }
}
*/
