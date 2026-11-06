using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using TMPro;
using UnityEngine;

public class ToyHelper : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counter;
    [SerializeField] private TextMeshProUGUI text;
    private AudioSource sound;
    private int count = 0;

    private void Start()
    {
        sound = Context.SoundPlayer.PlaySound("new_year_bells_1", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
        counter.text = count.ToString();
        Camera.main.orthographic = false;
    }

    public void IncrementToys()
    {
        count++;
        counter.text = count.ToString();
        if (count == 7)
        {
            text.DOFade(0, 0.15f).SetEase(Ease.Linear);
            counter.DOFade(0, 0.15f).SetEase(Ease.Linear);
            Context.SoundPlayer.StopPlayingSounds(sound);
            Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
        }
    }
}
