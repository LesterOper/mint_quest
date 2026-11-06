using System;
using System.Collections;
using System.Collections.Generic;
using App;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToiletPaperMover : MonoBehaviour,  IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    private float timeCount;
    private Vector2 deltaValue = Vector2.zero;
    private float mZCoord;
    private Vector3 mOffset;

    public void ClearMoving()
    {
        GetComponent<Rigidbody2D>().AddForce(deltaValue.normalized*0, ForceMode2D.Impulse);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<Rigidbody2D>().mass = 0f;
        GetComponent<Rigidbody2D>().gravityScale = 0f;
    }

    public void OnBeginDrag(PointerEventData data)
    {
        deltaValue = Vector2.zero;
    }

    public void OnDrag(PointerEventData data)
    {
        deltaValue += data.delta;
        if (data.dragging)
        {
            timeCount += Time.deltaTime;
            if (timeCount > 0.1f)
            {
                timeCount = 0.0f;
            }
        }
        transform.position = new Vector3(GetMouseWorldPos().x + mOffset.x, GetMouseWorldPos().y +  mOffset.y, 0f);
    }

    private Vector3 GetMouseWorldPos()
    {
        //x,y
        Vector3 mousePoint = Input.mousePosition;
        //z
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    
    public void OnEndDrag(PointerEventData data)
    {
      //  Debug.Log(deltaValue.ToString());
      GetComponent<Rigidbody2D>().AddForce(deltaValue.normalized*15, ForceMode2D.Impulse);
      GetComponent<Rigidbody2D>().mass = 2f;
      GetComponent<Rigidbody2D>().gravityScale = 1f;
      GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
      GetComponent<ToiletPaperMover>().enabled = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
    }
}
