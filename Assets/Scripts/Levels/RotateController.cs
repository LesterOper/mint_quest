using System;
using System.Collections;
using System.Collections.Generic;
using Levels;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private int angle;
    [SerializeField] private EnumRotateSide _rotateSide;
    public void OnDrag(PointerEventData eventData)
    { }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (Math.Abs(eventData.delta.x) > Math.Abs(eventData.delta.y))
        {
            
            if (eventData.delta.x > 0 && _rotateSide == EnumRotateSide.RightX)
            {
                GetComponent<IRotateItems>().RotateItem(angle);
            }
            else if(eventData.delta.x < 0 && _rotateSide == EnumRotateSide.LeftX)
            {
                GetComponent<IRotateItems>().RotateItem(-angle);
            }
        }
        else
        {
            if (eventData.delta.y > 0 && _rotateSide == EnumRotateSide.UpY)
            {
                GetComponent<IRotateItems>().RotateItem(angle);
            }
            else if(eventData.delta.y <= 0 && _rotateSide == EnumRotateSide.DownY)
            {
                GetComponent<IRotateItems>().RotateItem(-angle);
            }
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    { }
}
