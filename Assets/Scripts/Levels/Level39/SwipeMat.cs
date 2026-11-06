using System;
using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeMat : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler
{
    [SerializeField] private BoxCollider2D keyCollider;
    [SerializeField] private BoxCollider2D matCollider;
    private Vector2 startPos;
    private void Start()
    {
        startPos = transform.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var delta = eventData.delta;

        if (Math.Abs(delta.x) > Math.Abs(delta.y))
        {
            if (delta.x > 0)
            {
                transform.position += new Vector3(delta.x/275f, 0 ,0);
            }
            else 
                transform.position += new Vector3(delta.x/275f, 0 ,0);
        }

        if (Vector2.Distance(transform.position, startPos) >= 2f)
        {
            keyCollider.enabled = true;
            matCollider.enabled = false;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        /*var delta = eventData.delta;
        if (Math.Abs(delta.x) > Math.Abs(delta.y))
        {
            if (delta.x < 0)
            {
                transform.DOLocalMoveX(-35.4f, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    keyCollider.enabled = true;
                    matCollider.enabled = false;
                });
            }
            else
            {
                transform.DOLocalMoveX(-30.22f, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    keyCollider.enabled = true;
                    matCollider.enabled = false;
                });
            }
        }*/
    }
}
