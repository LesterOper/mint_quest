using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ArrowAnim : MonoBehaviour
{
    [SerializeField] private RectTransform arrowTr;
    private Vector2 startRectPos;

    public Vector2 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        startRectPos = arrowTr.anchoredPosition;
        Sequence sequence = DOTween.Sequence();

        sequence.Append(arrowTr.DOAnchorPos(targetPos, 0.25f).SetEase(Ease.Linear));
        sequence.Append(arrowTr.DOAnchorPos(startRectPos, 0.25f).SetEase(Ease.Linear));
        sequence.SetLoops(-1, LoopType.Yoyo);
    }
    
}
