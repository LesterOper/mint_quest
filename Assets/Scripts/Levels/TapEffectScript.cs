using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
public class TapEffectScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Material _material;

    private Vector3 _baseScale;

    private void Start()
    {
        _baseScale = transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Sequence animClick = DOTween.Sequence();

            animClick.Append(_material.DOFloat(1, "_ShineLocation", 0.07f));
            animClick.Append(_material.DOFloat(0, "_ShineLocation", 0f));
    }
}
