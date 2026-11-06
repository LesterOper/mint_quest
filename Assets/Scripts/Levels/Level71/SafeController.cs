using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class SafeController : MonoBehaviour, IClickItem
{
    [SerializeField] private CircleCollider2D safe;
    [SerializeField] private GameObject firstButton, secondButton;
    [SerializeField] private Sprite deactiveFirst, activeSecond, deactiveSecond;
    [SerializeField] private SpriteRenderer iconFirst, iconSecond;
    private int count = 0;

    private void Start()
    {
       
        Vibration.Init();
    }

    public void Vibro()
    {
        count++;
        Vibration.Vibrate(20L);
        if (count == 1)
        {
            firstButton.GetComponent<CircleCollider2D>().enabled = false;
            var rotCount = firstButton.GetComponent<RotatorSafe>().NumberRotate;

            secondButton.GetComponent<CircleCollider2D>().enabled = true;
            secondButton.GetComponent<RotatorSafe>().NumberRotate = rotCount;
            iconFirst.sprite = deactiveFirst;
            iconSecond.sprite = activeSecond;
        }

        if (count == 2)
        {
            secondButton.GetComponent<CircleCollider2D>().enabled = false;
            iconSecond.sprite = deactiveSecond;
            safe.enabled = true;
            GetComponent<ClickController>().enabled = true;
        }
    }

    public void tapOnItem()
    {
        transform.DORotate(new Vector3(0, 0, 1080f), 0.4f, RotateMode.FastBeyond360).SetEase(Ease.Linear);
        Invoke(nameof(Complete), 0.2f);
    }

    private void Complete()
    {
        Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
    }
}
