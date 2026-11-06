using System;
using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject target;
    [SerializeField] private float offset;
    public bool isDrag;
    public bool isNeedToAffect = false;
    private bool isDragExitLevel;
    
    private Vector3 mOffset;
    private Vector3 startPos;
    private float mZCoord;
    private float scale;
    private Tween shaker;

    public Tween Shaker => shaker;

    private void Start()
    {
        startPos = transform.position;
        scale = transform.localScale.x;
        if(isNeedToAffect)
            shaker = ObjectsDeformationManager.Shake(transform);
        //shaker.Play();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //if(eventData.currentInputModule.input.touchCount == 1)
            transform.position = GetMouseWorldPos() + mOffset;
        isDragExitLevel = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isDrag = false;
        if(isNeedToAffect)
            ObjectsDeformationManager.ScaleObject(scale, transform, 0.15f);
        var pos = transform.position;
        if (target != null)
        {
            if (Vector2.Distance(pos, target.transform.position) <= offset)
            {
                GetComponent<IDraggableItems>().moveItem(target.transform.position);
            }
            else
            {
                transform.DOMove(startPos, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    shaker.Play();
                });
            }
        }
        else
        {
            transform.DOMove(startPos, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
            {
                shaker.Play();
            });
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        shaker.Pause();
        if(isNeedToAffect)
            ObjectsDeformationManager.ScaleObject(scale * 1.2f, transform, 0.15f);
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        isDrag = true;
    }
    
    private Vector3 GetMouseWorldPos()
    {
        //x,y
        Vector3 mousePoint = Input.mousePosition;
        //z
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public bool getDrag()
    {
        return isDrag;
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    public void SetStartPos()
    {
        startPos = transform.position;
    }

    public Vector3 GetStartPos()
    {
        return startPos;
    }
}
