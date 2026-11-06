using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using UnityEngine;

public class DoorZoom : MonoBehaviour, IZoom
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject openDoor;
    [SerializeField] private Transform legs;

    private float startPos;
    private void Start()
    {
        Camera.main.orthographic = false;
    }

    public void Scale(float amountToZoom)
    {
        door.transform.DOLocalMoveZ(door.transform.localPosition.z - 3f, 0.25f).SetEase(Ease.Linear);
        legs.DOLocalMoveZ( legs.localPosition.z - 3f, 0.25f)
            .SetEase(Ease.Linear);
        if (door.transform.localPosition.z <= 1f)
        {
            GetComponent<ZoomController>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            door.transform.DOMove(new Vector3(5.5f, 0), 0.55f).SetEase(Ease.Linear).OnComplete(() =>
            {
                Camera.main.orthographic = true;
                Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
            });
            openDoor.transform.DORotate(new Vector3(0, -130f, 0), 0.45f).SetEase(Ease.Linear);
        }
    }
}
