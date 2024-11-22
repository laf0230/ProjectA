using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterCard : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        var relatedUI = UIManager_.Instance.GetUIGroup(UIType_.ManagementUI);
        relatedUI.Active(true);
    }
}
