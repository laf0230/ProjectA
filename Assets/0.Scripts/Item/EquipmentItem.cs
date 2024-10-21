using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentItem
{
    public string name; // 장비 아이템 이름
    public string description; // 장비 아이템 설명
    public Currency buyPrise; // 구매 가격
    public Currency sellPrise; // 판매 가격
    public List<AbilityInfo> abilityInfo = new List<AbilityInfo>(); // 아이템이 제공하는 능력 정보
}

