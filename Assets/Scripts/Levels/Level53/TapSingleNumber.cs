using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using UnityEngine;

public class TapSingleNumber : MonoBehaviour, IClickItem
{
    [SerializeField] private PolygonCollider2D coll;
    [SerializeField] private OrderTapNumberHelper _helper;
    public void tapOnItem()
    {
        transform.DOScale(0.4f, 0.075f).SetEase(Ease.Linear).OnComplete(() =>
        {
            _helper.NumberTapped(coll);
            transform.DOScale(0.55f, 0.075f).SetEase(Ease.Linear);
        });

    }
}
