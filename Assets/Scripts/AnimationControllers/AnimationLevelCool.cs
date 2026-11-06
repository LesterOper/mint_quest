using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;
using Levels;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimationLevelCool : MonoBehaviour
{
    [SerializeField] private GameController _game;
    private GameObject coolBtn;

    public void SetupAnimCool()
    {
        coolBtn = GameObject.FindWithTag("coolBtn");
        var type = coolBtn.GetComponent<CoolClickedController>().type;
        switch (type)
        {
            case AnimCoolEnum.FIRST : 
                coolBtn.GetComponent<CircleCollider2D>().enabled = false;
                coolBtn.GetComponent<Rigidbody2D>().simulated = false;
                SetFirstVariantOfCoolAnim(); 
                break;
            case AnimCoolEnum.SECOND : 
                coolBtn.GetComponent<CircleCollider2D>().enabled = false;
                coolBtn.GetComponent<Rigidbody2D>().simulated = false;
                SetSecondVariantOfCoolAnim(); 
                break;
            case AnimCoolEnum.THIRD : 
                coolBtn.GetComponent<CircleCollider2D>().enabled = false;
                coolBtn.GetComponent<Rigidbody2D>().simulated = false;
                SetThirdVariantOfCoolAnim(); 
                break;
            case AnimCoolEnum.FOURTH : 
                coolBtn.GetComponent<CircleCollider2D>().enabled = false;
                coolBtn.GetComponent<Rigidbody2D>().simulated = false;
                SetFourthVariantOfCoolAnim();
                break;
        }
    }
    
    private void SetFirstVariantOfCoolAnim()
    {
        Context.SoundPlayer.PlaySoundClick("level_finish", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
        Sequence sequence = DOTween.Sequence();
        
        sequence.Append(coolBtn.transform.DOScale(new Vector3(0f, 0f, 0f), 0.35f).SetEase(Ease.Linear));
        sequence.OnComplete(() =>
        {
            Debug.Log("COOL");
            Messenger.Broadcast(Messages.COOL_EFFECT_LOAD_NEXT_LEVEL);
            Messenger.Broadcast(Messages.COOL_BUTTON_ON_SCENE_NEED_RESET);
        });
    }

    private void SetSecondVariantOfCoolAnim()
    {
        Vector3 endValue;
        float rand = Random.value;
        if (rand > 0.5)
        {
            endValue = new Vector3(6,transform.position.y, 0);
        }
        else
        {
            endValue = new Vector3(-6,transform.position.y, 0);
        }
        
        Context.SoundPlayer.PlaySoundClick("level_finish", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
        coolBtn.transform.DOJump(endValue, 2.2f, 1, 0.55f).OnComplete(
                () =>
                {
                    Debug.Log("COOL");
                    Messenger.Broadcast(Messages.COOL_EFFECT_LOAD_NEXT_LEVEL);
                    Messenger.Broadcast(Messages.COOL_BUTTON_ON_SCENE_NEED_RESET);
                });
        
    }
    
    private void SetThirdVariantOfCoolAnim()
    {
        Context.SoundPlayer.PlaySoundClick("level_finish", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
        coolBtn.transform.DOMoveY(-12, 0.4f).SetEase(Ease.InQuint).OnComplete(
                () =>
                {
                    Debug.Log("COOL");
                    Messenger.Broadcast(Messages.COOL_EFFECT_LOAD_NEXT_LEVEL);
                    Messenger.Broadcast(Messages.COOL_BUTTON_ON_SCENE_NEED_RESET);
                });
        
    }
    
    private void SetFourthVariantOfCoolAnim()
    {
        float rotate;
        Vector3 endValue;
        float rand = Random.value;
        if (rand > 0.5)
        {
            endValue = new Vector3(6,transform.position.y, 0);
            rotate = -360;
        }
        else
        {
            endValue = new Vector3(-6,transform.position.y, 0);
            rotate = 360;
        }
        Context.SoundPlayer.PlaySoundClick("level_finish", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
        Sequence animCircle = DOTween.Sequence();
        animCircle.Append(coolBtn.transform.DOMove(endValue, 0.4f).SetEase(Ease.InQuint)
            .OnComplete(() =>
            {
                Debug.Log("COOL");
                Messenger.Broadcast(Messages.COOL_EFFECT_LOAD_NEXT_LEVEL);
                Messenger.Broadcast(Messages.COOL_BUTTON_ON_SCENE_NEED_RESET);
            }));
        animCircle.Join(coolBtn.transform.DORotate(new Vector3(0, 0, rotate) * 3, 2.5f, RotateMode.FastBeyond360));
    }
}
