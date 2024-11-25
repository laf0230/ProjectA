using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileContainer : MonoBehaviour
{
    public Transform container;
    public List<GameObject> profilList;
    public GameObject ProfilePrefab;

    public void AddProfile(CardSO card)
    {
        GameObject profileObject = Instantiate(ProfilePrefab, container);
        ProfileUI profilUI = profileObject.GetComponent<ProfileUI>();
        profilList.Add(profileObject);
        profilUI.SetCardCharacter(card);
    }

    public void SetProfileSelected(CardSO cardSO)
    {
            foreach (var profile in profilList)
            {
                var profile_ = profile.GetComponent<ProfileUI>();
                if (profile_.characterCard.character.Profile.Name == cardSO.character.Profile.Name)
                {
                    // 선택중인 프로필
                    profile_.SetLookState(true);
                }
                else
                {
                    // 선택중이 아닌 프로필
                    profile_.SetLookState(false);
                }
            }
    }

    public void SetProfiles(List<CardSO> profilList)
    {
        foreach (CardSO profile in profilList)
        {
            AddProfile(profile);
        }
    }
}
