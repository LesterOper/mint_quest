using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using UnityEngine;

public class VolkanoLavaController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer mintBack;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ParticleSystem _system;
    private float dir = 0;
    private bool isUpper = false;
    private bool isWork = true;
    private void Update()
    {
        if (!isWork) return;
        dir = Input.acceleration.y * 2.5f;
        transform.position = new Vector3(transform.position.x,
            Mathf.Clamp(transform.position.y, -13.4f, 0), transform.position.z);

        if (transform.position.y >= -8.88f)
        {
            _system.gameObject.SetActive(true);
            isWork = !isWork;
            rb.velocity = new Vector2(0,0);
            mintBack.DOFade(1, 3.5f).SetEase(Ease.Linear).OnComplete(Invoke);
        }
    }

    private void FixedUpdate()
    {
        if (!isWork) return;
        rb.velocity = new Vector2(0, dir);
    }

    private void Invoke()
    {
        Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
    }
}