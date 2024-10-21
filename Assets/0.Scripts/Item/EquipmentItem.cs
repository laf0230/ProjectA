using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentItem
{
    public string name; // ��� ������ �̸�
    public string description; // ��� ������ ����
    public Currency buyPrise; // ���� ����
    public Currency sellPrise; // �Ǹ� ����
    public List<AbilityInfo> abilityInfo = new List<AbilityInfo>(); // �������� �����ϴ� �ɷ� ����
}

