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
using VoxelBusters.Utility;

public class Level_25 : MonoBehaviour, IDraggableItems
{
    
    [SerializeField] private Transform _circle;
    [SerializeField] private Transform _number;
    [Space] [SerializeField] private SpriteRenderer _spriteRenderer;
    private bool isSixConnected = false;
    private bool isFourConnected = false;
    private bool isComplete;
    public static bool isCoolIsInvoked;

    public int count = 0;

    private void Animation()
    {
        _circle.GetComponent<CircleCollider2D>().enabled = false;
        _number.SetParent(_circle);
        _spriteRenderer.DOFade(1, 0.1f);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_circle.DOJump(_circle.transform.position + Vector3.left * 5, 0.5f, 4, 2.5f)
            .SetEase(Ease.OutSine).OnComplete(() => {  }));
    }

    private void CheckCount()
    {
        Debug.Log("COUNT:    " + count);
        isCoolIsInvoked = true;
        isComplete = !isComplete;
        Debug.Log("MINTTTTTTTTT");
        FacebookAnalyticsHelper.SendEventLevelPassed(5);
        Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
    }

    public void moveItem(Vector2 pos)
    {
        Context.SoundPlayer.PlaySoundClick("right_do");
        count++;
        transform.DOMove(pos, 0.15f).SetEase(Ease.Linear).OnComplete(Animation);
        
        Debug.Log("COUNT: " + count);
        GetComponent<DragController>().enabled = false;
        var obj = FindObjectsOfType<Level_25>();
        var ct = obj[0].count + obj[1].count;
        if (ct == 2)
        {
            Context.SoundPlayer.StopPlayingSounds("ts3");
            CheckCount();
        }
    }

    public void attachToParentObject()
    {
    }
}