using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class DogFeedHelper : MonoBehaviour
{
    [SerializeField] private GameObject candyPrefab;
    [SerializeField] private GameObject mask;
    [SerializeField] private GameObject target;
    private DragController _dragController;
    private GameObject currentCandy;
    private int count = 0;

    private void Start()
    {
        CreateCandy();
    }

    private void CreateCandy()
    {
        currentCandy = Instantiate(candyPrefab);
        currentCandy.transform.SetParent(transform);
        currentCandy.GetComponent<SpriteRenderer>().DOFade(1, 0.2f).SetEase(Ease.Linear);
        _dragController = currentCandy.GetComponent<DragController>();
        _dragController.SetTarget(target);
    }

    public void IncrementCandy()
    {
        count++;
        if (count == 5)
        {
            Destroy(currentCandy);
            mask.transform.DOMoveY(-8f, 0.85f).SetEase(Ease.Linear).OnComplete(() =>
            {
                Context.SoundPlayer.StopPlayingSounds();
                Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
            });
        }
        else
        {
            currentCandy.GetComponent<SpriteRenderer>().DOFade(0, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(currentCandy);
                CreateCandy();
            });

        }
    }
}
