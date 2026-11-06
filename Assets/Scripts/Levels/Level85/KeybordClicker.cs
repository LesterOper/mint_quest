using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using UnityEngine;

public class KeybordClicker : MonoBehaviour, IClickItem
{
    [SerializeField] private SpriteRenderer sprite;
    public void tapOnItem()
    {
        sprite.DOFade(1, 0.1f).SetEase(Ease.Linear).OnComplete(() => { sprite.DOFade(0, 0.1f).SetEase(Ease.Linear); });
    }
}
