using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using UnityEngine;

public class ChainElectrisity : MonoBehaviour, IClickItem
{
    [SerializeField] private ElectricityHelper _helper;
    private bool isRotateEnable = true;
    public void tapOnItem()
    {
        if (isRotateEnable)
        {
            transform.DORotate(new Vector3(0, 0, transform.eulerAngles.z + 90f), 0.25f).SetEase(Ease.Linear).OnComplete(
                () =>
                {
                    isRotateEnable = true;
                    Invoke(nameof(Invoke), 0.3f);
                });
            isRotateEnable = false;
        }
    }

    private void Invoke()
    {
        _helper.CheckTriggers();
    }
}
