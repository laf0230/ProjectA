using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomPropertyDrawer(typeof(AbilityInfo))]
public class AbilityInfoListEditor : Editor
{
    private ReorderableList abilityList;

    private void OnEnable()
    {
        // Initialize the ReorderableList
        abilityList = new ReorderableList(serializedObject, serializedObject.FindProperty("Abilities"), true, true, true, true);

        // Set up the header for the list
        abilityList.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "�ɷ� ���");
        };

        // Draw elements in the list
        abilityList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            SerializedProperty element = abilityList.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2; // Slightly offset the Y position for better visual alignment

            // Create a foldout for each ability
            bool foldout = EditorGUI.Foldout(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                element.isExpanded, $"�ɷ� {index + 1}");

            element.isExpanded = foldout;

            if (foldout)
            {
                EditorGUI.indentLevel++; // Increase indent for nested properties

                // Draw properties
                DrawAbilityFields(rect, element);

                EditorGUI.indentLevel--; // Decrease indent after drawing
            }
        };

        // Add new elements to the list
        abilityList.onAddCallback = (ReorderableList l) =>
        {
            l.serializedProperty.arraySize++;
            SerializedProperty newElement = l.serializedProperty.GetArrayElementAtIndex(l.serializedProperty.arraySize - 1);
            InitializeNewAbility(newElement);
        };

        // Remove elements from the list
        abilityList.onRemoveCallback = (ReorderableList l) =>
        {
            l.serializedProperty.DeleteArrayElementAtIndex(l.index);
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Draw the ReorderableList for abilities
        abilityList.DoLayoutList();

        // Mark the object as dirty if changes were made
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawAbilityFields(Rect position, SerializedProperty property)
    {
        // Offset for the properties
        position.y += EditorGUIUtility.singleLineHeight;

        // Draw properties
        EditorGUI.PropertyField(position, property.FindPropertyRelative("Name"), new GUIContent("�ΰ�ȿ�� �̸�"));
        position.y += EditorGUIUtility.singleLineHeight;

        EditorGUI.PropertyField(position, property.FindPropertyRelative("ID"), new GUIContent("�ΰ�ȿ�� ID"));
        position.y += EditorGUIUtility.singleLineHeight;

        EditorGUI.PropertyField(position, property.FindPropertyRelative("IsPercentage"), new GUIContent("�ۼ�Ʈ ����"));
        position.y += EditorGUIUtility.singleLineHeight;

        EditorGUI.PropertyField(position, property.FindPropertyRelative("Value"), new GUIContent("��"));
        position.y += EditorGUIUtility.singleLineHeight;

        EditorGUI.PropertyField(position, property.FindPropertyRelative("Duration"), new GUIContent("���� �ð�"));
        position.y += EditorGUIUtility.singleLineHeight;

        EditorGUI.PropertyField(position, property.FindPropertyRelative("TargetType"), new GUIContent("��ǥ���� ����"));
        position.y += EditorGUIUtility.singleLineHeight;

        EditorGUI.PropertyField(position, property.FindPropertyRelative("shape"), new GUIContent("����"));
        position.y += EditorGUIUtility.singleLineHeight;

        EditorGUI.PropertyField(position, property.FindPropertyRelative("AnimationType"), new GUIContent("�ִϸ��̼� Ʈ����"));
        position.y += EditorGUIUtility.singleLineHeight;

        // Draw movement-related fields
        SerializedProperty hasMovementProp = property.FindPropertyRelative("HasMovement");
        EditorGUI.PropertyField(position, hasMovementProp, new GUIContent("�̵��� ����"));
        position.y += EditorGUIUtility.singleLineHeight;

        if (hasMovementProp.boolValue)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("MovementActionType"), new GUIContent("�̵����� ����"));
        }

        // Draw effect status field
        EditorGUI.PropertyField(position, property.FindPropertyRelative("EffectStatus"), new GUIContent("����"));
    }

    private void InitializeNewAbility(SerializedProperty newElement)
    {
        newElement.FindPropertyRelative("Name").stringValue = "�� �ɷ�"; // Default name
        newElement.FindPropertyRelative("ID").intValue = 0; // Default ID
        newElement.FindPropertyRelative("IsPercentage").boolValue = false; // Default percentage flag
        newElement.FindPropertyRelative("Value").floatValue = 0f; // Default value
        newElement.FindPropertyRelative("Duration").floatValue = 0f; // Default duration
        newElement.FindPropertyRelative("TargetType").enumValueIndex = 0; // Default target type
        newElement.FindPropertyRelative("HasMovement").boolValue = false; // Default movement flag
        newElement.FindPropertyRelative("MovementActionType").enumValueIndex = 0; // Default movement action type
    }
}



[CustomEditor(typeof(SkillSO))]
public class SkillDataSOEditor : Editor
{
    SerializedProperty abilities;
    ReorderableList reorderableList;  // ReorderableList ����
    private bool showProfileFoldout = false; // Foldout ���¸� ������ ����
    private bool[] foldouts;

    // Ability �ν�����
    private void OnEnable()
    {
        abilities = serializedObject.FindProperty("Ability");

        foldouts = new bool[abilities.arraySize];

        reorderableList = new ReorderableList(serializedObject, abilities, true, true, true, true);

        reorderableList.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Abilities");
        };

        reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            SerializedProperty element = abilities.GetArrayElementAtIndex(index);
            rect.y += 2;

