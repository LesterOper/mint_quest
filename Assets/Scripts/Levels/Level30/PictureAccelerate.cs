using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using UnityEngine;

public class PictureAccelerate : MonoBehaviour
{
    [SerializeField] private Animation _animation;
    [SerializeField] private float shakeDetectionTreshol = 2.5f;
    [SerializeField] private float minShakeInterval = 0.1f;
    
    private float sqrShakeDetectionTreshol; // for acceleration
    private float timeSinceLastShake;

    private bool _canRotate = true;
    
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
        if (Input.acceleration.sqrMagnitude >= sqrShakeDetectionTreshol &&
            Time.unscaledTime >= timeSinceLastShake + minShakeInterval && Input.acceleration.x < 0)
        {
            timeSinceLastShake = Time.unscaledTime;
            _canRotate = false;
            Rotate();
        }
    }    
    
    
    private void Rotate()
    {
        /*Sequence rotate = DOTween.Sequence();

        rotate.Append(transform.DORotate(new Vector3(0, 0, -15), 0.2f));
        rotate.Append(transform.DORotate(new Vector3(0, 0, 12), 0.2f));
        rotate.Append(transform.DORotate(new Vector3(0, 0, -6), 0.2f));
        rotate.Append(transform.DORotate(new Vector3(0, 0, 0), 0.2f));

        rotate.OnComplete((() => _canRotate = true));*/

        _animation.Play();
    }
   
   
    
}
