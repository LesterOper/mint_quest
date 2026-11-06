using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DoorController : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject lockDoor;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject eye;
    [SerializeField] private DoorOpenController _doorOpenController;
    private GameObject back;
    private GameController _game;
    private bool canRotate = true;
    private float mZCoord;
    private Vector3 mOffset;
    private AudioSource _audioSource;

    private bool doorToDrag = false;

    private void Start()
    {
        _game = FindObjectOfType<GameController>();
        background.GetComponent<DoorOpenController>().enabled = false;
        Camera.main.orthographic = false;
        back = GameObject.Find("background");
        back.SetActive(false);
    }

    private void DoorOpen()
    {
        var scale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        eye.transform.SetParent(background.transform);
        lockDoor.transform.SetParent(background.transform);
        transform.SetParent(background.transform);
        doorToDrag = true;
    }

    public void DiasableDoor()
    {
        _doorOpenController.enabled = false;
        transform.DORotate(new Vector3(0, 0, -40f), 0.15f).SetEase(Ease.Linear);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        var pos = eventData.delta;
        Debug.Log(doorToDrag);
        if (doorToDrag)
        {
            //_audioSource = Context.SoundPlayer.PlaySound(_audioSource ,"door", AudioMixerController.MasterMixerGroup.SFX,PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
            var dir = GetMouseWorldPos();
            if (background.transform.rotation.y * 100 > 0)
            {
                background.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            }
            else
            {
                background.transform.Rotate(new Vector3(0f, eventData.delta.x / 7f, 0), Space.World);
            }
        }
        else
        {
            
            Rotate(eventData);
        }
    }

    private void Rotate(PointerEventData eventData)
    {
        var pos = eventData.delta;
        if (Math.Abs(pos.x) < Math.Abs(pos.y) && !doorToDrag)
        {
            if (pos.y < 0)
            {
                transform
                    .DORotate(new Vector3(0, 0, (transform.rotation.z - pos.y) / 100), 0.15f)
                    .SetEase(Ease.Linear)
                    .OnComplete(DoorOpen);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("key_1", AudioMixerController.MasterMixerGroup.SFX, 1);
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
        transform.DORotate(new Vector3(0, 0, -40f), 0.15f).SetEase(Ease.Linear);
        doorToDrag = false;
        Debug.Log(background.transform.localRotation.y);
        if (background.transform.rotation.y <= -0.3f)
        {
            Context.SoundPlayer.StopPlayingSounds(_audioSource);
            background.transform.DORotate(new Vector3(0, -100f, 0), 0.15f).SetEase(Ease.Linear).OnComplete(() =>
            {
                Camera.main.orthographic = true;
                //box.enabled = false;
                Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
            });
        }
        else
        {
            Context.SoundPlayer.StopPlayingSounds(_audioSource);
            background.transform.DORotate(new Vector3(0, 0, 0), 0.2f);
            DiasableDoor();
        }
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