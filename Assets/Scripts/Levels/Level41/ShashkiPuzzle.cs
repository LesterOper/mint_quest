using System;
using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;

public class ShashkiPuzzle : MonoBehaviour, IDraggableItems
{
    [SerializeField] private ShashkiHelper _helper;
    [SerializeField] private SpriteRenderer fillObj;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    public void moveItem(Vector2 pos)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        transform.DOMove(pos, 0.15f).SetEase(Ease.Linear);
        _helper.PutShashki(this);
    }

    public void attachToParentObject()
    {
        
    }

    public void ResetObj()
    {
       // fillObj.DOFade(0, 0.15f).SetEase(Ease.Linear);
        transform.DOMove(startPos, 0.15f).SetEase(Ease.Linear);
    }

    public void FillSquere(SpriteRenderer fillObj)
    {
        fillObj.DOFade(1, 0.2f).SetEase(Ease.Linear);
    }
}
