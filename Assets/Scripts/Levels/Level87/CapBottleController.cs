using System;
using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class CapBottleController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private SpriteRenderer cap;
    [SerializeField] private SprayDragController spray;
    [SerializeField] private RotateBottle arrow;
    private Vector3 mOffset;
    private float mZCoord;


    private int count = 0;
    

    private void Start()
    {
        arrow.Active();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("click_1", AudioMixerController.MasterMixerGroup.SFX, 1);
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Vector2.Distance(spray.transform.position, transform.position) >= 1.2f)
        {
            //cap.DOFade(0, 0.25f).SetEase(Ease.Linear);
            spray.enabled = true;
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



    public void Increment()
    {
        count++;
        if (count == 2)
        {
            
        }
    }
}
