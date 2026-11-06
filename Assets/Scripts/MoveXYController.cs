using System.Collections;
using System.Collections.Generic;
using App;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveXYController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private bool isDrag;
    private Vector3 mOffset;
    private float mZCoord;
    public TYPE_DRAG _TYPE_SCROLL = TYPE_DRAG.VERTICAL;
    
    public enum TYPE_DRAG
    {
        VERTICAL = 0,
        HORIZONTAL = 1
    }
    
    public bool GetDragStatus()
    {
        return isDrag;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_TYPE_SCROLL == TYPE_DRAG.VERTICAL)
        {
            transform.position = new Vector3(transform.position.x, GetMouseWorldPos().y + mOffset.y, 0f);
        }
        else
        {
            transform.position = new Vector3(GetMouseWorldPos().x + mOffset.x, transform.position.y, 0f);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDrag = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
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
}
