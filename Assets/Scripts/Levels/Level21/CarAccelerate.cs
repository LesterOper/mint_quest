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
using UnityEngine.EventSystems;

public class CarAccelerate : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _branch;
    [SerializeField] private GameObject _car;

    private BoxCollider2D _carCollider;
    
    private float minimumAcceleration = 0.75f;
    private bool _completeBranch = false;
    private bool _carEngine = false;
    private bool _levelComplete = false;

    private void Start()
    {
        _carCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        var acceletateX = Input.acceleration.x;

        if (acceletateX > minimumAcceleration && !_completeBranch)
        {
            _completeBranch = true;
            _branch.transform.DORotate(new Vector3(0, 0, -90), 1f).SetEase(Ease.InQuint).OnComplete(() =>
            {
                Context.SoundPlayer.PlaySoundClick("ts3");
                _carCollider.enabled = true;
            });
        }
        
        if (acceletateX > minimumAcceleration && _carEngine && !_levelComplete)
        {
            _levelComplete = true;
            //Context.SoundPlayer.PlaySoundClick("car_engine", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
            _car.transform.DOMoveX(7, 1.5f).SetEase(Ease.InQuint)
                .OnComplete((() => Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL)));
        }
    }
    

    public void OnPointerClick(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("car_start", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
        _car.transform.DOShakePosition(0.5f, new Vector3(0.1f,0.1f,0), 6, 20f).OnComplete((() =>
        {
            _carEngine = true;
            _carCollider.enabled = false;
            
            _car.transform.DOMoveY(-1.4f, 0.1f).SetEase(Ease.InSine).SetLoops(-1, LoopType.Yoyo);
        }));
    }
}
