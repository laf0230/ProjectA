using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsYourWishUI : MonoBehaviour
{
    [SerializeField] private Image characterIlust;
    [SerializeField] private TextMeshProUGUI wishDescription;
    [SerializeField] private Button endButton;

    private CardSO wishAchiever;

    public void SetReachedCharacter(CardSO cardSO)
    {
        wishAchiever = cardSO;
        characterIlust.sprite = wishAchiever.fullIllust;
        wishDescription.text = wishAchiever.reachingWishText;
    }

    public void ActiveAsYourWish()
    {
        gameObject.SetActive(true);
        endButton.onClick.AddListener(OnclickGameEnd);
    }

    public void OnclickGameEnd()
    {
        SceneManager.LoadScene(0);
    }
}
