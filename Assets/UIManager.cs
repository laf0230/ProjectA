using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class UI: MonoBehaviour
{
    public List<UI> openList;
    public List<UI> closeList;

    public void OpenSelf()
    {
        gameObject.SetActive(true);
    }
    
    public void CloseSelf() 
    {
        gameObject.SetActive(false);
    }

    public void OpenUIs()
    {
        foreach (UI ui in openList)
        {
            ui.CloseSelf();
        }
    }

    public void CloseUIs()
    {
        foreach(UI ui in closeList)
        {
            ui.CloseSelf() ;
        }
    }
}
