using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using UnityEngine;

public class Level_4 : MonoBehaviour
{
    [SerializeField] private Transform _circleTransform;
    [SerializeField] private Transform _triangle;
    [SerializeField] private Transform _endPosTriangle;
    [SerializeField] private Transform _mintCircle;
    [SerializeField] private DragItem _dragItem;

    private int _rotateCount = 0;

    public int RotateCount
    {
        get => _rotateCount;
        set => _rotateCount = value;
    }

    private void Start()
    {
        _dragItem.enabled = false;
        Vector2 screen = Camera.main.ScreenToWorldPoint(new Vector3(-Screen.width, Screen.height));
        _circleTransform.position = new Vector3(screen.x/3f, _circleTransform.position.y);
    }

    private void OnEnable()
    {
        Messenger.AddListener(Messages.DRAG_ITEM_CHANGED_POSITION_TO_END, CompleteLevel);
        Messenger.AddListener(Messages.DRAG_ITEM_CHANGED_CORRECT_ROTATION_TO_END, RotateCompleteParts);
    }
    
    
    private void OnDisable()
    {
        Messenger.RemoveListener(Messages.DRAG_ITEM_CHANGED_POSITION_TO_END, CompleteLevel);
        Messenger.RemoveListener(Messages.DRAG_ITEM_CHANGED_CORRECT_ROTATION_TO_END, RotateCompleteParts);
    }
    

    private void CompleteLevel()
    {
        if (_triangle.position == _endPosTriangle.position && _rotateCount == 2)
        {
            _mintCircle.DOScale(new Vector3(5, 5, 5), 0.5f).SetEase(Ease.Linear).OnComplete((() =>
            {
                Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
            }));
        }
    }

    private void RotateCompleteParts()
    {
        _rotateCount++;
        if (_rotateCount == 2)
        {
            _dragItem.enabled = true;
        }
    }

}
