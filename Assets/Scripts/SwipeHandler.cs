using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeHandler : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    [SerializeField] private Sprite onSwitcher;
    [SerializeField] private Sprite offSwitcher;
    [SerializeField] private GameObject switcher;
    [SerializeField] private float onPositionX, offPostionX;
    private bool isActive;
    
    void Start()
    {
        
        isActive = PlayerPrefs.GetInt(gameObject.name) == 1;
        MoveAndChangeSwitcher();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Math.Abs(eventData.delta.x) > Math.Abs(eventData.delta.y))
        {
            if (eventData.delta.x > 0 && !isActive)
            {
                MoveAndChangeSwitcher();
                isActive = true;
            }
            else
            {
                MoveAndChangeSwitcher();
                isActive = false;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    private void MoveAndChangeSwitcher()
    {
        Sprite sprite;
        float targetPos;
        if (isActive)
        {
            sprite = onSwitcher;
            targetPos = onPositionX;
        }
        else
        {
            sprite = offSwitcher;
            targetPos = offPostionX;
        }

        switcher.GetComponent<RectTransform>().DOLocalMoveX(targetPos, 1f);
        switcher.GetComponent<Image>().DOFade(0, 0.5f).OnComplete(() =>
        {
            switcher.GetComponent<Image>().sprite = sprite;
            switcher.GetComponent<Image>().DOFade(1, 0.5f);
        });
    }
}
