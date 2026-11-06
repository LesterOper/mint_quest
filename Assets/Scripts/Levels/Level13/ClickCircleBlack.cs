using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using TMPro;
using UnityEngine;

public class ClickCircleBlack : MonoBehaviour, IClickItem
{
    [SerializeField] private TextMeshProUGUI _mintText;
    [SerializeField] private MintAccelerate _mintAccelerate;
    [SerializeField] private SpriteRenderer _spriteRendererBlack;
    public void tapOnItem()
    {
        _mintAccelerate.enabled = false;
        _mintText.DOFade(0, 0.5f);
        
        _mintAccelerate.transform.DOMoveY(-3f, 1f);
        _spriteRendererBlack.DOFade(0, 0.5f).OnComplete((() => Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL)));
    }
}
