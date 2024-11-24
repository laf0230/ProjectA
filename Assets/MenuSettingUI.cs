using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSettingUI : MonoBehaviour
{
    [SerializeField] private Button 종료하기;
    [SerializeField] private Button 다시하기;
    [SerializeField] private Button 돌아가기;
    private void Start()
    {
        종료하기.onClick.AddListener(OnClick종료하기Button);
        다시하기.onClick.AddListener(OnClick다시하기Button);
        돌아가기.onClick.AddListener(OnClick돌아가기Button);
    }

    public void OnClick종료하기Button()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnClick다시하기Button()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClick돌아가기Button()
    {
        gameObject.SetActive(false);
    }
}
