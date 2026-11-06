using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using UnityEngine;

public class SimpleMoveAndAttache : MonoBehaviour, IDraggableItems
{
    [SerializeField] private GameObject parent;
    public void moveItem(Vector2 pos)
    {
        transform.DOMove(pos, 0.15f).SetEase(Ease.Linear);
        FindObjectOfType<GameController>().CoolButtonLevelCompleteEffect();
    }

    public void attachToParentObject()
    {
        gameObject.transform.SetParent(parent.transform);
    }
}