            SerializedProperty Name = element.FindPropertyRelative("Name");
            foldouts[index] = EditorGUI.Foldout(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), foldouts[index], Name.stringValue);

            if (foldouts[index])
            {
                EditorGUI.indentLevel++;

                SerializedProperty name = element.FindPropertyRelative("Name");
                SerializedProperty id = element.FindPropertyRelative("ID");
                SerializedProperty isPercentage = element.FindPropertyRelative("IsPercentage");
                SerializedProperty hasMovement = element.FindPropertyRelative("HasMovement");
                SerializedProperty value = element.FindPropertyRelative("Value");
                SerializedProperty duration = element.FindPropertyRelative("Duration");
                SerializedProperty targetType = element.FindPropertyRelative("TargetType");
                SerializedProperty shape = element.FindPropertyRelative("shape");
                SerializedProperty animationType = element.FindPropertyRelative("AnimationType");
                SerializedProperty effectStatus = element.FindPropertyRelative("EffectStatus");
                SerializedProperty movementActionType = element.FindPropertyRelative("MovementActionType");

                EditorGUI.PropertyField(new Rect(rect.x, rect.y + 20, rect.width, EditorGUIUtility.singleLineHeight), name);
                EditorGUI.PropertyField(new Rect(rect.x, rect.y + 40, rect.width, EditorGUIUtility.singleLineHeight), id);
                EditorGUI.PropertyField(new Rect(rect.x, rect.y + 60, rect.width, EditorGUIUtility.singleLineHeight), isPercentage);
                EditorGUI.PropertyField(new Rect(rect.x, rect.y + 80, rect.width, EditorGUIUtility.singleLineHeight), hasMovement);
                EditorGUI.PropertyField(new Rect(rect.x, rect.y + 100, rect.width, EditorGUIUtility.singleLineHeight), value);
                EditorGUI.PropertyField(new Rect(rect.x, rect.y + 120, rect.width, EditorGUIUtility.singleLineHeight), duration);
                EditorGUI.PropertyField(new Rect(rect.x, rect.y + 140, rect.width, EditorGUIUtility.singleLineHeight), targetType);
                EditorGUI.PropertyField(new Rect(rect.x, rect.y + 160, rect.width, EditorGUIUtility.singleLineHeight), shape);
                EditorGUI.PropertyField(new Rect(rect.x, rect.y + 180, rect.width, EditorGUIUtility.singleLineHeight), animationType);

                if (id.intValue == 0)
                {
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y + 200, rect.width, EditorGUIUtility.singleLineHeight), effectStatus);
                }

                if (hasMovement.boolValue)
                {
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y + 220, rect.width, EditorGUIUtility.singleLineHeight), movementActionType);
                }

                EditorGUI.indentLevel--;
            }
        };

        reorderableList.elementHeightCallback = (int index) =>
        {
            // �⺻ ����
            float height = EditorGUIUtility.singleLineHeight + 4;

            // �������� �� �߰� ����
            if (foldouts[index])
            {
                height += 220f; // �� �ʵ���� �����ϴ� ���� �߰�
            }

            return height;
        };

        reorderableList.onAddCallback = (ReorderableList list) =>
        {
            abilities.arraySize++;
            foldouts = new bool[abilities.arraySize];
        };

        reorderableList.onRemoveCallback = (ReorderableList list) =>
        {
            abilities.DeleteArrayElementAtIndex(list.index);
            foldouts = new bool[abilities.arraySize];
        };
    }

    // Skill �ν�����
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SkillSO skillData = (SkillSO)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Skill Settings", EditorStyles.boldLabel);
        showProfileFoldout = EditorGUILayout.Foldout(showProfileFoldout, "Profile");

        if (showProfileFoldout)
        {
            // ������ �̹��� �ʵ�
            skillData.Profile.ProfileImg = EditorGUILayout.ObjectField("Profile Image", skillData.Profile.ProfileImg, typeof(Sprite), false) as Sprite;

            // ������ �̸� �Է� �ʵ�
            skillData.Profile.Name = EditorGUILayout.TextField("Name", skillData.Profile.Name);

            // ������ ���� �Է� �ʵ�
            skillData.Profile.Description = EditorGUILayout.TextArea(skillData.Profile.Description, GUILayout.Height(60));
        }
        skillData.Type = (SkillType)EditorGUILayout.EnumPopup("Skill Type", skillData.Type);
        skillData.ShapeType = (SkillShapeType)EditorGUILayout.EnumPopup("Shape Type", skillData.ShapeType);
        skillData.SkillSize = EditorGUILayout.FloatField("Skill Size", skillData.SkillSize);
        skillData.TargetType = EditorGUILayout.IntField("Target Type", skillData.TargetType);
        skillData.CoolTime = EditorGUILayout.FloatField("Cool Time", skillData.CoolTime);
        skillData.BulletType = EditorGUILayout.IntField("Bullet Type", skillData.BulletType);
        skillData.Damage = EditorGUILayout.FloatField("Damage", skillData.Damage);
        skillData.Speed = EditorGUILayout.FloatField("Speed", skillData.Speed);
        skillData.Reach = EditorGUILayout.FloatField("Reach", skillData.Reach);
        skillData.HasMovementAction = EditorGUILayout.Toggle("Has Movement Action", skillData.HasMovementAction);

        if (skillData.HasMovementAction)
        {
            skillData.MovementRange = EditorGUILayout.FloatField("Movement Range", skillData.MovementRange);
        }

        EditorGUILayout.Space();
        reorderableList.DoLayoutList();

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(skillData);
        }
    }
}
