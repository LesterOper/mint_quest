using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;

public class CartridgesController : MonoBehaviour, IDraggableItems
{
    [SerializeField] private GameObject targetComplete;
    [SerializeField] private PistolController _pistolController;
    public void moveItem(Vector2 pos)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        transform.DOMove(pos, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
            {
                transform.DOMove(targetComplete.transform.position, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    _pistolController.SetFlag();
                    GetComponent<DragController>().enabled = false;
                });
            });
    }

    public void attachToParentObject()
    {
        throw new System.NotImplementedException();
    }
}
