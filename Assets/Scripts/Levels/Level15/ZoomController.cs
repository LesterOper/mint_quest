using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using Swimmer.Library.Common;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public float amountToZoom;
    private Vector2 touchFirstPos, touchSecondPos;
    private Vector3 startScale;
    [SerializeField] private bool isSpriderLevel, isDoorLevel, isPuzzle;
    
    private void Start()
    {
        startScale = transform.localScale;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var inputEvent = eventData.currentInputModule.input;
        if (inputEvent.touchCount < 2) return;
        if (inputEvent.touchCount == 2)
        {
            if (touchFirstPos == Vector2.zero && touchSecondPos == Vector2.zero)
            {
                touchFirstPos = inputEvent.GetTouch(0).position;
                touchSecondPos = inputEvent.GetTouch(1).position;
            }

            Vector2 f1StartPos = inputEvent.GetTouch(0).position;
            Vector2 f2StartPos = inputEvent.GetTouch(1).position;
            
            var startDistance = Vector2.Distance(touchSecondPos, touchFirstPos);
            var newDistance = Vector2.Distance(f2StartPos, f1StartPos);
            
            if (newDistance > startDistance)
            {
                float zoom = 0;
                if (isDoorLevel)
                {
                    zoom = amountToZoom;
                }
                else if(isSpriderLevel)
                {
                    zoom = 1.1f;
                    
                }
                else if (isPuzzle)
                {
                    zoom = amountToZoom;
                }
                GetComponent<IZoom>().Scale(zoom);
            }
        }
        else
        {
            touchSecondPos = touchFirstPos = Vector3.zero;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
