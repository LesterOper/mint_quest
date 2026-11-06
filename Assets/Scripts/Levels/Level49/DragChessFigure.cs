using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragChessFigure : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private ChessHelper _helper;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] public bool isWhiteFigure;
    private Vector3 mOffset;
    private Vector2 startPos;
    private float mZCoord;
    private Vector2 currentPos;

    private void Start()
    {
        startPos = _rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        currentPos = eventData.pointerCurrentRaycast.screenPosition;
        transform.position = currentPos;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _helper.SetDragged(this);
        _helper.MoveChessFigure(_rectTransform.anchoredPosition);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }
    
    private Vector3 GetMouseWorldPos()
    {
        //x,y
        Vector3 mousePoint = Input.mousePosition;
        //z
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public Vector2 GetPos()
    {
        return startPos;
    }

    public void MoveToStartPos()
    {
        _rectTransform.DOAnchorPos(startPos, 0.2f).SetEase(Ease.Linear);
    }

    public void MoveToTargetPos(Vector2 targetPos)
    {
        _rectTransform.DOAnchorPos(targetPos, 0.2f).SetEase(Ease.Linear);
    }
}
