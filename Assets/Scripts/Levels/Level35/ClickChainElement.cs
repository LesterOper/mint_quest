using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickChainElement : MonoBehaviour, IClickItem, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private ChainController _controller;
    [SerializeField] private BoxCollider2D _collider2D;
    private Vector2 pointDown;
    private Vector2 pointUp;

    public void tapOnItem()
    {
        if (Vector2.Distance(pointDown, pointUp) < 0.2f)
        {
            if (_controller.IncrementCountAndCheckChain(gameObject))
            {
                transform.DOScale(new Vector3(0, 0, 0), 0.25f).SetEase(Ease.Linear);
                _collider2D.enabled = false;
            }
        }
        else 
            _controller.ResetChain();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        pointDown = Camera.main.ScreenToWorldPoint(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointUp = Camera.main.ScreenToWorldPoint(eventData.position);
        tapOnItem();
    }
}