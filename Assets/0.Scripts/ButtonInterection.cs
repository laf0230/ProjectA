using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonInterection : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler
{
    Image image;
    Color mouseUpColor = new Color(0, 42, 255, 255);
    Color mouseDownColor = new Color(0, 16, 98, 108);
    Color mouseClickecColor = new Color(0, 0, 0, 108);

    public void OnPointerClick(PointerEventData eventData)
    {
        image = gameObject.GetComponent<Image>();
        
        image.color = mouseClickecColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        image = gameObject.GetComponent<Image>();
        
        image.color = mouseDownColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        image = gameObject.GetComponent<Image>();

        image.color = mouseUpColor;
    }
}
