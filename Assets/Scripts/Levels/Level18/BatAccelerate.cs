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
using VoxelBusters.Utility;

public class BatAccelerate : MonoBehaviour, IClickItem
{
    [SerializeField] private GameObject _batWake;
    [SerializeField] private GameObject _batSleep;
    [SerializeField] private Animation _animation;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] public float speed = 10.0f;
    [Space]
    [SerializeField] private float shakeDetectionTreshol = 2.6f;
    [SerializeField] private float minShakeInterval = 0.2f;
    
    private bool _canRotate = true;
    private bool _isAnimate = false;
    private bool _isComplete = false;
    
    private float sqrShakeDetectionTreshol; // for acceleration
    private float timeSinceLastShake;
    private int _batCount = 0;


    private void Start()
    {
        //For acceleration
        sqrShakeDetectionTreshol = Mathf.Pow(shakeDetectionTreshol, 2);
    }
    
    void Update()
    {
        AccelerationAnalyze();
    }
    
    
    private void AccelerationAnalyze()
    {
        if (Input.acceleration.sqrMagnitude >= sqrShakeDetectionTreshol &&
            Time.unscaledTime >= timeSinceLastShake + minShakeInterval && _canRotate)
        {
            if (_batCount > 4)
            {
                AnimationBat();
            }
            else
            {
                timeSinceLastShake = Time.unscaledTime;
                _batCount++;

                _canRotate = false;
                RotateBat();
            }
        }
    }

    private void RotateBat()
    {
        Sequence rotate = DOTween.Sequence();

        rotate.Append(_batSleep.transform.DORotate(new Vector3(0, 0, -17), 0.2f).SetEase(Ease.OutQuad));
        rotate.Append(_batSleep.transform.DORotate(new Vector3(0, 0, 12), 0.2f).SetEase(Ease.OutQuad));
        rotate.Append(_batSleep.transform.DORotate(new Vector3(0, 0, -9), 0.2f).SetEase(Ease.OutQuad));
        rotate.Append(_batSleep.transform.DORotate(new Vector3(0, 0, 6), 0.2f).SetEase(Ease.OutQuad));
        rotate.Append(_batSleep.transform.DORotate(new Vector3(0, 0, -3f), 0.2f).SetEase(Ease.OutQuad));
        rotate.Append(_batSleep.transform.DORotate(new Vector3(0, 0, 0f), 0.2f).SetEase(Ease.OutQuad));

        rotate.OnComplete((() => _canRotate = true));
    }

    
    private void AnimationBat()
    {
        Sequence anim = DOTween.Sequence();
        
        anim.AppendCallback((() =>
        {
            _batSleep.SetActive(false);
            _batWake.SetActive(true);
        }));
        anim.AppendCallback(() =>
        {
            Context.SoundPlayer.PlaySoundClick("bat_2", AudioMixerController.MasterMixerGroup.SFX, 1);
            _animation.Play("bat_fly");
        });
        anim.Append(_text.GetComponent<RectTransform>().DOAnchorPosX(-400, 0.4f));

        anim.InsertCallback(1.8f,() => SendCompleteMessage());
    }

    private void SendCompleteMessage()
    {
        Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
    }

    public void tapOnItem()
    {
        RotateBat();
    }
}
