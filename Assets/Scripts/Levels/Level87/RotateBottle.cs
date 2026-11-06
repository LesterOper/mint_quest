using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;

public class RotateBottle : MonoBehaviour, IClickItem
{
    [SerializeField] private GameObject bottle;
    [SerializeField] private SpriteRenderer thisRend, otherRend;
    [SerializeField] private Sprite activeOther, deactiveThis;
    [SerializeField] private CircleCollider2D _colliderThis, _colliderOther;
    public float angleY;

    private void Start()
    {
        
    }

    public void tapOnItem()
    {
        Context.SoundPlayer.PlaySoundClick("turn_move_1");
        bottle.transform.DORotate(new Vector3(0, angleY, 0), 0.25f).SetEase(Ease.Linear).OnComplete(() =>
            {
                thisRend.sprite = deactiveThis;
                otherRend.sprite = activeOther;
                _colliderThis.enabled = false;
                _colliderOther.enabled = true;
            });
    }

    public void Active()
    {
        otherRend.sprite = activeOther;
        _colliderOther.enabled = true;
        _colliderThis.enabled = false;
    }
}
