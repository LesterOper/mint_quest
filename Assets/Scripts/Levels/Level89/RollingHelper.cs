using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using UnityEngine;

public class RollingHelper : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private SpriteRenderer exit, ground, dopGround;
    private GameObject current;
    private int countTransportHumans = 0;
    public bool isFirst, isSecond;

    private void Start()
    {
        Camera.main.orthographic = false;
        CreateTrain();
    }

    public void CreateTrain()
    {
        if (countTransportHumans < 2)
        {
            Destroy(current);
            current = Instantiate(prefab);
        }
    }

    public void Increment(GameObject obj)
    {
        countTransportHumans++;
        if (countTransportHumans == 2)
        {
            if(current != null)
                Destroy(current);
            obj.SetActive(false);
            exit.DOFade(0, 0.15f).SetEase(Ease.Linear);
            dopGround.DOFade(0, 0.15f).SetEase(Ease.Linear);
            ground.DOFade(0, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
            {
                Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
            });

        }
        else
        {
            isFirst = obj.name == "person_1";
            isSecond = obj.name == "person_2";
            obj.SetActive(false);
        }
    }
}
