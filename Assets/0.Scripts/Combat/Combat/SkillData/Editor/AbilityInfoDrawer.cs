using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(AbilityInfo))]
public class AbilityInfoDrawer : PropertyDrawer
{
    private bool foldout = false; // 접기 상태 저장
    private Vector2 scrollPos; // 스크롤 위치 저장

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // 접기 상태 표시
        foldout = EditorGUI.Foldout(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), foldout, label.text);

        if (foldout)
        {
            // ScrollView 시작
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(0)); // 스크롤 높이 조절

            // Get properties
            SerializedProperty nameProp = property.FindPropertyRelative("Name");
            SerializedProperty idProp = property.FindPropertyRelative("ID");
            SerializedProperty isPercentageProp = property.FindPropertyRelative("IsPercentage");
            SerializedProperty valueProp = property.FindPropertyRelative("Value");
            SerializedProperty durationProp = property.FindPropertyRelative("Duration");
            SerializedProperty targetTypeProp = property.FindPropertyRelative("TargetType");
            SerializedProperty shapeProp = property.FindPropertyRelative("shape");
            SerializedProperty animationTypeProp = property.FindPropertyRelative("AnimationType");
            SerializedProperty effectStatusProp = property.FindPropertyRelative("EffectStatus");
            SerializedProperty hasMovementProp = property.FindPropertyRelative("HasMovement");
            SerializedProperty MovementActionType = property.FindPropertyRelative("MovementActionType");

            // 필드 그리기
            EditorGUILayout.PropertyField(nameProp, new GUIContent("Ability Name"));
            EditorGUILayout.PropertyField(idProp, new GUIContent("Ability ID"));
            EditorGUILayout.PropertyField(isPercentageProp, new GUIContent("Is Percentage"));

            // 스위치문으로 ID에 따라 다른 필드 표시
            switch (idProp.intValue)
            {
                case 0:
                    EditorGUILayout.PropertyField(effectStatusProp, new GUIContent("Effect Status"));
                    break;
                default:
                    break;
            }

            EditorGUILayout.PropertyField(valueProp, new GUIContent("Value"));
            EditorGUILayout.PropertyField(durationProp, new GUIContent("Duration"));
            EditorGUILayout.PropertyField(targetTypeProp, new GUIContent("Target Type"));
            EditorGUILayout.PropertyField(shapeProp, new GUIContent("Shape"));
            EditorGUILayout.PropertyField(animationTypeProp, new GUIContent("Animation Type"));
            EditorGUILayout.PropertyField(hasMovementProp, new GUIContent("Has Movement"));
            if (hasMovementProp.boolValue)
            {
                EditorGUILayout.PropertyField(MovementActionType, new GUIContent("Movement Action Type"));
            }

            // ScrollView 끝
            EditorGUILayout.EndScrollView();
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // 접기 상태에 따라 높이 조정
        return EditorGUIUtility.singleLineHeight;
    }
}
