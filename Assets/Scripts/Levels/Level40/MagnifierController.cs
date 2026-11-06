using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;

public class MagnifierController : MonoBehaviour, IDraggableItems
{
    [SerializeField] private BoxCollider2D cockroanch;
    [SerializeField] private BoxCollider2D magnifier;
    public void moveItem(Vector2 pos)
    {
        Context.SoundPlayer.PlaySoundClick("right_do", AudioMixerController.MasterMixerGroup.SFX, 1);
        transform.DOMove(pos, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
        {
            cockroanch.enabled = true;
            magnifier.enabled = false;
        });
    }

    public void attachToParentObject()
    {
    }
}
