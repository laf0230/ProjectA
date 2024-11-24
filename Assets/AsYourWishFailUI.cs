using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsYourWishFailUI : MonoBehaviour
{
    [SerializeField] private Button gameEndButton;

    private void Start()
    {
        gameEndButton.onClick.AddListener(OnGameEndButtonClick);
    }

    public void OnGameEndButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
