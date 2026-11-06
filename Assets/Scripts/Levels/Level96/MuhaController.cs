using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using UnityEngine;
using Random = UnityEngine.Random;


public class MuhaController : MonoBehaviour
{
    private float rotZ;
    private Sequence sequence;
    private float timeToStep = 1f;
    private int index = 0;
    private int SCREEN_WIDTH, SCREEN_HEIGHT;
    private Vector3 currentDir = Vector2.up;
    private Quaternion quot;
    private bool isEnable = true;

    public bool IsEnable
    {
        get => isEnable;
        set => isEnable = value;
    }

    private void Start()
    {
        SCREEN_WIDTH = Screen.width;
        SCREEN_HEIGHT = Screen.height;
    }

    private void Update()
    {
        if (!isEnable) return;
        float posX;
        float posY;
        Vector3 position;
        timeToStep -= Time.deltaTime;
        if (timeToStep <= 0)
        {
            float randToCenter = Random.Range(0, 100);
            if (randToCenter > 80f)
            {
                posX = 0;
                posY = 0;
                position = new Vector3(posX, posY, 0);
            }
            else
            {
                posX = Random.Range(0, SCREEN_WIDTH);
                posY = Random.Range(0, SCREEN_HEIGHT);
                position = Camera.main.ScreenToWorldPoint(new Vector3(posX, posY, 0));
            }

            
            transform.DOMove(new Vector3(position.x, position.y, 0), 0.4f).SetEase(Ease.Linear);
            
            float angle = Vector2.SignedAngle(transform.up, position - transform.position);
            if (Math.Abs(angle) >= 1e-3f)
            {
                var angles = transform.eulerAngles;
                angles.z += angle;
                transform.eulerAngles = angles;
            }
            timeToStep = 0.6f;
        }
        
    }

    public void StopSequance()
    {
        sequence.Pause();
    }
}
