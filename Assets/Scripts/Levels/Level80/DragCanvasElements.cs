using System;
using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragCanvasElements : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private RectTransform tr;
    [SerializeField] private RectTransform target;
    public bool isNeedToAffect;
    private Vector2 startPos;
    private float scale;
    private Vector3 mOffset;
    private float mZCoord;

    private void Start()
    {
        //startPos = tr.anchoredPosition;
        startPos = transform.position;
        scale = transform.localScale.x;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
            if (isNeedToAffect)
                ObjectsDeformationManager.ScaleObject(scale * 1.2f, transform, 0.25f);
            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isNeedToAffect)
            ObjectsDeformationManager.ScaleObject(scale, transform, 0.25f);
        /*if (Vector2.Distance(tr.anchoredPosition, target.anchoredPosition) <= 80f)
        {
            GetComponent<IDraggableItems>().moveItem(new Vector2(0,0));
        }
        else
        {
            tr.DOAnchorPos(startPos, 0.15f).SetEase(Ease.Linear);
        }*/
        if (Vector2.Distance(tr.position, target.position) <= 0.5f)
        {
            GetComponent<IDraggableItems>().moveItem(startPos);
        }
        else
        {
            tr.DOMove(startPos, 0.15f).SetEase(Ease.Linear);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = GetMouseWorldPos() + mOffset;
        //transform.position = eventData.pointerCurrentRaycast.screenPosition;
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
