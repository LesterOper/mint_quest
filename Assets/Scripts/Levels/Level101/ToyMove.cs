using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;

public class ToyMove : MonoBehaviour, IDraggableItems
{
    [SerializeField] private ToyHelper _helper;
    public void moveItem(Vector2 pos)
    {
        Context.SoundPlayer.PlaySoundClick("right_do", AudioMixerController.MasterMixerGroup.SFX, 1);
        transform.DOMove(pos, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
        {
            GetComponent<DragController>().enabled = false;
            _helper.IncrementToys();
        });
        
    }

    public void attachToParentObject()
    {
    }
}
