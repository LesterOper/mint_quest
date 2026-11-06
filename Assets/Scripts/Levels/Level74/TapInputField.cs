using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using UnityEngine;

public class TapInputField : MonoBehaviour, IClickItem
{
    [SerializeField] private RectTransform numbers;
    [SerializeField] private CodeChecker _checker;
    [SerializeField] private KeyboardAnim anim;
    public void tapOnItem()
    {
        //numbers.DOAnchorPosY(-460f, 0.3f).SetEase(Ease.Linear);
        anim.OnAnim();
        _checker.SetCurrentField(gameObject);
    }
}
