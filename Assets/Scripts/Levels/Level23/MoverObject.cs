using System;
using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoverObject : MonoBehaviour, IDraggableItems, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private SpriteRenderer switchButton;
    [SerializeField] private Sprite activate;
    [SerializeField] private GameObject socket;
    [SerializeField] private float offset;
    private Vector3 mOffset;
    private float mZCoord;
    [SerializeField] private SwipeScreen _swipeScreen;

    private Transform objTransform;

    private void Start()
    {
        objTransform = transform;
    }

    public void moveItem(Vector2 pos)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        transform.DOMoveX(pos.x, 0.15f).SetEase(Ease.Linear);
        switchButton.DOFade(1, 0.6f).SetEase(Ease.Linear).OnComplete(() =>
        {
            switchButton.sprite = activate;
            switchButton.DOFade(1, 0.4f);
        });
        //_swipeScreen.SetSwipeScreenEnumParameter();
        switchButton.GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = false;
        FindObjectOfType<GameController>().SetCameraAndBackgroundParameters();
        Camera.main.transform.DOMove(new Vector3(0, 0, -10), 0.3f).SetEase(Ease.Linear);
        GameObject.FindGameObjectWithTag("Background").transform.DOMove(new Vector3(0, 0, 0), 0.3f)
            .SetEase(Ease.Linear);
    }

    private void Update()
    {
        objTransform.position = new Vector3(Mathf.Clamp(objTransform.position.x, 10.9f, 11.7f), objTransform.position.y);
    }

    public void attachToParentObject()
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector3(GetMouseWorldPos().x + mOffset.x, transform.position.y);
        /*if (transform.localPosition.x >= 10.4f)
        {
            transform.localPosition = new Vector3(10.4f, transform.localPosition.y);
        }*/
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Vector2.Distance(socket.transform.position, transform.position) <= offset)
        {
            moveItem(socket.transform.position);
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
