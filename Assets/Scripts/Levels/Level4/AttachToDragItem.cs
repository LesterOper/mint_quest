using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using UnityEngine;

public class AttachToDragItem : MonoBehaviour, IDraggableItems
{
    [SerializeField] private EnumNeedAttach _attach;
    [SerializeField] private GameObject parent;
    public void moveItem(Vector2 pos)
    {
        transform.DOMove(pos, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (_attach == EnumNeedAttach.Attach)
            {
                attachToParentObject();
            }
        });
    }

    public void attachToParentObject()
    {
        transform.SetParent(parent.transform);
        //FindObjectOfType<GameController>().DestroyCurrentLevel();
        Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
    }
}
