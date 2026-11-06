using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using UnityEngine;

public class ClickCode : MonoBehaviour, IClickItem
{
    [SerializeField] private BlurNumberLevel _blurNumber;
    [SerializeField] private GameObject number;
    [SerializeField] private SpriteRenderer sprite;

    public void tapOnItem()
    {
        sprite.DOFade(0.3f, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
            {
                sprite.DOFade(1, 0.15f).SetEase(Ease.Linear);
            });
        _blurNumber.AddToCode(Convert.ToInt32(number.name));
    }
}
