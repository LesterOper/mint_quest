using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoorOpenController : MonoBehaviour, IDragHandler, IPointerUpHandler, IBeginDragHandler, IPointerDownHandler
{

    [SerializeField] private DoorController _doorController;
    public bool canRotate = true;
    public void OnDrag(PointerEventData eventData)
    {
        var pos = eventData.delta;
        if (Math.Abs(eventData.delta.x) > Math.Abs(eventData.delta.y))
        {
            if (eventData.delta.x < 0 && canRotate)
            {
                var rotate = transform.eulerAngles.y;
                transform.DORotate(new Vector3(0, rotate + (eventData.delta.x * 1.9f), 0f), 0.2f).OnComplete(() => canRotate = true).SetEase(Ease.Linear);
                canRotate = false;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(transform.localRotation.y*100);
        if (transform.localRotation.y <= -0.30f)
        {
            Camera.main.orthographic = true;
            Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
        }
        else
        {
            transform.DORotate(new Vector3(0, 0, 0), 0.2f);
            _doorController.DiasableDoor();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }
}
