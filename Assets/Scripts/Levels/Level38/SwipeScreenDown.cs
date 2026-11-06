using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeScreenDown : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerDownHandler
{
    private GameObject camera;
    private GameObject background;
    [SerializeField] private GameObject detector;

    public GameObject CAMERA => camera;
    public GameObject BACK => background;
    
    private void Start()
    {
        camera = GameObject.FindWithTag("MainCamera");
        background = GameObject.FindWithTag("Background");
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var delta = eventData.delta;

        if (Math.Abs(delta.x) < Math.Abs(delta.y))
        {
            if (delta.y > 0)
            {
                camera.transform.DOMoveY(-10f, 0.3f).SetEase(Ease.Linear);
                background.transform.DOMoveY(-10f, 0.3f).SetEase(Ease.Linear);
                gameObject.SetActive(false);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
