using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using UnityEngine;

public class SupportLevelScript : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxIqos;
    [SerializeField] private GameObject cap;
    [SerializeField] private GameObject connectToBox;
    [SerializeField] private GameObject iqos;
    [SerializeField] private RotateIqos iqosRotate;

    private void Start()
    {
        iqos.GetComponent<DragController>().enabled = false;
    }

    public void SetEnable()
    {
        iqosRotate.enabled = true;
        iqosRotate.setEnableRotate(true);
        boxIqos.enabled = true;
        cap.GetComponent<DragController>().enabled = false;
        //iqos.GetComponent<DragController>().enabled = true;
        connectToBox.SetActive(true);
    }

    public void SpecialEffect()
    {
        transform.DORotate(new Vector3(0, 0, 320f), 0.15f).SetEase(Ease.Linear);
        boxIqos.GetComponent<BoxIqosContr>().changeState();
        iqos.SetActive(false);
        
        Invoke(nameof(AddToSpecialEffect), 0.5f);
    }

    private void AddToSpecialEffect()
    {
        boxIqos.transform.DOMove(new Vector3(0f, 0f), 0.2f).SetEase(Ease.Linear).OnComplete(() =>
        {
            boxIqos.transform.DOScale(new Vector3(10f, 10f, 10f), 0.25f).SetEase(Ease.Linear).OnComplete(() =>
            {
                Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
            });
        });
    }

    private void setupEffect()
    {
        Messenger.Broadcast(Messages.MINT_EFFECT);
    }

    public void setAttachForIqos()
    {
        iqos.GetComponent<ItemDragController>().Attach = EnumNeedAttach.Attach;
    }

    public void EnableIqosDrag()
    {
       // iqos.GetComponent<DragController>().enabled = true;
    }
}