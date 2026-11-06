using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClockController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler, IBeginDragHandler
{
    [SerializeField] private SpriteRenderer back;
    [SerializeField] private SpriteRenderer blackClockLarge;
    [SerializeField] private SpriteRenderer blackClockSmall;
    [SerializeField] private SpriteRenderer moon;
    [SerializeField] private GameObject mask;
    [SerializeField] private SpriteRenderer sun;
    private int countClick = 0;

    private float startAngle = 0;
    private Vector2 startPosMouse;
    private bool isCorrectDir = true;
    private bool isComplete;

    private void Effect()
    {
        back.DOFade(1f, 0.5f).SetEase(Ease.Linear).OnComplete(() => { AddToEffect(); });
    }

    private void AddToEffect()
    {
        sun.DOFade(0, 0.5f).SetEase(Ease.Linear).OnComplete(() => Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM));
        moon.DOFade(0, 0.2f).SetEase(Ease.Linear);
        blackClockLarge.DOFade(0, 0.3f).SetEase(Ease.Linear);
        blackClockSmall.DOFade(0, 0.3f).SetEase(Ease.Linear);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isCorrectDir)
        {
            Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            float Angle =
                Mathf.Atan2(worldPos.y - (transform.localPosition.y), worldPos.x - (transform.localPosition.x)) /
                Mathf.PI * 180f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Angle - 45f);

            var diffPercent = ((startAngle - Angle) / 250);
            Debug.Log(Angle + "   " + diffPercent);
            back.DOFade(diffPercent, 0.2f).SetEase(Ease.Linear);
            blackClockLarge.DOFade(diffPercent, 0.2f).SetEase(Ease.Linear);
            blackClockSmall.DOFade(diffPercent, 0.2f).SetEase(Ease.Linear);
            moon.DOFade(1 - diffPercent, 0.2f).SetEase(Ease.Linear);
            Debug.Log(transform.eulerAngles.z);
            if (back.color.a > 0.8f)
            {
                Effect();
                enabled = false;
                isComplete = true;
                return;
            }
        }

        //startAngle = Angle;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isComplete)
        {
            back.DOFade(0, 0.2f).SetEase(Ease.Linear);
            blackClockLarge.DOFade(0, 0.2f).SetEase(Ease.Linear);
            blackClockSmall.DOFade(0, 0.2f).SetEase(Ease.Linear);
            moon.DOFade(1, 0.2f).SetEase(Ease.Linear);
            transform.DORotate(new Vector3(0, 0, 70f), 0.2f).SetEase(Ease.Linear).OnComplete(() => Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startAngle = transform.eulerAngles.z;
        isCorrectDir = eventData.delta.x > 0;
    }
}