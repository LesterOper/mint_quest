using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class CrackScreen : MonoBehaviour, IClickItem
{
    [SerializeField] private List<GameObject> crackGlassSprites;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private int index = 0;

    public void tapOnItem()
    {
        Context.SoundPlayer.PlaySoundClick("break_glass");
        if (index == 5)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
        }
        else
        {
                crackGlassSprites[index].SetActive(true);
                index++;
        }
    }
}