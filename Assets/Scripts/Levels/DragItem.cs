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

public class DragItem : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private GameObject _endDragPos;
    [SerializeField] private bool _ifNeedDragOnEndPos = true;
    [SerializeField] private float _errorOffset = 0.2f;
    [SerializeField] private bool isTriangle;
    private Vector3 mOffset;
    private float mZCoord;

    private void Start()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(eventData.currentInputModule.input.touchCount == 1)
            transform.position = GetMouseWorldPos() + mOffset;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        var position = transform.position;

        if (_ifNeedDragOnEndPos) 
        {
            if (Vector2.Distance(position, _endDragPos.transform.position) <= _errorOffset)
            {
                if (isTriangle)
                {
                    if (FindObjectOfType<ZoomTriangle>().isZoom)
                    {
                        transform.DOMove(_endDragPos.transform.position, 0.15f).SetEase(Ease.Linear)
                            .OnComplete((() => Messenger.Broadcast(Messages.DRAG_ITEM_CHANGED_POSITION_TO_END)));
                        Context.SoundPlayer.PlaySoundClick("right_do", AudioMixerController.MasterMixerGroup.SFX, 1);
                    }
                }
                else
                {
                    transform.DOMove(_endDragPos.transform.position, 0.15f).SetEase(Ease.Linear)
                        .OnComplete((() => Messenger.Broadcast(Messages.DRAG_ITEM_CHANGED_POSITION_TO_END)));
                    Context.SoundPlayer.PlaySoundClick("right_do", AudioMixerController.MasterMixerGroup.SFX, 1);
                }
            }
            else
            {
                transform.DOMove(position, 0.3f).SetEase(Ease.Linear)
                    .OnComplete((() => Messenger.Broadcast(Messages.DRAG_ITEM_CHANGED_POSITION_TO_END)));
            }
        }
        else
        {
            transform.DOMove(position, 0.3f).SetEase(Ease.Linear)
                .OnComplete((() => Messenger.Broadcast(Messages.DRAG_ITEM_CHANGED_POSITION_TO_END)));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        mZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        mOffset = transform.position - GetMouseWorldPos();
    }


    private Vector3 GetMouseWorldPos()
    {
        //x,y
        Vector3 mousePoint = Input.mousePosition;
        //z
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}