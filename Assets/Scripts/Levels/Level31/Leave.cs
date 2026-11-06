using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;
using Random = UnityEngine.Random;

public class Leave : MonoBehaviour
{
    [SerializeField] private  Vector3[] _wayPath;
    [SerializeField] private  Vector3[] _wayPathComplete;
    private AudioSource _source;

    public void MoveDown()
    {
        Vector3 _point = new Vector3(Random.Range(-2f,2f), Random.Range(-2.4f, -2.6f), 0);
        Vector3 _rotate = new Vector3(0, 0, Random.Range(0, 361));
           
        Sequence anim = DOTween.Sequence();
        anim.Append(transform.DOMove(_point, 1.5f).SetEase(Ease.InOutSine));
        anim.Join(transform.DORotate(_rotate, 1.5f).SetEase(Ease.InOutSine));
        anim.OnComplete((() => Messenger.Broadcast(Messages.LEAVE_DOWN)));
    }

    
}
