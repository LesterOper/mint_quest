using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using UnityEngine;

public class LockButtonController : MonoBehaviour, IClickItem
{
    [SerializeField] private SpriteRenderer blockScreen;
    [SerializeField] private GameObject target;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    public void tapOnItem()
    {
        transform.DOMove(target.transform.position, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                transform.DOMove(startPos, 0.2f).SetEase(Ease.Linear);
                blockScreen.DOFade(1, 0.25f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
                });
            });
        
    }
}
