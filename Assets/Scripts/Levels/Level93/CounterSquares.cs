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

public class CounterSquares : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject number;
    [SerializeField] private TextMeshProUGUI text;
    private const int correctCount = 12;
    private int counter = 0;
    
    public void Count()
    {
        counter = Convert.ToInt32(text.text);

        if (counter == correctCount)
        {
            Invoke(nameof(Complete), 0.5f);
        }
        else
        {
            Context.SoundPlayer.PlaySoundClick("error", AudioMixerController.MasterMixerGroup.SFX, 1);

            ResetCounter();
        }
    }

    public void ResetCounter()
    {
        number.transform.DOShakePosition(0.3f, new Vector3(0.08f, 0.08f, 0.08f)).SetEase(Ease.Linear);
        text.text = "";
        counter = 0;
    }

    private void Complete()
    {
        canvas.SetActive(false);
        Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
    }
}
