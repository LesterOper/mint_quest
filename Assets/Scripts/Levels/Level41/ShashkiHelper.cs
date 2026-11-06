using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class ShashkiHelper : MonoBehaviour
{
    public List<ShashkiPuzzle> Order
    {
        get => order;
        set => order = value;
    }

    public List<GameObject> TargetOrder
    {
        get => targetOrder;
        set => targetOrder = value;
    }

    [SerializeField] private List<ShashkiPuzzle> order;
    [SerializeField] private List<GameObject> targetOrder;
    [SerializeField] private List<SpriteRenderer> fillObjs;
    private GameObject current;
    private SpriteRenderer currentFill;
    private int index = 0;
    private void Start()
    {
        current = targetOrder[index];
        currentFill = fillObjs[index];
        order.ForEach(obj =>
        {
            obj.GetComponent<DragShashki>().SetTarget(current);
        });
    }

    public void PutShashki(ShashkiPuzzle currentDragged)
    {
            currentDragged.FillSquere(currentFill);
            index++;
            if (index == order.Count)
            {
                Invoke(nameof(InvokeComplete), 0.2f);
                return;
            }

            current = targetOrder[index];
            currentFill = fillObjs[index];
            order.ForEach(obj =>
            {
                obj.GetComponent<DragShashki>().SetTarget(current);
            });
    }

    public void ResetShashki()
    {
        Context.SoundPlayer.PlaySoundClick("error");
        order.ForEach(obj => { obj.ResetObj(); });
        index = 0;
        current = targetOrder[index];
        currentFill = fillObjs[index];
        order.ForEach(obj =>
        {
            obj.GetComponent<DragShashki>().SetTarget(current);
        });
        fillObjs.ForEach(obj => { obj.DOFade(0, 0.15f).SetEase(Ease.Linear); });
    }

    private void InvokeComplete()
    {
        Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
    }


    public GameObject GetClosestTargetObject(Transform dragged)
    {
        GameObject minTarget = targetOrder[index];
        for (int i = index; i < targetOrder.Count; i++)
        {
            if (Vector3.Distance(dragged.position , minTarget.transform.position) > Vector3.Distance(dragged.position,targetOrder[0].transform.position))
            {
                minTarget = targetOrder[i];
            }
        }

        return minTarget;
    }
}
