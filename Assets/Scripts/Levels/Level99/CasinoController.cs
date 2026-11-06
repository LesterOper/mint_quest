using System;
using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class CasinoController : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject parent;
    [SerializeField] private Rigidbody2D ball;
    private bool isCorrectDir;
    public bool isStatic = false;
    private Vector2 movement;
    private Vector2 deltaStart;
    private float xAngle;
    private bool isPlaying;

    private void Start()
    {
        xAngle = transform.rotation.x;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var delta = Camera.main.ScreenToWorldPoint(eventData.delta);
        if (Vector2.Distance(deltaStart, delta) > 5f)
        {
            if (Math.Abs(delta.x) < Math.Abs(delta.y))
            {
                if (delta.y < 0)
                    transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.Euler(new Vector3(180f, 0, 0)), 4 * Time.deltaTime);
                if (transform.rotation.x * 100f < -90f && !isStatic)
                {
                    if (!isPlaying)
                    {
                        isPlaying = true;
                        //Context.SoundPlayer.PlaySoundClick("dzin_4", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
                    }

                    Invoke(nameof(DynamicBall), 0.2f);
                }
            }
        }
    }
    
    private void DynamicBall()
    {
        ball.bodyType = RigidbodyType2D.Dynamic;
        isStatic = true;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        deltaStart = Camera.main.ScreenToWorldPoint(eventData.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
