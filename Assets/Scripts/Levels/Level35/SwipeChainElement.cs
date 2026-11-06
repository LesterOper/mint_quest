using System;
using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeChainElement : MonoBehaviour, IBeginDragHandler, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private ChainController _controller;
    [SerializeField] private bool isSwipeToLeft;
    [SerializeField] private BoxCollider2D _collider2D;
    private Vector2 pointDown;
    private Vector2 pointUp;

    public void OnBeginDrag(PointerEventData eventData)
    {
        var delta = eventData.delta;
        if (Math.Abs(delta.x) > Math.Abs(delta.y))
        {
            Context.SoundPlayer.PlaySoundClick("turn_move_1", AudioMixerController.MasterMixerGroup.SFX, 1);
            switch (isSwipeToLeft)
            {
                case true :
                    if (delta.x < 0)
                    {
                        if (_controller.IncrementCountAndCheckChain(gameObject))
                        {
                            transform.DOMoveX(-10f, 0.45f).SetEase(Ease.Linear);
                            _collider2D.enabled = false;
                        }
                    }
                    else
                    {
                        _controller.ResetChain();
                    }
                    break;
                case false:
                    if (delta.x > 0)
                    {
                        if (_controller.IncrementCountAndCheckChain(gameObject))
                        {
                            transform.DOMoveX(10f, 0.45f).SetEase(Ease.Linear);
                            _collider2D.enabled = false;
                        }
                    }
                    else
                    {
                        _controller.ResetChain();
                    }
                    break;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        pointDown = Camera.main.ScreenToWorldPoint(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointUp = Camera.main.ScreenToWorldPoint(eventData.position);
    }
}