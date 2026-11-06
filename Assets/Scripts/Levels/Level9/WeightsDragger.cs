using System;
using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeightsDragger : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject targetOne, targetTwo;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D _collider2D;
    [SerializeField] private WeightsController _controller;
    [SerializeField] private GameObject leftBorder, rightBorder;
    [SerializeField] private GameObject downBorder;
    public float offset;
    public bool isNeedToAffect = false;
    private Vector3 mOffset;
    private Vector3 startPos;
    private float mZCoord;
    private float scale;
    private Vector3 cam;

    private void Start()
    {
        cam = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        leftBorder.transform.position = new Vector3(-cam.x, leftBorder.transform.position.y);
        rightBorder.transform.position = new Vector3(cam.x, rightBorder.transform.position.y);
        startPos = transform.position;
        scale = transform.localScale.x;
    }

    private void Update()
    {
        var tr = transform.position;
        tr = new Vector3(Mathf.Clamp(tr.x, leftBorder.transform.position.x, rightBorder.transform.position.x), 
            Mathf.Clamp(tr.y, downBorder.transform.position.y, cam.y), 0);
        transform.position = tr;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        if(isNeedToAffect)
            ObjectsDeformationManager.ScaleObject(scale * 1.2f, transform, 0.15f);
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        DeactivatePhysics();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(isNeedToAffect)
            ObjectsDeformationManager.ScaleObject(scale, transform, 0.15f);

        ActivatePhysics();
    }
    
    private Vector3 GetMouseWorldPos()
    {
        //x,y
        Vector3 mousePoint = Input.mousePosition;
        //z
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }


    private void ActivatePhysics()
    {
        _collider2D.isTrigger = false;
        rb.constraints = RigidbodyConstraints2D.None;
    }

    private void DeactivatePhysics()
    {
        _collider2D.isTrigger = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
    }
}
