using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class AmuletBallController : MonoBehaviour 
{
    [SerializeField] private EdgeCollider2D edge;
    [SerializeField] private CircleCollider2D ball;
    [SerializeField] private Transform target;
    private BoxCollider2D boxForTouch;
    private Vector3 startBallPos;
    private Vector3 scale = new Vector3(0.4f, 0.4f, 0.4f);

    private Vector2 startPos;

    private bool isEnable = true;

    private void Awake()
    {
        startBallPos = ball.transform.localPosition;
        scale = ball.gameObject.transform.localScale;
        boxForTouch = GetComponent<BoxCollider2D>();
    }
    
    private void Update()
    {
        if(!isEnable)
            isEnable = Input.touchCount < 1;
        if (!isEnable) return;
        if (Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = Camera.main.ScreenToWorldPoint(touch.position);
                    Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
                    break;
                case TouchPhase.Moved:
                    var movedPos = Camera.main.ScreenToWorldPoint(touch.position);
                    var x = movedPos.x - startPos.x;
                    var y = movedPos.y - startPos.y;
                    ball.transform.localPosition += new Vector3(x, y);
                    startPos = movedPos;
                    break;
                case TouchPhase.Ended:
                    ResetBall();
                    break;
            }
        }
    } 

    public void moveItem(Vector2 pos)
    {
        isEnable = false;
        ball.transform.DOLocalMove(pos, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
        {
            boxForTouch.enabled = false;
            edge.enabled = false;
            ball.enabled = false;
            Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
        });
    }

    public void ResetBall()
    {
        isEnable = false;
        boxForTouch.enabled = false;
        ball.transform.DOScale(0, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
        {
            ball.transform.DOLocalMove(startBallPos, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
            {
                ball.transform.DOScale(scale, 0.15f).SetEase(Ease.Linear);
                Invoke(nameof(InvokeActivateDrag), 0.15f);
            });
        });
    }

    private void InvokeActivateDrag()
    {
        //isEnable = true;
        boxForTouch.enabled = true;
    }
}
