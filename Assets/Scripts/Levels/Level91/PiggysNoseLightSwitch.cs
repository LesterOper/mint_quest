using System.Collections;
using System.Collections.Generic;
using App;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class PiggysNoseLightSwitch : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private PiggysNoseController _piggysNoiseController = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        _piggysNoiseController.SwitchLight();
    }
}
