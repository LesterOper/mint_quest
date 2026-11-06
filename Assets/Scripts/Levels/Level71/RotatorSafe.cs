using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;

public class RotatorSafe : MonoBehaviour, IClickItem
{
    [SerializeField] private float angle;
    [SerializeField] private GameObject safe;
    [SerializeField] private CircleCollider2D _collider2D;
    [SerializeField] private SafeController _safeController;
    private int numberRotate = 0;

    public int NumberRotate
    {
        get => numberRotate;
        set => numberRotate = value;
    }

    public void tapOnItem()
    {
        GetComponent<SpriteRenderer>().DOFade(0.4f, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
        {
            GetComponent<SpriteRenderer>().DOFade(0.4f, 0.15f).SetEase(Ease.Linear);
        });
        _collider2D.enabled = false;
        safe.transform.DORotate(new Vector3(0, 0, safe.transform.eulerAngles.z + angle), 0.15f,
            RotateMode.FastBeyond360).OnComplete(() => _collider2D.enabled = true);
        if (angle < 0)
        {
            
            numberRotate += 10;
            Debug.Log("Neg: " + numberRotate);
            if (numberRotate > 90)
                numberRotate = 0;
            if(numberRotate == 60)
                Invoke(nameof(InvokeVibro), 0.2f);
        }
        else
        {
            numberRotate -= 10;
            Debug.Log("Neg: " + numberRotate);
            if (numberRotate < 0)
                numberRotate = 90;
            if(numberRotate == 80)
                Invoke(nameof(InvokeVibro), 0.2f);
        }
        
    }

    private void InvokeVibro()
    {
        _safeController.Vibro();
    }
}