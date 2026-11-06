using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragScreen : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private GameObject detectorScreen;
    private GameObject camera;
    private GameObject background;

    public GameObject CAMERA => camera;
    public GameObject BACKGROUND => background;

    private Vector2 posDelta;
    private bool isDrag;

    private void Start()
    {
        camera = GameObject.FindWithTag("MainCamera");
        background = GameObject.FindWithTag("Background");
    }

    private void Update()
    {
        camera.transform.position = 
            new Vector3(Mathf.Clamp(camera.transform.position.x, 0, 5.5f), camera.transform.position.y, -10f);
        background.transform.position = 
            new Vector3(Mathf.Clamp(background.transform.position.x, 0, 5.5f), background.transform.position.y);
        detectorScreen.transform.position = 
            new Vector3(Mathf.Clamp(detectorScreen.transform.position.x, 0, 5.5f), detectorScreen.transform.position.y);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var pos = eventData.delta;
        posDelta = pos;
        isDrag = true;
        if (Math.Abs(pos.x) > Math.Abs(pos.y))
        {
            if (pos.x < 0)
            {
                camera.transform.position -= new Vector3(pos.x/275f, 0, 0); 
                background.transform.position -= new Vector3(pos.x/275f, 0, 0); 
                detectorScreen.transform.position -= new Vector3(pos.x/275f, 0, 0); 
            }
            else
            {
                camera.transform.position -= new Vector3(pos.x/275f, 0, 0); 
                background.transform.position -= new Vector3(pos.x/275f, 0, 0); 
                detectorScreen.transform.position -= new Vector3(pos.x/275f, 0, 0); 
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        isDrag = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isDrag)
        {
            if (posDelta.x < 0)
            {
                camera.transform.DOMoveX(camera.transform.position.x + 0.3f, 0.2f).SetEase(Ease.Linear);
                background.transform.DOMoveX(background.transform.position.x + 0.3f, 0.2f).SetEase(Ease.Linear);
                detectorScreen.transform.DOMoveX(detectorScreen.transform.position.x + 0.3f, 0.2f)
                    .SetEase(Ease.Linear);
            }
            else
            {
                camera.transform.DOMoveX(camera.transform.position.x - 0.3f, 0.2f).SetEase(Ease.Linear);
                background.transform.DOMoveX(background.transform.position.x - 0.3f, 0.2f).SetEase(Ease.Linear);
                detectorScreen.transform.DOMoveX(detectorScreen.transform.position.x - 0.3f, 0.2f)
                    .SetEase(Ease.Linear);
            }
            isDrag = false;
        }
    }
}
