using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using UnityEngine;

public class BallInMaze : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private BoxCollider2D[] _walls;
    [SerializeField] CircleCollider2D[] _circles;
    [SerializeField] private EdgeCollider2D edge;
    [SerializeField] private EdgeCollider2D edgeSecond;
    private Vector3 startPos;
    private float maxX, minX, maxY, minY;
    private bool isEnable = true;
    
    
    private Camera main;
    private Rigidbody2D rb;
    private float dirX;
    private float dirY;
    private float _moveSpeed = 6.5f;

    private bool _Complete = false;

    void Start()
    {
        startPos = transform.position;
        main = Camera.main;
        maxX = Screen.width;
        minX = -Screen.width;
        maxY = Screen.height;
        minY = -Screen.height;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        dirY = Input.acceleration.y * _moveSpeed;
        dirX = Input.acceleration.x * _moveSpeed;
    }

    private void FixedUpdate()
    {
        if (!isEnable) return;
        rb.velocity = new Vector2(dirX, dirY);
        var area = main.ScreenToWorldPoint(new Vector3(maxX, maxY, 0));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -area.x + 0.2f,area.x-0.2f), 
            Mathf.Clamp(transform.position.y, -area.y + 0.2f, area.y-0.2f), 0);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "BlackHole")
        {
            isEnable = false;
            transform.DOMove(other.transform.position, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
            {
                transform.DOScale(0, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    transform.DOMove(startPos, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
                        {
                            transform.DOScale(0.5f, 0.15f).SetEase(Ease.Linear).OnComplete(() => { isEnable = true; });
                        });
                });
            });
        }
        else
        {
            rb.simulated = false;
            GetComponent<CircleCollider2D>().enabled = false;
            edgeSecond.enabled = false;
            edge.enabled = false;

            Sequence anim = DOTween.Sequence();

            anim.Append(transform.DOJump(_target.position, 5f, 1, 0.8f).SetEase(Ease.InSine));
            anim.Append(transform.DOScale(Vector3.zero, 0.8f).SetEase(Ease.InSine));

            anim.OnComplete((() =>
            {
                foreach (var wall in _walls)
                {
                    wall.enabled = false;
                }

                foreach (var circle in _circles)
                {
                    circle.enabled = false;
                }

                Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
            }));
        }
    }
}
