using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using UnityEngine;

public class TapDayMonthField : MonoBehaviour, IClickItem
{
    [SerializeField] private RectTransform numbers;
    public void tapOnItem()
    {
        numbers.DOAnchorPosY(-868.34f, 0.25f).SetEase(Ease.Linear);
    }
}
