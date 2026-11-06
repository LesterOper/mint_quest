using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using UnityEngine;

public class TaxiButtonsTapper : MonoBehaviour, IClickItem
{
    [SerializeField] private TaxiHelper _helper;
    [SerializeField] private GameObject text;
    private bool isClick = false;

    private Vector3 btnScale;
    private Vector3 textScale;

    private void Start()
    {
        btnScale = transform.localScale;
        textScale = text.transform.localScale;
    }

    public void tapOnItem()
    {
        if (!isClick)
        {
            isClick = true;
            transform.DOScale(btnScale / 1.2f, 0.15f).SetEase(Ease.Linear);
            text.transform.DOScale(textScale / 1.2f, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
            {
                transform.DOScale(btnScale, 0.15f).SetEase(Ease.Linear);
                text.transform.DOScale(textScale, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    isClick = false;
                    _helper.Checker(gameObject);
                });
            });
        }
    }
}
