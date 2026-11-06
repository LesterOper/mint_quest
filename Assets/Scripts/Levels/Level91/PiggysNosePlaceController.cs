using System;
using System.Collections;
using System.Collections.Generic;
using Swimmer.Library.Common;
using UnityEngine;

public class PiggysNosePlaceController : MonoBehaviour
{
    [SerializeField] private PiggysNoseController _piggysNoseController = null;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "NosesPlace")
        {
            Context.SoundPlayer.PlaySoundClick("right_do");
            _piggysNoseController.MoveToNosePosition();
        }
    }
}
