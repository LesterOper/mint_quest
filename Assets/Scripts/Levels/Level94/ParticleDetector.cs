using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class ParticleDetector : MonoBehaviour
{
    [SerializeField] private GameObject mask;
    [SerializeField] private GameObject toilet;
    [SerializeField] private Collider2D bachok, side;
    private void OnParticleCollision(GameObject obj)
    {
        if (obj.name == "Collision")
        {
            //Context.SoundPlayer.PlaySoundClick("water_drop_2", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
            Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.NONE, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
        }
        else if (obj.name == "Collision (1)")
        {
            //Context.SoundPlayer.PlaySoundClick("water_drop_2", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
            Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.NONE, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
        }
        else
        {
            if (obj.name == toilet.name)
            {
                //Context.SoundPlayer.PlaySoundClick("water_drop_2", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
                Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.NONE, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
                Debug.Log("DETECT");
                mask.transform.DOLocalMoveY(mask.transform.localPosition.y + 0.1f, 0.2f).SetEase(Ease.Linear);
                if (mask.transform.localPosition.y >= -1.6f)
                {
                    toilet.GetComponent<BoxCollider2D>().enabled = false;
                    bachok.enabled = side.enabled = false;
                    Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
                    
                }
            }
        }
    }
}
