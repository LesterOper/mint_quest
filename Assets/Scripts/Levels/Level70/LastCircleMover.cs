using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class LastCircleMover : MonoBehaviour, IDraggableItems
{
    public void moveItem(Vector2 pos)
    {
        Context.SoundPlayer.PlaySoundClick("click_1", AudioMixerController.MasterMixerGroup.SFX, 1);
        transform.DOMove(pos, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
        {
            Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
        });
    }

    public void attachToParentObject()
    {
        throw new System.NotImplementedException();
    }
}
