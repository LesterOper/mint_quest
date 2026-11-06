using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using UnityEngine;

public class Level23Controller : MonoBehaviour, IDraggableItems
{
    [SerializeField] private SwipeScreen _swipeScreen;
    public void moveItem(Vector2 pos)
    {
        transform.DOLocalMoveX(pos.x, 0.15f).SetEase(Ease.Linear);
    }

    public void attachToParentObject()
    {
    }
}
