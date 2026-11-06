using System.Collections;
using System.Collections.Generic;
using App;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class Call911BtnController : MonoBehaviour, IPointerClickHandler
{
    public Call911Controller _call911Controller;
    
    public enum PHONE_BTN
    {
        _0 = 0,
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _5 = 5,
        _6 = 6,
        _7 = 7,
        _8 = 8,
        _9 = 9,
    }
    public PHONE_BTN _Btn = PHONE_BTN._0;
    

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        GetComponent<Animation>().Play();
        _call911Controller.SetPhoneNumber((int)_Btn);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
