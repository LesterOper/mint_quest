using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;

public class DragArrows : MonoBehaviour, IDraggableItems
{
    [SerializeField] private ClickArrows _arrows;
    [SerializeField] private DragController _dragController;
    [SerializeField] private BallWayController _ballWayController;
    public void moveItem(Vector2 pos)
    {
        Context.SoundPlayer.PlaySoundClick("right_do", AudioMixerController.MasterMixerGroup.SFX, 1);
        transform.DOMove(pos, 0.15f).SetEase(Ease.Linear);
        _ballWayController.Check();
        _dragController.enabled = false;
    }

    public void attachToParentObject()
    {
        throw new System.NotImplementedException();
    }
}
