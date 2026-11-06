using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class CodeLockController : MonoBehaviour
{
    [SerializeField] private List<TapCircle> correctOrder;
    private List<TapCircle> buff;
    private TapCircle current;

    private void Start()
    {
        buff = new List<TapCircle>(correctOrder);
        current = buff[0];
    }

    public void CheckOrder(TapCircle tapEl)
    {
        if (current == tapEl)
        {
            //Context.SoundPlayer.PlaySoundClick("pick_3", AudioMixerController.MasterMixerGroup.SFX, 1);
            Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
            tapEl.GetComponent<SpriteRenderer>().DOFade(0, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                tapEl.gameObject.SetActive(false);
            });
            buff.RemoveAt(0);
           

            if (buff.Count == 0)
            {
                Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
                return;
            }
            current = buff[0];
        }
        else
        {
            ResetButtons();
        }
    }

    public void ResetButtons()
    {
        correctOrder.ForEach(obj =>
        {
            obj.gameObject.SetActive(true);
            obj.GetComponent<SpriteRenderer>().DOFade(1, 0.1f).SetEase(Ease.Linear);
        });
        buff = new List<TapCircle>(correctOrder);
        current = buff[0];
    }
}
