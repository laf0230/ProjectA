using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class RankData
{
    public CardSO characterCard;
    public int killCount = 0;
}

public class RankingElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private Image profileImage;
    [SerializeField] private TextMeshProUGUI nameSpace;
    [SerializeField] private TextMeshProUGUI killCountSpace;
    [SerializeField] private GameObject selectedIcon;

    private int rank;
    private Sprite profile;
    private string characterName;
    private int killCount;
    private bool isCharacterSelected;

    public void Initialize(RankData data, int rank)
    {
        profile = data.characterCard.profile;
        characterName = data.characterCard.profile.name;
        killCount = data.killCount;
        this.rank = rank;

        SetProfile(profile);
        SetName(characterName);
        SetKillCount(killCount);
        SetRank(rank);
    }

    public void SetIsSelected(bool isSelected)
    {
        isCharacterSelected = isSelected;

        selectedIcon.SetActive(isSelected);
    }

    #region Setter

    private void SetRank(int rank)
    {
        rankText.text = rank.ToString();
    }

    private void SetProfile(Sprite profile)
    {
        profileImage.sprite = profile;
    }

    private void SetName(string name)
    {
        characterName = name;
    }

    private void SetKillCount(int killCount)
    {
        killCountSpace.text = killCount.ToString();
    }

    #endregion
}
