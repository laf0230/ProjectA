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
                    // 버튼 클릭 시 열리는 창 목록 지정
                    if (ui.openUIElement.Count < 0) return;
                    ui.openUIElement.ForEach(ui => ui.gameObject.SetActive(true));

                    // 버튼 클릭 시 닫히는 창 목록 지정
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
            // 캐릭터 선택하는 UI제외 전부 끄는 코드
        foreach(UI ui in uis)
        {
            // 모든 ui 닫기
            foreach(GameObject uiElement in ui.openUIElement)
            {
                uiElement.SetActive(false);
            }

            // 캐릭터 선택창만 열기
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
