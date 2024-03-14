using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TouchEventUI : MonoBehaviour,IPointerDownHandler 
{
    public void OnPointerDown(PointerEventData eventData)
    {
        ComponentManager.instance.pufferPlayer.Jump();    
    }
}