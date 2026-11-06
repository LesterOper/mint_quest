using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;

public class GearController : MonoBehaviour, IDraggableItems
{
    [SerializeField] private GearHelper _helper;
    public void moveItem(Vector2 pos)
    {
        transform.DOMove(pos, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.DOMoveZ(-0.3f, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
            {
                Context.SoundPlayer.PlaySoundClick("right_do", AudioMixerController.MasterMixerGroup.SFX, 1);
                transform.DOMoveZ(0, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    _helper.CheckDraggedGear();
                });
            });
        });
    }

    public void attachToParentObject()
    {
        throw new System.NotImplementedException();
    }
}
