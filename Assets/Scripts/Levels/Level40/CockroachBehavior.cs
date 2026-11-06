using System.Collections;
using System.Collections.Generic;
using App.Global;
using Levels;
using Swimmer.Library;
using UnityEngine;

public class CockroachBehavior : MonoBehaviour, IClickItem
{
    [SerializeField] private Animation anim;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public void tapOnItem()
    {
        _spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
        anim.Play();
        Invoke(nameof(CompleteEffect), 2.5f);
    }

    private void CompleteEffect()
    {
        Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
    }
}
