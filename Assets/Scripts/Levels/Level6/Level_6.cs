using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class Level_6 : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private SpriteRenderer ropeBlack;
    private Vector3 mOffset;
    private float mZCoord;
    private Vector3 startPos;
    private bool isPointerDown = false;
    private bool isComplete = false;
    private Touch[] touchPos;
    private float target = 46.5f;

    private void Start()
    {
        startPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var pos = transform.localPosition;
        transform.position = new Vector3(transform.position.x, GetMouseWorldPos().y + mOffset.y, 0);
        if(!isComplete)
            if (pos.y <= target)
            {
                isComplete = true;
                GetComponent<BoxCollider2D>().enabled = false;
                SpecialEffect();
            }
    }

    private void SpecialEffect()
    {
        isComplete = true;
        GetComponent<BoxCollider2D>().enabled = false;
        Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isComplete)
        {
            Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
            Debug.Log("DOWN");
            touchPos = Input.touches;
            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            mOffset = gameObject.transform.position - GetMouseWorldPos();
            transform.DOKill();
            isPointerDown = true;
            Debug.Log(isPointerDown);
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        //x,y
        Vector3 mousePoint = Input.mousePosition;
        //z
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //var count = eventData.clickCount;
        var count = eventData.currentInputModule.input.touchCount;
        Debug.Log("UP UNCOMPLETE:  " + count);
        touchPos = Input.touches;
        Debug.Log("UP TOUCHES" + touchPos.Length);
        if (!isComplete && count == 1)
        {
            Debug.Log("UP COMPLETE");
            isPointerDown = false;
            //Invoke(nameof(ResetRobe), 0.5f);
            if (!isPointerDown)
            {
                Invoke(nameof(ResetRobe), 0.15f);
            }
        }
    }

    private void ResetRobe()
    {
        Debug.Log("RESET: " + isPointerDown);
        if(!isPointerDown)
            transform.DOMove(startPos, 5f).SetEase(Ease.Linear);
    }
}