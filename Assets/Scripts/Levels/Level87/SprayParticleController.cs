using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using UnityEngine;

public class SprayParticleController : MonoBehaviour
{
    private int countFirst = 0, countSecond = 0;
    private int objectCount = 0;
    [SerializeField] private GameObject first, second;
    [SerializeField] private GameObject firstMask, secondMask;
    private bool isFirst, isSecond, isComplete;
    private float height;

    private void Start()
    {
        var vector = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        height = vector.y*2.8f;
    }

    private void OnParticleCollision(GameObject obj)
    {
        if (obj == first && !isFirst)
        {
            firstMask.transform.DOMoveY(firstMask.transform.position.y + 0.3f, 0.15f).SetEase(Ease.Linear);
            if (firstMask.transform.position.y >= height)
            {
                isFirst = true;
            }
        }

        if (obj == second && !isSecond)
        {
            secondMask.transform.DOMoveY(secondMask.transform.position.y + 0.3f, 0.15f).SetEase(Ease.Linear);
            if (secondMask.transform.position.y >= height)
            {
                isSecond = true;
            }
        }

        if (isFirst && isSecond)
        {
            if (!isComplete)
            {
                isComplete = true;
                Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
            }
        }
    }
}
