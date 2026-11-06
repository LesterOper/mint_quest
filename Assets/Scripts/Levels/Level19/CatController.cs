using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using Swimmer.Library.Common;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CatController : MonoBehaviour, IDragHandler, IPointerDownHandler, IBeginDragHandler
{
    [SerializeField] private Animation _animation;
    [SerializeField] private GameObject feedTheCat;
    private bool isPlaying;
    private Vector3 mOffset;
    private float mZCoord;
    private Vector3 vect;

    private void Start()
    {
        var width = Screen.width;
        var height = Screen.height;
        vect = Camera.main.ScreenToWorldPoint(new Vector3(width, height, 0));
    }

    private void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -vect.x, vect.x + 0.3f), transform.position.y, 0);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector3(GetMouseWorldPos().x + mOffset.x, transform.position.y, 0);
        if (transform.position.x <= 0.6f)
        {
            enabled = false;
            transform.position = new Vector3(0.6f, transform.position.y);
            GetComponent<BoxCollider2D>().enabled = false;
            _animation.Play();
            //feedTheCat.SetActive(false);
            if (!isPlaying)
            {
                isPlaying = true;
                Context.SoundPlayer.PlaySoundClick("cat_new_3", AudioMixerController.MasterMixerGroup.NONE, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
            }

            Invoke(nameof(SpecialEffect), 1.2f);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }
    
    private Vector3 GetMouseWorldPos()
    {
        //x,y
        Vector3 mousePoint = Input.mousePosition;
        //z
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void SpecialEffect()
    {
        
        Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
    }
}
