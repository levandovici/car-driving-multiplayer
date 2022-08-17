using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
{
    [SerializeField]
    private Button _button;



    public event Action OnPointerEnterEvent;

    public event Action OnPointerDownEvent;
    
    public event Action OnPointerExitEvent;
    
    public event Action OnPointerUpEvent;



    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnterEvent?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownEvent?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerExitEvent?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpEvent?.Invoke();
    }



    public void OnDestroy()
    {
        OnPointerEnterEvent = null;

        OnPointerDownEvent = null;
        
        OnPointerExitEvent = null;
        
        OnPointerUpEvent = null;
    }
}
