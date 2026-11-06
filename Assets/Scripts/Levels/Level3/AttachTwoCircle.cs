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

public class AttachTwoCircle : MonoBehaviour, IDraggableItems
{
    [SerializeField] private SpriteRenderer _circleSprite;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject ring;
    [SerializeField] private GameObject platform;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private Transform targetJump;
    private int countDetectedPlatform = 0;
    public void moveItem(Vector2 pos)
    {
        Context.SoundPlayer.PlaySoundClick("right_do", AudioMixerController.MasterMixerGroup.SFX, 1);
        _circleSprite.DOFade(1, 0.2f);
        transform.DOMove(pos, 0.1f).SetEase(Ease.Linear);
        attachToParentObject();
    }

    public void attachToParentObject()
    {
        transform.SetParent(parent.transform);
        transform.GetComponent<CircleCollider2D>().enabled = false;
        platform.SetActive(true);
        ring.GetComponent<SpriteRenderer>().DOFade(1, 0.4f);
        parent.GetComponent<CircleCollider2D>().enabled = true;
    }

    public void specialEffect()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(parent.transform.DOMoveY(-3.9f, 0.7f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        }));
        sequence.Append(parent.transform.DOJump(targetJump.position, 2.5f, 1, 1f).SetEase(animCurve).OnComplete(() =>
        {
        }));
        sequence.OnComplete(() =>
        {
            ring.GetComponent<SpriteRenderer>().DOFade(0, 0f);
            Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
        });
    }
}