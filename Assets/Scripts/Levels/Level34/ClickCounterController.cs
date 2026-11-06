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

public class ClickCounterController : MonoBehaviour, IClickItem
{
    [SerializeField] private TextMeshProUGUI mainCounter;
    public int counter = 10;

    private void Start()
    {
        counter = 1000;
        mainCounter.text = counter.ToString();
    }

    public void tapOnItem()
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        counter--;
        if (counter == -100)
        {
            InvokeCompleteLevelEffect();
        }
        else
        {
            mainCounter.text = counter.ToString();
        }
    }

    private void InvokeCompleteLevelEffect()
    {
        mainCounter.gameObject.SetActive(false);
        Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
    }
}
