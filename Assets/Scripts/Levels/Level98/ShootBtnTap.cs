using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class ShootBtnTap : MonoBehaviour, IClickItem
{
    [SerializeField] private GameObject light;
    public void tapOnItem()
    {
        Context.SoundPlayer.PlaySoundClick("camera_2", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
        transform.DOLocalMoveY(1, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.DOLocalMoveY(1.15f, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
            {
                light.transform.DOScale(new Vector3(7, 7, 7), 0.2f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
                });
            });
        });
        
    }
}
