using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class CirclesMover : MonoBehaviour, IDraggableItems
{
    [SerializeField] private Level_27 controller;
    [SerializeField] private GameObject endPos;
    public void moveItem(Vector2 pos)
    {
        Context.SoundPlayer.PlaySoundClick("right_do", AudioMixerController.MasterMixerGroup.SFX, 1);
        transform.DOMove(endPos.transform.position, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
        {
            controller.CheckCircles(gameObject);
        });
    }

    public void attachToParentObject()
    {
        
    }
}
