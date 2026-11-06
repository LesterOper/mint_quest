using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessHelper : MonoBehaviour
{
    [SerializeField] private List<DragChessFigure> whiteSide, blackSide;
    [SerializeField] private GameObject canvas;
    private DragChessFigure _dragged;

    private void Awake()
    {
        canvas.SetActive(true);
    }


    public void MoveChessFigure(Vector2 pointerUpPos)
    {
        if (_dragged.isWhiteFigure)
        {
            var figure = blackSide.Find(obj => Vector2.Distance(pointerUpPos, obj.GetComponent<RectTransform>().anchoredPosition) <= 50f);
            if (figure != null)
            {
                var targetPos = figure.GetComponent<RectTransform>().anchoredPosition;
                var tgPos = _dragged.GetPos();
                _dragged.MoveToTargetPos(targetPos);
                figure.MoveToTargetPos(tgPos);
            }
            else
            {
                _dragged.MoveToStartPos();
            }
        }
        else
        {
            var figure = whiteSide.Find(obj => Vector3.Distance(pointerUpPos, obj.GetComponent<RectTransform>().anchoredPosition) <= 50f);
            if (figure != null)
            {
                var targetPos = figure.GetComponent<RectTransform>().anchoredPosition;
                var tgPos = _dragged.GetPos();
                _dragged.MoveToTargetPos(targetPos);
                figure.MoveToTargetPos(tgPos);
            }
            else
            {
                _dragged.MoveToStartPos();
            }
        }
    }

    public void SetDragged(DragChessFigure figure)
    {
        _dragged = figure;
    }
}
