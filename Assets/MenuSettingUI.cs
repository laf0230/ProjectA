using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSettingUI : MonoBehaviour
{
    [SerializeField] private Button �����ϱ�;
    [SerializeField] private Button �ٽ��ϱ�;
    [SerializeField] private Button ���ư���;
    private void Start()
    {
        �����ϱ�.onClick.AddListener(OnClick�����ϱ�Button);
        �ٽ��ϱ�.onClick.AddListener(OnClick�ٽ��ϱ�Button);
        ���ư���.onClick.AddListener(OnClick���ư���Button);
    }

    public void OnClick�����ϱ�Button()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnClick�ٽ��ϱ�Button()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClick���ư���Button()
    {
        gameObject.SetActive(false);
    }
}
