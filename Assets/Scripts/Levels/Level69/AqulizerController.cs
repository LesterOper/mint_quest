using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;

public class AqulizerController : MonoBehaviour, IDraggableItems
{
    [SerializeField] private AqualizerHelper _helper;
    public bool isCorrectPos;
    public void moveItem(Vector2 pos)
    {
        Context.SoundPlayer.PlaySoundClick("right_do", AudioMixerController.MasterMixerGroup.SFX, 1);
        transform.DOMove(pos, 0.15f).SetEase(Ease.Linear);
        isCorrectPos = true;
        _helper.CheckCount();
    }

    public void attachToParentObject()
    {
        
    }
}
