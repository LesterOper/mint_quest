using System.Collections;
using System.Collections.Generic;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;

public class TapRotateCamera : MonoBehaviour, IClickItem
{
    [SerializeField] private CameraShootHelper _helper;
    
    public void tapOnItem()
    {
        Context.SoundPlayer.PlaySoundClick("turn_move_1");
        _helper.ActivateSide();
    }
}
