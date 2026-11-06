using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using TMPro;
using UnityEngine;

public class AccelerationControllerTap : MonoBehaviour
{
    
    [SerializeField] private CircleCollider2D _circleCollider;
    [SerializeField] private RectTransform text;
    
    private float minimumAcceleration = 0.25f;
    private bool _canTap = false;
    
    

    void Update()
    {
        var acceletateY = Input.acceleration.y;

        if (acceletateY > minimumAcceleration && !_canTap)
        {
            text.DORotate(new Vector3(0f, 0f, 180f), 0f).SetEase(Ease.Linear);
            _canTap = true;
            _circleCollider.enabled = true;
        }
        else if (acceletateY < minimumAcceleration && _canTap)
        {
            text.DORotate(new Vector3(0f, 0f, 0f), 0f).SetEase(Ease.Linear);
            _canTap = false;
            _circleCollider.enabled = false;
        }
    }

    
}
