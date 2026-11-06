using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level31 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMint;
    [SerializeField] private float shakeDetectionTreshol = 2.5f;
    [SerializeField] private float minShakeInterval = 0.1f;
    [SerializeField] private Leave[] _leaves;
    [Space]
    [SerializeField] private Vector3[] _wayPoints;

    private float sqrShakeDetectionTreshol; // for acceleration
    private float timeSinceLastShake; // for acceleration

    private int _leaveCount = 0;
    private bool isAll = false;


    private void OnEnable()
    {
        Messenger.AddListener(Messages.LEAVE_DOWN, NeedCheckLevelIsComplete);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(Messages.LEAVE_DOWN, NeedCheckLevelIsComplete);
    }

    private void Start()
    {
        //For acceleration
        sqrShakeDetectionTreshol = Mathf.Pow(shakeDetectionTreshol, 2);
    }

    private void Update()
    {
        AccelerationAnalyze();
    }

    private void AccelerationAnalyze()
    {
        if (isAll) return;
        if (Input.acceleration.sqrMagnitude >= sqrShakeDetectionTreshol &&
            Time.unscaledTime >= timeSinceLastShake + minShakeInterval && !isAll)
        {
            if (_leaveCount < _leaves.Length)
            {
                _leaves[_leaveCount].MoveDown();
                _leaveCount++;
                timeSinceLastShake = Time.unscaledTime;
            }
        }
    }
    
    private  void NeedCheckLevelIsComplete()
    {
        if (_leaveCount >= _leaves.Length)
        {
            isAll = true;
            Invoke(nameof(Complete), 1.5f);
        }
    }

    private void Complete()
    {
        if (isAll)
        {
            isAll = false;
            _textMint.DOFade(0, 0.1f);
            Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
        }
    }
}
