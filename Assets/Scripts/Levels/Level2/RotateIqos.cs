using System;
using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateIqos : MonoBehaviour, IDragHandler, IPointerDownHandler, IBeginDragHandler, IPointerUpHandler
{
    [SerializeField] private DragController _dragController;
    [SerializeField] private SupportLevelScript _script;
    
    private Touch t;
    private Vector2 movement;
    private bool enableRotate = false;
    private float moveSpeed = 2f;
    private bool isCorrectDir = false;

    private Vector2 touchSecondPos = Vector2.zero;
    private Vector2 touchFirst = Vector2.zero;
    void Update()
    {
        if (Input.touchCount == 1)
        {
            if (touchFirst == Vector2.zero)
            {
                touchFirst = Input.GetTouch(0).position;
                _dragController.enabled = true;
            }
        }
        else if (Input.touchCount == 2)
        {
            _dragController.enabled = false;
            if (Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                if (touchSecondPos == Vector2.zero)
                {
                    touchSecondPos = Input.GetTouch(1).position;
                }

                Vector2 sTouch = Input.GetTouch(1).position;

                float angle = Vector2.SignedAngle(transform.up, sTouch - touchFirst);
                if (Math.Abs(angle) >= 1e-3f)
                {
                    var angles = transform.eulerAngles;
                    angles.z += angle / 8;
                    transform.eulerAngles = angles;
                }
            }
        }
        else
        {
            touchFirst = touchSecondPos = Vector2.zero;
        }
    }

    public void setEnableRotate(bool enable)
    {
        enableRotate = enable;
    }

    public void OnDrag(PointerEventData eventData)
    {
        /*if (!enableRotate || _dragController.getDrag()) return;
        if (isCorrectDir)
        {
            Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            float angle = Vector2.SignedAngle(transform.up, worldPos - transform.position);
            if (Math.Abs(angle) >= 1e-3f)
            {
                var angles = transform.eulerAngles;
                angles.z += angle;
                transform.eulerAngles = angles;
            }
            Debug.Log(transform.eulerAngles.z);
            if (transform.eulerAngles.z <= 322f && transform.eulerAngles.z >= 318f)
            {
                transform.DORotate(new Vector3(0, 0, 320f), 0.15f).SetEase(Ease.Linear);
                setEnableRotate(false);
                _script.setAttachForIqos();
            }
        }*/
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isCorrectDir = eventData.delta.x > 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        /*if (!enableRotate)
        {
            _script.EnableIqosDrag();
        }*/
    }
}