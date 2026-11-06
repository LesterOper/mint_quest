using System;
using System.Collections;
using System.Collections.Generic;
using App;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragOneAxis : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerDownHandler, IPointerUpHandler
{
    //[SerializeField] private List<AqulizerController> _controllers;
    [SerializeField] private GameObject target;
    [SerializeField] private float offset; 
    private float xPos;
    private Vector3 mOffset;
    private Vector3 startPos;
    private float mZCoord;

    private void Start()    
    {
        xPos = transform.position.x;
    }

    private void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xPos, xPos), Mathf.Clamp(transform.position.y, -3.7f, -2.17f));
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        var pos = transform.position;
        if (target != null)
        {
            if (Vector2.Distance(pos, target.transform.position) <= offset)
            {
                GetComponent<IDraggableItems>().moveItem(target.transform.position);
            }
            else
            {
                GetComponent<AqulizerController>().isCorrectPos = false;
                //transform.position = startPos;
            }
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
}
