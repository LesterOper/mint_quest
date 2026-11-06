using System;
using System.Collections;
using System.Collections.Generic;
using App;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class CubeSwiper : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private CubeSwiperHelper _helper;
    private Vector3 pointDown, pointUp;

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var delta = eventData.delta;
        string dir = "";
        if (Math.Abs(delta.x) > Math.Abs(delta.y))
        {
            if (delta.x > 0)
                dir = "right";

            else
                dir = "left";
        }
        else
        {
            if (delta.y > 0)
                dir = "up";
            else
                dir = "down";
        }

        Context.SoundPlayer.PlaySoundClick("turn_move_1");
        _helper.ChechDirectionSwipe(dir);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        pointDown = Camera.main.ScreenToWorldPoint(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointUp = Camera.main.ScreenToWorldPoint(eventData.position);
        if (Vector3.Distance(pointDown, pointUp) <= 0.2)
        {
            _helper.ResetEdges();
        }
    }
}