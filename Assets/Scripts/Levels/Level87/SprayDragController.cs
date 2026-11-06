using System;
using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class SprayDragController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private ParticleSystem _particleSystem;
    private AudioSource sound;
    private Vector3 mOffset;
    private float mZCoord;
    private float startY;
    private bool isPlaying = false;

    private void Start()
    {
        startY = transform.localPosition.y;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var delta = eventData.delta;
        if (Mathf.Abs(delta.x) < Mathf.Abs(delta.y))
        {
            if (delta.y < 0)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, 
                    Mathf.Clamp(GetMouseWorldPos().y + mOffset.y, 1.75f, startY));
                if (transform.localPosition.y <= 1.75f)
                {
                    if(!Context.SoundPlayer.IsTargetSourcePlaying(sound))
                        sound = Context.SoundPlayer.PlaySound("spray", AudioMixerController.MasterMixerGroup.NONE, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
                    _particleSystem.Play();
                   //Invoke(nameof(StopParticle), 0.5f);
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Context.SoundPlayer.StopPlayingSounds(sound);
        StopParticle();
    }
    
    private Vector3 GetMouseWorldPos()
    {
        //x,y
        Vector3 mousePoint = Input.mousePosition;
        //z
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }


    private void StopParticle()
    {
        _particleSystem.Stop();
        transform.DOLocalMoveY(startY, 0.15f).SetEase(Ease.Linear);
    }
}
