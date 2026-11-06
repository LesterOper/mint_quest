﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using UnityEngine;

public class ZoomTriangle : MonoBehaviour, IZoom
{
    [SerializeField] private DragItem _drag;
    public bool isZoom;
    private void Start()
    {
        //_drag.enabled = false;
    }


    public void Scale(float amountToZoom)
    {
        transform.DOScale(new Vector3(amountToZoom, amountToZoom, amountToZoom), 0.3f)
            .SetEase(Ease.InOutSine)
            .OnComplete((() =>
            {
                isZoom = true;
                Invoke(nameof(InvokeMethod), 0.3f);
                GetComponent<ZoomController>().enabled = false;
            }));
    }

    private void InvokeMethod()
    {
        _drag.enabled = true;
    }
}