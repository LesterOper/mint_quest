using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private float _endRotateZ = 0;
    [SerializeField] private bool _ifNeedRotateToEndPos = true;
    [SerializeField] private float _errorOffset = 8f;
    private Vector3 movement;
    private bool isRotate;
    private float speed = 4.5f;
    private Vector3 mOffset;
    private float mZCoord;
    private Vector2 mouseStartPos;
    private float defference = 0;
    private Vector3 screenPos;
    private Vector3 worldPos;
    private bool isFirst = true;

    private void Start()
    {
        mouseStartPos = Input.mousePosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        mouseStartPos = Camera.main.ScreenToWorldPoint(screenPos);
    }

    public void OnDrag(PointerEventData eventData)
    {
        screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = worldPos - (transform.localPosition.normalized);
        var dist = direction.magnitude;
        direction = direction / dist;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 60f;
        
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, rotation, 2.5f * Time.deltaTime);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        int rotationZ = (int) transform.eulerAngles.z;

        if (_ifNeedRotateToEndPos)
        {
            if (Math.Abs(_endRotateZ - rotationZ) <= _errorOffset || Math.Abs(360 - rotationZ) <= _errorOffset)
            {
                transform.DORotate(new Vector3(0, 0, _endRotateZ), 0.15f).SetEase(Ease.Linear)
                    .OnComplete((() => Messenger.Broadcast(Messages.DRAG_ITEM_CHANGED_CORRECT_ROTATION_TO_END)));
            }
            else
            {
                transform.DORotate(new Vector3(0, 0, rotationZ), 0.3f).SetEase(Ease.Linear)
                    .OnComplete((() => Messenger.Broadcast(Messages.DRAG_ITEM_CHANGED_ROTATION_TO_END)));
            }
        }
        else
        {
            transform.DORotate(new Vector3(0, 0, rotationZ), 0.3f).SetEase(Ease.Linear)
                .OnComplete((() => Messenger.Broadcast(Messages.DRAG_ITEM_CHANGED_ROTATION_TO_END)));
        }

        isRotate = false;
        isFirst = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isRotate = true;
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
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