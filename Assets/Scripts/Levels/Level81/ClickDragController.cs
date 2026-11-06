using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;

public class ClickDragController : MonoBehaviour, IClickItem, IDraggableItems
{
    [SerializeField] private ExitLevelHelper _helper;
    [SerializeField] private float angle;
    public void tapOnItem()
    {
        transform.DORotate(new Vector3(0, 0, angle), 0.15f).SetEase(Ease.Linear);
    }

    public void moveItem(Vector2 pos)
    {
        Context.SoundPlayer.PlaySoundClick("right_do", AudioMixerController.MasterMixerGroup.SFX, 1);
        transform.DOMove(pos, 0.15f).SetEase(Ease.Linear);
        GetComponent<DragController>().enabled = false;
        _helper.IncrementCount();
    }

    public void attachToParentObject()
    {
        
    }
}
