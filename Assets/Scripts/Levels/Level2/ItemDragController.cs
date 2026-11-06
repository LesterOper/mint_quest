using System;
using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;

public class ItemDragController : MonoBehaviour, IDraggableItems
{
    [SerializeField] private GameObject iqos;
    [SerializeField] private GameObject attachCap;
    [SerializeField] private EnumNeedAttach _attach;
    [SerializeField] private GameObject box;
    private Vector3 startPos;
    private bool isEnableToDetectBox = true;
    public EnumNeedAttach Attach
    {
        get => _attach;
        set => _attach = value;
    }


    [SerializeField] private GameObject parent;

    private void Start()
    {
        startPos = transform.position;
    }


    private void Update()
    {
        if(!isEnableToDetectBox) return;
        if (gameObject.name == "1")
        {
            if (Vector3.Distance(transform.position, box.transform.position) <= 0.5f)
            {
                transform.DOMove(startPos, 0.15f).SetEase(Ease.Linear);
            }
        }
    }

    public void moveItem(Vector2 pos)
    {
        if (_attach == EnumNeedAttach.Attach && parent.activeInHierarchy)
        {
            if (parent.name == "box")
            {
                if (transform.eulerAngles.z <= 340f && transform.eulerAngles.z >= 320f)
                {
                    Context.SoundPlayer.PlaySoundClick("right_do");
                    transform.DOMove(pos, 0.15f).SetEase(Ease.Linear);
                    Invoke(nameof(StartSpecialEffect), 0.3f);
                    attachToParentObject();
                    return;
                }
                transform.DOMove(startPos, 0.15f).SetEase(Ease.Linear);
            }
            else
            {
                iqos.transform.DORotate(new Vector3(0, 0, 0), 0.25f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    Context.SoundPlayer.PlaySoundClick("right_do");
                    transform.DOMove(attachCap.transform.position, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        attachToParentObject();
                        isEnableToDetectBox = false;
                    });
                });
                
            }

            

        }
    }

    public void attachToParentObject()
    {
        if (parent.name == "iqos")
        {
            FindObjectOfType<SupportLevelScript>().SetEnable();
        }
        transform.SetParent(parent.transform);
    }

    private void StartSpecialEffect()
    {
        FindObjectOfType<SupportLevelScript>().SpecialEffect();
    }
}