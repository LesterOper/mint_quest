using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;


public class SwiperBlind : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler
{
    [SerializeField] private GameObject[] blinds;
    [SerializeField] private GameObject detector;
    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var delta = eventData.delta;

        if (Math.Abs(delta.x) < Math.Abs(delta.y))
        {
            if (delta.y < 0)
            {
                detector.SetActive(false);
                CloseBlinds();
            }
        }
    }

    private void CloseBlinds()
    {
        int rand = Random.Range(0, 2);
        
        Sequence sequence = DOTween.Sequence();
        if (rand == 1)
        {
            foreach (GameObject blind in blinds)
            {
                sequence.Join(blind.transform.DOScaleY(1, 0.6f).SetEase(Ease.Linear));
            }
        }
        else
        {
            foreach (GameObject blind in blinds)
            {
                sequence.Append(blind.transform.DOScaleY(1, 0.15f).SetEase(Ease.Linear));
            }
        }

        sequence.OnComplete(() => { Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM); });
    }
}
