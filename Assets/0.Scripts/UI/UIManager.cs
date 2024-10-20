using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public List<GameObject> UIList = new List<GameObject>();
    public List<UI> uis = new List<UI>();
    public ItemInfoUI itemInfo;
    // private UIWindow currentOpenUI;

    internal void SetCharacter(List<GameObject> selectedCharacters)
    {
        foreach(GameObject ui in UIList)
        {
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (UI ui in uis)
        {
            if (ui == null) return;

            if (ui.button != null)
            {
                ui.button.onClick.AddListener(() =>
                {
                    // ��ư Ŭ�� �� ������ â ��� ����
                    if (ui.openUIElement.Count < 0) return;
                    ui.openUIElement.ForEach(ui => ui.gameObject.SetActive(true));

                    // ��ư Ŭ�� �� ������ â ��� ����
                    if (ui.closeUIElement.Count < 0) return;
                    ui.closeUIElement.ForEach(ui => ui.gameObject.SetActive(false));
                });
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

/*
    public void OpenUI(CharacterCard card)
    {
        if(currentOpenUI != null)
        {
            currentOpenUI.Hide();
        }
    }
*/

    public void GameStart()
    {
        EnableSelectCharacterUI();
    }

    public void InvokeUI(string uiName)
    {

        foreach (var ui in uis)
        {
            if(ui.name == uiName)
            {
                ui.button.onClick.Invoke();
            }
        }
    }

    public void EnableSelectCharacterUI()
    {
            // ĳ���� �����ϴ� UI���� ���� ���� �ڵ�
        foreach(UI ui in uis)
        {
            // ��� ui �ݱ�
            foreach(GameObject uiElement in ui.openUIElement)
            {
                uiElement.SetActive(false);
            }

            // ĳ���� ����â�� ����
            if (ui.name == "CharacterSelect")
                foreach (var item in ui.openUIElement)
                {
                    item.gameObject.SetActive(true);
                }
        }
    }

    public void CharacterManagementUI()
    {
        foreach (var ui in uis)
        {
            if(ui.name == "CharacterManage")
            {
                ui.button.onClick.Invoke();
            }
        }
    }

}


[System.Serializable]
public class UI
{
    public string name;
    public Button button;
    public List<GameObject> openUIElement = new List<GameObject>();
    public List<GameObject> closeUIElement = new List<GameObject>();
}
/*

public class CharacterCard : MonoBehaviour
{
    public string characterId;

    private void OnMouseDown()
    {
        GameManager.Instance.UIManager.OpenUI(this);
    }
}

public class UIWindow : MonoBehaviour
{
    public GameObject uiPannel;

    public void Show()
    {
        uiPannel.SetActive(true);
    }

    public void Hide()
    {
        uiPannel.SetActive(false);
    }
}
*/
