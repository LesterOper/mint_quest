using System;
using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Swimmer.Library.Common;
using UnityEngine;

public class DetectorObject : MonoBehaviour
{
    [SerializeField] private WeightsController _controller;
    [SerializeField] private GameObject piece;
    public bool isLeft;
    private Vector3 pos;
    private float scale;

    private void Start()
    {
        pos = piece.transform.position;
        scale = piece.transform.localScale.x;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.GetComponent<WeightsDragger>() != null)
        {
            _controller.WeightsAdd(other.collider.GetComponent<Rigidbody2D>(), isLeft);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Rigidbody2D>().constraints != RigidbodyConstraints2D.FreezeAll)
        {
            _controller.TryToDelete(other.gameObject.GetComponent<Rigidbody2D>(),isLeft);
        }
        
        if (other.gameObject == piece)
        {
            /*piece.transform.DOScale(0, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
            {
                piece.transform.DOMove(pos, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        piece.transform.DOScale(scale, 0.15f);
                    });
            });*/
            if(!FindObjectOfType<WeightsController>().isComplete)
                FindObjectOfType<GameController>().RestartLevel();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _controller.TryToDelete(other.GetComponent<Rigidbody2D>(),isLeft);
    }
}
