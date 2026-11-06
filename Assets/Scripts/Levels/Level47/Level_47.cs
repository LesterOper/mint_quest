using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using TMPro;
using UnityEngine;

public class Level_47 : MonoBehaviour
{
    [SerializeField] private GameObject _firstElement;
    [SerializeField] private Transform[] _ovalTransforms;
    
    private int _currentOval = 0;
    private float _currentAngle = -22.5f;
    private bool _canRotate = true;


    private void Update()
    {
        var AccelerateX = Input.acceleration.x;
        var AccelerateY = Input.acceleration.y;

        if (AccelerateX > 0.25f && AccelerateY > 0.25f && (_currentOval == 0 || _currentOval == 1 || _currentOval == 2 || _currentOval == 3) && _canRotate)
        {
            _canRotate = false;
            SetRotateOval(_currentOval);
        }
        else if (AccelerateX > 0.25f && AccelerateY < -0.25f && (_currentOval == 4 || _currentOval == 5 || _currentOval == 6 || _currentOval == 7) && _canRotate)
        {
            _canRotate = false;
            SetRotateOval(_currentOval);
        }
        else if (AccelerateX < -0.25f && AccelerateY < -0.25f && (_currentOval == 8 || _currentOval == 9 || _currentOval == 10 || _currentOval == 11) && _canRotate)
        {
            _canRotate = false;
            SetRotateOval(_currentOval);
        }
        else if (AccelerateX < -0.25f && AccelerateY > 0.25f && (_currentOval == 12 || _currentOval == 13 || _currentOval == 14) && _canRotate)
        {
            _canRotate = false;
            SetRotateOval(_currentOval);
        }
    }

    private void SetRotateOval(int index)
    {
        Vector3 endRotatePos = new Vector3(0,0,0 + _currentAngle);
        _ovalTransforms[index].DOLocalRotate(endRotatePos, 0.3f).SetEase(Ease.InSine).OnComplete((() =>
        {
            CheckComplete();
        }));
    }


    private void CheckComplete()
    {
        if (_currentOval == _ovalTransforms.Length - 1)
        {
            Sequence anim = DOTween.Sequence();
            
            anim.Append(_firstElement.transform.DORotate(new Vector3(0, 0, -360) * 3, 1f, RotateMode.FastBeyond360));
            anim.Join(_firstElement.transform.DOMoveX(5, 0.5f).SetEase(Ease.InSine));

            anim.OnComplete((() => Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL)));
        }
        else
        {
            _canRotate = true;
            _currentOval++;
        }
    }
}
