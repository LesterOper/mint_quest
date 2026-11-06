using System;
using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;

public class CandyFeedDog : MonoBehaviour, IDraggableItems
{
    private DogFeedHelper _helper;

    private void Start()
    {
        _helper = FindObjectOfType<DogFeedHelper>();
    }

    public void moveItem(Vector2 pos)
    {
        Context.SoundPlayer.PlaySoundClick("right_do", AudioMixerController.MasterMixerGroup.SFX, 1);
        transform.DOMove(pos, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
        {
            Context.SoundPlayer.PlaySoundClick("dog_eat", AudioMixerController.MasterMixerGroup.SFX, 1);
            _helper.IncrementCandy();
        });
    }

    public void attachToParentObject()
    {
        
    }
}
