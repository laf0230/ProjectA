using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonUIInterection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color BaseColor;
    public Color EnteredColor = new Color(0.3604485f, 0.4151512f, 0.4716981f, 0.6f);

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("진입");
        var image = gameObject.GetComponent<Image>();
        BaseColor = image.color;
        image.color = EnteredColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("나가기");
        var image = gameObject.GetComponent<Image>();
        image.color = BaseColor;
    }
}
