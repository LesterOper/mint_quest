using System;
using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class FreakEyesController : MonoBehaviour, IDraggableItems
{
    [SerializeField] private FreakHelper _helper;
    private DragController _controller;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        _controller = GetComponent<DragController>();
    }

    public void moveItem(Vector2 pos)
    {
        Context.SoundPlayer.PlaySoundClick("right_do", AudioMixerController.MasterMixerGroup.NONE, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
        transform.DOMove(pos, 0.25f).SetEase(Ease.Linear);
        _helper.Count();
        _controller.enabled = false;
    }

    public void attachToParentObject()
    { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name != "eyeLeftEndPosition" && other.name != "eyeRightEndPosition")
        {
            _controller.enabled = false;
            transform.DOMove(startPos, 0.15f).SetEase(Ease.Linear).OnComplete(() => { _controller.enabled = true; });
        }
    }
}
