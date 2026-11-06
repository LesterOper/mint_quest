using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using TMPro;
using UnityEngine;

public class TapButton : MonoBehaviour, IClickItem
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Vector3 _baseScale;

    private void Start()
    {
        _baseScale = transform.localScale;
    }

    public void tapOnItem()
    {
        _spriteRenderer.DOFade(1, 0.05f).OnComplete((() =>
        {
            _text.enabled = false;
            GetComponent<ClickController>().enabled = false;
            Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
        }));
    }
}
