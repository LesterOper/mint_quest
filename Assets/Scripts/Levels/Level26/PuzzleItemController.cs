using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;

public class PuzzleItemController : MonoBehaviour, IDraggableItems
{
    [SerializeField] private PuzzleHelper _helper;
    private bool isCorrectPos = false;

    public bool IsCorrectPos
    {
        get => isCorrectPos;
        set => isCorrectPos = value;
    }

    public void moveItem(Vector2 pos)
    {
        Context.SoundPlayer.PlaySoundClick("right_do", AudioMixerController.MasterMixerGroup.SFX, 1);
        isCorrectPos = true;
        transform.DOMove(pos, 0.15f).SetEase(Ease.Linear);
        _helper.AttachToPuzzle();
    }

    public void attachToParentObject()
    {
    }
}
