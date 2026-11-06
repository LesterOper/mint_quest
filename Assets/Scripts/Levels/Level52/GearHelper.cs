using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using UnityEngine;

public class GearHelper : MonoBehaviour
{
    [SerializeField] private List<GearController> dragGear;
    [SerializeField] private List<GameObject> allGear;
    [SerializeField] private Transform leftMint, rightMint;
    private int count = 0;

    private void Start()
    {
        Camera.main.orthographic = false;
    }

    public void CheckDraggedGear()
    {
        count++;
        if (count == 4)
        {
            Effect();
        }
    }

    private void Effect()
    {
        float angle;
        for (int i = 0; i < allGear.Count; i++)
        {
            if (i % 2 == 0)
            {
                angle = -720f;
            }
            else
            {
                angle = 720f;
            }
            allGear[i].transform.DORotate(new Vector3(0f, 0f, angle), 1.3f, RotateMode.FastBeyond360);
            allGear[i].GetComponent<CircleCollider2D>().enabled = false;
        }

        leftMint.DORotate(new Vector3(0f, 0f, 0f), 1.3f).SetEase(Ease.Linear);
        rightMint.DORotate(new Vector3(0f, 0f, 0f), 1.3f).SetEase(Ease.Linear).OnComplete(() =>
        {
            Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
        });
    }
}
