using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using Swimmer.Library;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class AccelearateMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Transform ballComplete;

    private Transform secondTarget;

    private float dirY;

    private float speed = 2;

    private float timeToMove;
    private float endY = -2.328f;
    private float startY;
    private bool isEnable;

    private void Awake()
    {
        secondTarget = GameObject.Find("third").transform;
        startY = transform.position.y;
        timeToMove = 0.5f;
        isEnable = true;
    }

    void Update()
    {
        if (!isEnable) return;
        Debug.Log(isEnable);
        timeToMove -= Time.deltaTime;
        if (timeToMove <= 0)
        {
            dirY = Input.acceleration.y * speed;

            if (Vector2.Distance(ballComplete.position, secondTarget.position) <= 0.4f)
            {
                isEnable = false;
                Effect();
            }
        }
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, startY, -2.328f));
    }

    private void FixedUpdate()
    {
        if(!isEnable) return;
        Debug.Log(isEnable);
        rb.velocity = new Vector2(0, dirY);
    }

    private void Effect()
    {
        Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
    }
}
