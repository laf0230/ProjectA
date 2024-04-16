using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIPreset 
{
    [SerializeField]
    public GameObject UI;
    public List<Button> openUIBtn;
    public List<Button> closeUIBtn;
}

public class GameStart_UIManager : MonoBehaviour
{
    [ArrayElementTitle("UI")]
    [SerializeField]
    public List<UIPreset> UIPresetList;

    // Start is called before the first frame update
    void Start()
    {
        #region UI Init
        foreach (UIPreset p in UIPresetList)
        {
            if (p != null)
            {
                if (p.openUIBtn != null)
                {
                    foreach (Button openBtn in p.openUIBtn)
                    {
                        openBtn.onClick.AddListener(() =>
                        {
                            openUI(p.UI);
                        });
                     }
                }
                if (p.closeUIBtn != null)
                {
                    foreach (Button closeBtn in p.closeUIBtn)
                    {
                        closeBtn.onClick.AddListener(() =>
                        {
                            closeUI(p.UI);
                        });
                    }
                }
            }
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void openUI(GameObject ui)
    {
        ui.SetActive(true);
    }

    public void closeUI(GameObject ui)
    {
        ui.SetActive(false);
    }

}
