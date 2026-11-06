using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class CircleClick : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler
{
    [SerializeField] private GameObject scaled;
    public float amountToZoom;
    private bool isEnable;
    private Vector2 touchFirstPos, touchSecondPos;
    private Vector3 startScale;
    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    private void Update()
    {
        if (isEnable) return;
        if (Input.touchCount == 2)
        {
            if (touchFirstPos == Vector2.zero && touchSecondPos == Vector2.zero)
            {
                touchFirstPos = Input.GetTouch(0).position;
                touchSecondPos = Input.GetTouch(1).position;
            }

            Vector2 f1StartPos = Input.GetTouch(0).position;
            Vector2 f2StartPos = Input.GetTouch(1).position;
            
            var startDistance = Vector2.Distance(touchSecondPos, touchFirstPos);
            var newDistance = Vector2.Distance(f2StartPos, f1StartPos);
            
            if (newDistance > startDistance)
            {
                transform.DOScale(transform.localScale.x + 0.1f, 0.15f).SetEase(Ease.Linear);
                if (transform.localScale.x >= 1.85f && !isEnable)
                {
                    isEnable = true;
                    Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
                }
            }
        }
        else
        {
            touchSecondPos = touchFirstPos = Vector3.zero;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
