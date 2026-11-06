using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using UnityEngine;

public class ToothClicker : MonoBehaviour, IClickItem
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private ToothHelper _helper;
    private Quaternion startRot;
    private Vector3 startPos;


    public bool isTop;

    private void Start()
    {
        startPos = transform.position;
        startRot = transform.localRotation;
    }

    public void tapOnItem()
    {
        if (!isTop)
        {
            transform.DOShakeRotation(0.3f, new Vector3(0, 0, 30)).SetEase(Ease.Linear).OnComplete(() =>
            {
                transform.DOMoveY(transform.position.y + 0.2f, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                        _helper.CheckCorrectTooth(gameObject);  
                    });
            });
            
        }
        else
        {
            transform.DOShakeRotation(0.3f, new Vector3(0, 0, 30)).SetEase(Ease.Linear).OnComplete(() =>
            {
                /*transform.DOMoveY(transform.position.y - 0.2f, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
                {*/
                _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                _helper.CheckCorrectTooth(gameObject);  
                //});
            });
            
        }
         
    }

    public void ResetTooth()
    {
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        transform.position = startPos;
    }
}
