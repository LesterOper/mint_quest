using System;
using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels.Level23;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SwipeScreen : MonoBehaviour, IBeginDragHandler, IPointerDownHandler, IDragHandler
{
    private GameObject camera;
    private GameObject background;
    private GameObject detectorTouchScreen;
    public SwipeScreenEnum _screenEnum;
    private Vector3 mOffset;
    private float mZCoord;
    [SerializeField] private float minX, maxX, minY, maxY;

    private delegate void Move(PointerEventData eventData);

    private Move _move;

    public int index = 0;

    private void Start()
    {
        camera = GameObject.FindWithTag("MainCamera");
        background = GameObject.FindWithTag("Background");
        detectorTouchScreen = GameObject.Find("detector");
    }

    private void Update()
    {
        camera.transform.position = new Vector3(Mathf.Clamp(camera.transform.position.x, minX, maxX), Mathf.Clamp(camera.transform.position.y, minY, maxY), camera.transform.position.z);
        background.transform.position = new Vector3(Mathf.Clamp(background.transform.position.x, minX, maxX), Mathf.Clamp(background.transform.position.y, minY, maxY), background.transform.position.z);
        detectorTouchScreen.transform.position = new Vector3(Mathf.Clamp(detectorTouchScreen.transform.position.x, minX, maxX), Mathf.Clamp(detectorTouchScreen.transform.position.y, minY, maxY), detectorTouchScreen.transform.position.z);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    public void OnDrag(PointerEventData eventData)
    {
        var pos = eventData.delta;
        camera.transform.position -= new Vector3(pos.x / 275f, pos.y / 275f, 0);
        background.transform.position -= new Vector3(pos.x / 275f, pos.y / 275f, 0);
        detectorTouchScreen.transform.position -= new Vector3(pos.x / 275f, pos.y / 275f, 0);
    }

    private Vector3 GetMouseWorldPos()
    {
        //x,y
        Vector3 mousePoint = Input.mousePosition;
        //z
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}