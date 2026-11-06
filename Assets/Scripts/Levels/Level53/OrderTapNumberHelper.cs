using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class OrderTapNumberHelper : MonoBehaviour
{
    [SerializeField] private List<PolygonCollider2D> correctOrder;
    private List<PolygonCollider2D> bufCorrectOrder;
    private PolygonCollider2D current;


    private void Start()
    {
        bufCorrectOrder = new List<PolygonCollider2D>(correctOrder);
        current = bufCorrectOrder[0];
    }

    public void NumberTapped(PolygonCollider2D tapped)
    {
        if (tapped == current)
        {
            current.enabled = false;
            bufCorrectOrder.RemoveAt(0);
            if (bufCorrectOrder.Count == 0)
            {
                Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
                return;
            }

            current = bufCorrectOrder[0];
        }
        else
            tapped.transform.DOShakePosition(0.6f, new Vector3(0.08f, 0.08f, 0.08f)).SetEase(Ease.Linear).OnComplete(() =>
            {
                Context.SoundPlayer.PlaySoundClick("error", AudioMixerController.MasterMixerGroup.SFX, 1);

                ResetNumbers();
            });
            
    }

    private void ResetNumbers()
    {
        bufCorrectOrder = new List<PolygonCollider2D>(correctOrder);
        current = bufCorrectOrder[0];
        correctOrder.ForEach(obj => { obj.enabled = true; });
    }
}
