using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum SkillType
{
    Attack,
    Skill,
    Ultimate,
    Passive
}

public enum StatusList
{
    Health, // ü��
    Speed, // �̵��ӵ�
    AttackSpeed, // ���� �ӵ�
}

public enum SkillShapeType
{
    Circle,
    Rectangle
}

[CreateAssetMenu(fileName = "NewSkillData", menuName = "New Skill Data")]
public class SkillSO : ScriptableObject, ISkillData
{
    [SerializeField] private Profile profile;
    [SerializeField] private SkillType type;
    [SerializeField] private SkillShapeType shapeType;
    [SerializeField] private float skillSize;
    [SerializeField] private int targetType; // ��� Ÿ�� (���� ���, ���� ��� ��)
    [SerializeField] private float coolTime;
    [SerializeField] private int bulletType = 1;
    [SerializeField] private List<Transform> targets; // �������� ����
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float reach;
    [SerializeField] private bool hasMovementAction;
    [SerializeField] private MovementActionType movementActionType;
    [SerializeField] private TargetMovementLocaction targetMovementLocaction; 
    [SerializeField] private float movementRange; 
    [SerializeField] private List<AbilityInfo> ability;

    #region Getter / Setter
    
    // Properties to access private fields
    public Profile Profile { get => profile; set => profile = value; }
    public SkillType Type { get => type; set => type = value; }
    public SkillShapeType ShapeType { get => shapeType; set => shapeType = value; }
    public float SkillSize { get => skillSize; set => skillSize = value; }
    public int TargetType { get => targetType; set => targetType = value; }
    public float CoolTime { get => coolTime; set => coolTime = value; }
    public int BulletType { get => bulletType; set => bulletType = value; }
    public List<Transform> Targets { get => targets; set => targets = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Reach { get => reach; set => reach = value; }
    public bool HasMovementAction { get => hasMovementAction; set => hasMovementAction = value; }
    public MovementActionType MovementActionType { get => movementActionType; set => movementActionType = value; }
    public TargetMovementLocaction TargetMovementLocaction { get => targetMovementLocaction; set => targetMovementLocaction = value; }
    public float MovementRange { get => movementRange; set => movementRange = value; }
    public List<AbilityInfo> Ability { get; set; } = new List<AbilityInfo>();
    #endregion
}

[CustomEditor(typeof(SkillSO))]
public class SkillSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SkillSO skillSO = (SkillSO)target;

        // Update the serialized object
        serializedObject.Update();

        // Draw all the fields
        EditorGUILayout.PropertyField(serializedObject.FindProperty("profile"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("type"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("shapeType"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("skillSize"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("targetType"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("coolTime"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("bulletType"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("damage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("speed"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("reach"));

        // Draw the hasMovementAction field and conditionally show other fields
        EditorGUILayout.PropertyField(serializedObject.FindProperty("hasMovementAction"));

        if (skillSO.HasMovementAction)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("movementActionType"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("targetMovementLocaction"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("movementRange"));
        }

        // Draw the ability list (you might want to use a reorderable list for this)
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ability"), true);

        // Apply any changes made to the serialized object
        serializedObject.ApplyModifiedProperties();
    }
}
