using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using UnityEngine;

public class ZoomCircleWithSpider : MonoBehaviour, IZoom
{
    [SerializeField] private BoxCollider2D spriderCollider;
    public void Scale(float amountScaleToZoom)
    {
        var scale = transform.localScale;
        transform.DOScale(new Vector3(scale.x * amountScaleToZoom, scale.y * amountScaleToZoom, scale.z * amountScaleToZoom), 0.25f).SetEase(Ease.Linear);

        if (scale.x > 0.4f)
        {
            spriderCollider.enabled = true;
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
