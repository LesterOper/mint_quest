using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using UnityEngine;

public class MirrorClickController : MonoBehaviour, IClickItem
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject pair;
    [SerializeField] private MirrorClickHelper _helper;
    public void tapOnItem()
    {
        if (_helper.CheckPair(gameObject, pair))
        {
            _renderer.DOFade(0, 0.15f).SetEase(Ease.Linear);
            GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
}
