using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralPlaceCheckerController : MonoBehaviour
{
    [SerializeField] private TiltSpiralVoiceController _voiceVolumeController = null;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        _voiceVolumeController.StopRotationAndPlayFinishActions();
    }
}
