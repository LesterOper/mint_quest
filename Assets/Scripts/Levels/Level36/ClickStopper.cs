using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using UnityEngine;

public class ClickStopper : MonoBehaviour, IClickItem
{
    [SerializeField] private List<GameObject> planks;
    [SerializeField] private List<Vector2> positions;
    private List<GameObject> bufPlanks;
    private Tween _tween;
    private void Start()
    {
        bufPlanks = new List<GameObject>(planks);
        SetDOTWeenPlanks();
    }

    public void tapOnItem()
    {
        GetComponent<SpriteRenderer>().DOFade(0.3f, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
            {
                GetComponent<SpriteRenderer>().DOFade(1, 0.15f).SetEase(Ease.Linear);
            });
        var plank = planks[0];
        var position = positions[0];

        if (Vector2.Distance(plank.transform.position, position) <= 0.4f)
        {
            _tween.Kill();
            planks.RemoveAt(0);
            positions.RemoveAt(0);
            plank.transform.DOMoveX(position.x, 0.15f).SetEase(Ease.Linear);
            if (planks.Count == 0)
            {
                GetComponent<CircleCollider2D>().enabled = false;
                Effect();
            }
            else 
                SetDOTWeenPlanks();
        }
    }

    private void SetDOTWeenPlanks()
    {
        _tween = planks[0].transform.DOMoveX(7f, 2.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }

    private void Effect()
    {
        Sequence sequence = DOTween.Sequence();
        bufPlanks.ForEach(obj => { sequence.Join(obj.transform.DOScale(new Vector3(7, 7, 7), 0.25f).SetEase(Ease.Linear)); });
        
        sequence.OnComplete(Cool);

    }

    private void Cool()
    {
        gameObject.GetComponent<SpriteRenderer>().DOFade(0, 0.1f).SetEase(Ease.Linear);
        Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
    }
}
