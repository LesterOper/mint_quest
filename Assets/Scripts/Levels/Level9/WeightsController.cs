using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using UnityEngine;

public class WeightsController : MonoBehaviour, IClickItem
{
    [SerializeField] private Rigidbody2D constObj;
    [SerializeField] private List<Collider2D> allColliders;
    [SerializeField] private Rigidbody2D plank;
    public bool isComplete;
    
    private List<Rigidbody2D> left, right;

    private void Start()
    {
        
        left = new List<Rigidbody2D>();
        right = new List<Rigidbody2D>();
    }

    public void tapOnItem()
    {
    }

    public void WeightsAdd(Rigidbody2D dragged, bool isLeft)
    {
        
        if (isLeft && !left.Contains(dragged))
            left.Add(dragged);
        else if(!isLeft && !right.Contains(dragged))
            right.Add(dragged);

        if (left.Count + right.Count == 3)
        {
            if (IsMassEquals())
            {
                Invoke(nameof(Complete), 1.5f);
            }
        }
    }

    public void TryToDelete(Rigidbody2D rb, bool isLeft)
    {
        if (isLeft) left.Remove(rb);
        else right.Remove(rb);
    }
    
    private bool IsMassEquals()
    {
        float massL = 0, massR = 0;
        left.ForEach(obj => { massL += obj.mass; });
        massL += constObj.mass;
        right.ForEach(obj => { massR += obj.mass; });
        return massL == massR;
    }

    private void DeactivateColliders()
    {
        allColliders.ForEach(obj => { obj.enabled = false; });
    }

    private void Complete()
    {
        if (IsMassEquals())
        {
            isComplete = true;
            plank.bodyType = RigidbodyType2D.Static;
            plank.transform.DORotate(new Vector3(0, 0, 0), 1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                Invoke(nameof(InvokeComplete), 0.5f);
            });
        }
    }

    private void InvokeComplete()
    {
        DeactivateColliders();
        Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
    }
}