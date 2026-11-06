using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class AcselerateBalls : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject prefab, completePrefab;
    private GameObject current;
    private float accelerometrUpdateInterval = 1 / 60;
    private float lowPassKernel = 1;
    private float shakeDetection = 1.5f;
    private float lowPassFilterFact;
    private Vector3 lowPassValue = Vector3.zero;
    private Vector3 deltaAcc;
    private Vector3 acc;
    private int count = 0;
    private float timeToNextShake = 0.75f;

    private bool isActiveComplete = false;

    private void Start()
    {
        shakeDetection *= shakeDetection;
        lowPassFilterFact = accelerometrUpdateInterval / lowPassKernel;
        lowPassValue = Input.acceleration;
    }

    private void Update()
    {
        if(isActiveComplete) return;
        Debug.Log(isActiveComplete);
        acc = Input.acceleration;
        timeToNextShake -= Time.deltaTime;
        if (Mathf.Abs(acc.y) > Mathf.Abs(acc.x) && timeToNextShake <= 0)
        {
            lowPassValue = Vector3.Lerp(lowPassValue, acc, lowPassFilterFact);
            deltaAcc = acc - lowPassValue;
            if (deltaAcc.sqrMagnitude >= shakeDetection)
            {
                if (Mathf.Abs(deltaAcc.y) > Mathf.Abs(deltaAcc.x))
                {
                    if (count == 0)
                    {
                        current = Instantiate(prefab, parent.transform);
                        Context.SoundPlayer.PlaySoundClick("right_do");
                        count++;
                    }
                    else if (count == 1)
                    {
                        current.transform.DORotate(new Vector3( 0,0,180f), 0.15f).SetEase(Ease.Linear);
                        count++;
                        Context.SoundPlayer.PlaySoundClick("right_do");
                    }
                    else if (count == 2)
                    {
                        current = Instantiate(completePrefab, parent.transform);
                        isActiveComplete = true;
                        Context.SoundPlayer.PlaySoundClick("right_do");
                    }
                }
            }

            timeToNextShake = 1f;
        }
    }
}
