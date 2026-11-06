using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using Swimmer.Library.Common;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Level_24 : MonoBehaviour, IClickItem
{
    [SerializeField] private TextMeshProUGUI questionSun;
    private SpriteRenderer _sunSpriteRenderer;

    void Start()
    {
        _sunSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void SunEvent()
    {
        _sunSpriteRenderer.GetComponent<CircleCollider2D>().enabled = false;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_sunSpriteRenderer.DOFade(1, 1.5f).SetEase(Ease.Linear));
        sequence.Append(_sunSpriteRenderer.transform.DOJump(new Vector3(-0.675f, -1.46f, 0), 0.2f, 4, 1.6f)
            .SetEase(Ease.Linear));
        sequence.InsertCallback(1.8f,
            () => Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1));
        sequence.InsertCallback(2.2f,
            () => Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1));
        sequence.InsertCallback(2.6f,
            () => Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1));
        sequence.InsertCallback(3f,
            () => Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1));
        sequence.Append(questionSun.DOFade(0, 1.2f).SetEase(Ease.Linear));
        sequence.Join(_sunSpriteRenderer.transform.DOJump(new Vector3(4, -1, 0), 1.5f, 1, 0.7f).SetEase(Ease.Linear)
            .OnComplete((() => Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL))));
    }

    public void tapOnItem()
    {
        SunEvent();
    }
}