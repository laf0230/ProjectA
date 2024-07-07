using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Image LoadingCharacterImage;
    public List<Sprite> LoadingCharacterSprites = new List<Sprite>();
    public bool isLoading = false;

    // [ArrayElementTitle("UI")]
    [SerializeField]
    public List<UIPreset> UIPresetList;

    private WaitForSeconds loadCharacterDelay = new WaitForSeconds(2f);

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

    public void OpenLoadScene()
    {
        StartCoroutine(LoadingScene(true));
    }

    IEnumerator LoadingScene(bool isGameStart)
    {
        isLoading = true;
        StartCoroutine(LoadingCharacter());
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("BattleGround1");
    }

    IEnumerator LoadingCharacter()
    {
        var i = 0;
        while (true)
        {
            i = Random.Range(0, LoadingCharacterSprites.Count - 1);
            if (LoadingCharacterSprites[i] == null)
                yield return null;
            LoadingCharacterImage.sprite = LoadingCharacterSprites[i];
            LoadingCharacterImage.SetNativeSize();
            yield return loadCharacterDelay;
        }
    }


}
