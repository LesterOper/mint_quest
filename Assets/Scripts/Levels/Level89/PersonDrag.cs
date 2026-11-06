using System;
using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;

public class PersonDrag : MonoBehaviour, IDraggableItems
{
    private GameObject openingDoor;
    private RollingHelper _helper;
    private GameObject target;

    private void Start()
    {
        target = GameObject.Find("exit");
        openingDoor = GameObject.Find("OpeningDoor");
        GetComponent<DragHumans>().SetTarget(target);
        _helper = FindObjectOfType<RollingHelper>();
    }

    public void moveItem(Vector2 pos)
    {
        Context.SoundPlayer.PlaySoundClick("right_do", AudioMixerController.MasterMixerGroup.SFX, 1);
        openingDoor.transform.DORotate(new Vector3(0, -60f, 0), 0.25f).SetEase(Ease.Linear).OnComplete(() =>
        {
            openingDoor.transform.DORotate(new Vector3(0, 0, 0), 0.25f).SetEase(Ease.Linear).OnComplete(() =>
            {
                transform.DOMove(pos, 0.15f).SetEase(Ease.Linear);
            });
        });
        _helper.Increment(gameObject);
    }

    public void attachToParentObject()
    {
    }
}
