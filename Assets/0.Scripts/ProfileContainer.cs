using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void SetProfiles(List<CardSO> profilList)
    {
        foreach (CardSO profile in profilList)
        {
            AddProfile(profile);
        }
    }
}
