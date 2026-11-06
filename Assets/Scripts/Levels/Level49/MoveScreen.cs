using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveScreen : MonoBehaviour, IDragHandler, IPointerDownHandler, IBeginDragHandler, IPointerUpHandler
{
    private Vector2 posDown;
    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        posDown = eventData.pointerCurrentRaycast.screenPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var delta = eventData.delta;
        if (Vector2.Distance(posDown, eventData.position) <= 90f)
        {
            if (Math.Abs(delta.x) > Math.Abs(delta.y))
            {
                if (delta.x < 0)
                {
                    Context.SoundPlayer.PlaySoundClick("turn_move_1");
                    GetComponent<RectTransform>().DOAnchorPosX(-1000f, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                       Invoke(nameof(ActivateCool), 0.2f);
                    });
                }
            }
        }
    }

    private void ActivateCool()
    {
        Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
        Invoke(nameof(DeactivateCanvas), 0.6f);
    }

    private void DeactivateCanvas()
    {
        gameObject.SetActive(false);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
