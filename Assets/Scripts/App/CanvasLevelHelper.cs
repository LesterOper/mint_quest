using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using Swimmer.Library;
using UnityEngine;

public class CanvasLevelHelper : MonoBehaviour
{
    private void OnEnable()
    {
        //Messenger.AddListener(Messages.DEACTIVATE_CANVAS, DeActivateCanvas);
    }

    private void OnDisable()
    {
       // Messenger.RemoveListener(Messages.DEACTIVATE_CANVAS, DeActivateCanvas);
    }

    private void Start()
    {
        
    }

    private void DeActivateCanvas()
    {
        FindObjectOfType<Destroyer>().gameObject.SetActive(false);
    }
}
