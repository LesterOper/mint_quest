using System;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private CarAndKeysController _controller;
    [SerializeField] private GameObject carKeysPlace;
    private bool isDrag;
    private Vector3 mOffset;
    private float mZCoord;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        isDrag = false;
        if (carKeysPlace != null)
        {
            if (Vector3.Distance(transform.position, carKeysPlace.transform.position) <= 0.35f)
            {
                if (_controller.GetRideStatus() == CarAndKeysController.CarStatus.WALL_WAS_BROCKEN)
                {
                    _controller.OpenDoor();
                    Context.SoundPlayer.PlaySoundClick("car_open_signal");
                }
            }
            else
            {
                transform.DOMove(startPos, 0.15f).SetEase(Ease.Linear);
            }
        }
        else
        {
            if(FindObjectOfType<GameController>().CurrentLevel.name != "Level 77(Clone)")
                transform.DOMove(startPos, 0.15f).SetEase(Ease.Linear);
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        isDrag = true;
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
