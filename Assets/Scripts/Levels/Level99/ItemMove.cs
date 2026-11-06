using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.XR.WSA;

public class ItemMove : MonoBehaviour, IDraggableItems
{
    [SerializeField] private GameObject mint;
    [SerializeField] private DragController _controller;

    public void moveItem(Vector2 pos)
    {
        transform.DOMove(pos, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
        {
            mint.transform.DOScale(new Vector3(7, 7, 7), 0.3f).SetEase(Ease.Linear).OnComplete(() =>
            {
                Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
            });
        });
    }

    public void attachToParentObject()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Context.SoundPlayer.PlaySoundClick("ts3");
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        GetComponent<CircleCollider2D>().isTrigger = true;
        _controller.SetStartPos();
    }
}