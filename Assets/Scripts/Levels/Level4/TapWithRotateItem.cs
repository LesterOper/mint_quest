using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using UnityEngine;

public class TapWithRotateItem : MonoBehaviour , IClickItem
{
    [SerializeField] private AllIn1Shader _shader;
    [SerializeField] private Material _materialDefault;
    [SerializeField] private Transform _rotateItem;
    [SerializeField] private float _angle = 30f;
    [SerializeField] private DragItem _drag;
    [Space] 
    [SerializeField] private bool _ifNeedDragAfterRotate = false;

    private Vector3 _baseScale;
    private Material _renderMaterial;
    
    private int _countRotate = 0;
    private bool _completeRotate = false;
    private bool _canRotate = true;
    

    private void Start()
    {
        float widthScreen = Screen.width;
        
        _renderMaterial = GetComponent<SpriteRenderer>().material;
        
        if (_ifNeedDragAfterRotate) _drag.enabled = false;
        _baseScale = transform.localScale;
    }

    public void tapOnItem()
    {
        if (!_completeRotate && _canRotate)
        {
            Anim();
            _canRotate = false;
             _rotateItem.DORotate(new Vector3(0, 0, transform.eulerAngles.z + _angle), 0.1f).OnComplete((() =>
             {
                 if (_countRotate == 5)
                 {
                     //if (_ifNeedDragAfterRotate) _drag.enabled = true;
                     _completeRotate = true;
                     
                     _shader.enabled = false;
                     GetComponent<SpriteRenderer>().material = _materialDefault;
                     
                     Messenger.Broadcast(Messages.DRAG_ITEM_CHANGED_CORRECT_ROTATION_TO_END);
                 }
                 else
                 {          
                     _countRotate++;
                     _canRotate = true;
                 }
             }));
        }
    }

    
    private void Anim()
    {
        Sequence animClick = DOTween.Sequence();
        
        animClick.Append(_renderMaterial.DOFloat(1, "_ShineLocation", 0.1f));
        animClick.Join(transform.DOScale(new Vector3(transform.localScale.x * 0.9f, transform.localScale.y * 0.9f, transform.localScale.z * 0.9f), 0.05f));
        animClick.Append(transform.DOScale(new Vector3(_baseScale.x, _baseScale.y, _baseScale.z), 0.05f));
        animClick.Append(_renderMaterial.DOFloat(0, "_ShineLocation", 0f));
    }


    
}
