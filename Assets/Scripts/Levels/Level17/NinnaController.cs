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

public class NinnaController : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler
{
    [SerializeField] private GameObject detector;
    [SerializeField] private GameObject whereIsNina;
    [SerializeField] private float minX, maxX;
    private RectTransform ninaQuestion;
    private GameObject coolBtn;
    private GameObject camera;
    private GameObject background;

    private void Update()
    {
        camera.transform.position = new Vector3(Mathf.Clamp(camera.transform.position.x, minX, maxX), camera.transform.position.y, camera.transform.position.z);
        background.transform.position = new Vector3(Mathf.Clamp(background.transform.position.x, minX, maxX), background.transform.position.y, background.transform.position.z);
        detector.transform.position = new Vector3(Mathf.Clamp(detector.transform.position.x, minX, maxX), detector.transform.position.y, detector.transform.position.z);
    }

    private void Start()
    {
        camera = GameObject.FindWithTag("MainCamera");
        background = GameObject.FindWithTag("Background");
        ninaQuestion = whereIsNina.GetComponent<RectTransform>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        var pos = eventData.delta;
        if (Math.Abs(pos.x) > Math.Abs(pos.y))
        {
            camera.transform.position -= new Vector3(pos.x/275f, 0, 0); 
            background.transform.position -= new Vector3(pos.x/275f, 0, 0); 
            detector.transform.position -= new Vector3(pos.x/275f, 0, 0);
            ninaQuestion.anchoredPosition += new Vector2(pos.x, 0);
            
        }
        if (camera.transform.position.x >= 9.5f)
        {
            detector.SetActive(false);
            
            Invoke(nameof(Effect), 1f);
        }
    }

    private void Effect()
    {
        whereIsNina.SetActive(false);
        Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
    }
}