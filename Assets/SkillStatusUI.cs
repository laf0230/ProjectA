using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillStatusUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skillDescription;
    private SkillSO inventedSkillSO;

    internal void SetAndActiveSkillDescription(SkillSO skillSO)
    {
        if (inventedSkillSO == null || inventedSkillSO.Profile.Name != skillSO.Profile.Name)
        {
            gameObject.SetActive(true);
            Debug.Log("스킬이 보여집니다");
            // 다른 스킬일 때
            inventedSkillSO = skillSO;
            skillDescription.text = inventedSkillSO.Profile.Description;
        }
        else
        {
            inventedSkillSO = null;
            gameObject.SetActive(false);
        }
    }
}
