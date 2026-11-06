﻿using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using Swimmer.Library;
using UnityEngine;

public class Level_50 : MonoBehaviour
{
    [SerializeField] private Transform[] _transformsPart;
    [SerializeField] private Transform[] _endTransforms;
    [SerializeField] private bool[] _isCompletePart;
    [SerializeField] private ZoomController _zoomController;
    [SerializeField] private DragItem dr;
    
    private int _partCount = 0;
    private void OnEnable()
    {
        Messenger.AddListener(Messages.DRAG_ITEM_CHANGED_POSITION_TO_END, CheckLevel);
    }
    
    private void OnDisable()
    {
        Messenger.RemoveListener(Messages.DRAG_ITEM_CHANGED_POSITION_TO_END, CheckLevel);
    }

    private void Start()
    {
    }

    private void Update()
    {
        /*var input = Input.touchCount;
        if (input == 1)
        {
            dr.enabled = true;
            _zoomController.enabled = false;
        }
        else if (input == 2)
        {
            dr.enabled = false;
            _zoomController.enabled = true;
        }*/
    }

    private void CheckLevel()
    {
        for (int i = 0; i < _transformsPart.Length; i++)
        {
            if (_isCompletePart[i] == false)
            {
                if (_transformsPart[i].position == _endTransforms[i].position)
                {
                    _isCompletePart[i] = true;
                    _partCount++;
                    
                    _transformsPart[i].GetComponent<DragItem>().enabled = false;
                    _transformsPart[i].GetComponent<PolygonCollider2D>().enabled = false;
                }
            }
        }
        
        if(_partCount == _transformsPart.Length) 
            Invoke(nameof(Effect), 0.2f);
    }

    private void Effect()
    {
        Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
    }
    
    private IEnumerator CheckCount()
    {
        if (_partCount == _transformsPart.Length)
        {
            yield return new WaitForSeconds(0.2f);
            Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
        }
    }
}
