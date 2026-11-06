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

public class HammerController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private List<SpriteRenderer> sphereTriggers;
    [SerializeField] private float rotZDown, rotZUp;
    public float timeForLongTap = 1f;
    private List<string> exTap = new List<string>() {"short", "long", "short"};
    public string currentExTap;
    private int indexOfExTap = 0;
    private int countTap = 0;
    private int countToFillCircle = 0;

    private void Start()
    {
        currentExTap = exTap[indexOfExTap];
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            timeForLongTap -= Time.deltaTime;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(currentExTap.Equals("short"))
            Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        else Context.SoundPlayer.PlaySound("ts3", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
        transform.DORotate(new Vector3(0, 0, rotZDown), 0.15f).SetEase(Ease.Linear);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (timeForLongTap <= 0)
        {
            if (currentExTap.Equals("long"))
            {
                //Context.SoundPlayer.PlaySound("ts3", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
                countTap++;
                if (countTap == 3)
                    IncrementOrderTaps();
            }
            else
            {
                ResetOrderTaps();
            }
            
        }
        else
        {
            if (currentExTap.Equals("short"))
            {
                //Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
                countTap++;
                if (countTap == 3)
                    IncrementOrderTaps();
            }
            else
            {
                
                ResetOrderTaps();
            }
        }
        
        Context.SoundPlayer.StopPlayingSounds();
        timeForLongTap = 0.5f;
        Invoke(nameof(InvokeRotate), 0.15f);
    }

    private void InvokeRotate()
    {
        transform.DORotate(new Vector3(0, 0, rotZUp), 0.07f).SetEase(Ease.Linear);
    }


    private void ResetOrderTaps()
    {
        countTap = 0;
        indexOfExTap = 0;
        currentExTap = exTap[indexOfExTap];
        countToFillCircle = 0;
        sphereTriggers.ForEach(obj => { obj.DOFade(0, 0.15f).SetEase(Ease.Linear); });
    }

    private void IncrementOrderTaps()
    {
        countTap = 0;
        indexOfExTap++;
        sphereTriggers[countToFillCircle].DOFade(1, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (indexOfExTap == exTap.Count)
            {
                Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
                return;
            }
            currentExTap = exTap[indexOfExTap];
        });
        countToFillCircle++;
        
        
    }
}
