using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;

public class DoorDetector : MonoBehaviour, IClickItem
{
    [SerializeField] private DoorSwapController doorSwap;
    
    public void tapOnItem()
    {
        if (!doorSwap.IsShuffle)
        {
            if (gameObject.name == "Door (1)")
            {
                doorSwap.IncrementAndCheckCountDetected();
            }
            else
            {
                Context.SoundPlayer.PlaySoundClick("error", AudioMixerController.MasterMixerGroup.SFX, 1);
                transform.DOShakePosition(0.2f, 0.1f, 7).OnComplete(() =>
                {
                    doorSwap.ResetAccordingMistake();
                });
            }
        }
    }
}
