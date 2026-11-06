using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using UnityEngine;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;

public class PiggysNoseController : MonoBehaviour
{
    [SerializeField] private Sprite _spriteSwitchOn = null;
    [SerializeField] private Sprite _spriteSwitchOff = null;
    [SerializeField] private GameObject _objLightSwitch = null;
    [SerializeField] private GameObject _objLightBackground = null;
    [SerializeField] private GameObject _objNose = null;
    [SerializeField] private GameObject _piggy = null;
     
    private SpriteRenderer _spriteLightSwitch;
    private bool _light = true;
    private Vector3 _piggysNoseFinishPosition = new Vector3(1.12f, -5.25f);
    private float cameraSize = 1f;
    
    public void SwitchLight()
    {
        _light = !_light;

        if (_light)
        {
            _spriteLightSwitch.sprite = _spriteSwitchOn;
            _objLightBackground.SetActive(true);
        }
        else
        {
            _spriteLightSwitch.sprite = _spriteSwitchOff;
            _objLightBackground.SetActive(false);
        }
    }

    private void RunFinishLVLActions()
    {
        
        _objLightSwitch.GetComponent<PiggysNoseLightSwitch>().enabled = false;
        _light = true;
        _spriteLightSwitch.sprite = _spriteSwitchOn;
        _objLightBackground.SetActive(true);
        Context.SoundPlayer.PlaySoundClick("pig_new_2", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
        _piggy.GetComponent<Animation>().Play("PiggysDisappear");
        Invoke("DropDownLightSwitch", 1f);
        Invoke("ShowCoolBtn", 2f);
    }

    private void DropDownLightSwitch()
    {
        _objLightSwitch.GetComponent<Animation>().Play();
    } 
    
    private void ShowCoolBtn()
    {
        RefreshSizeCamera();
        Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
    }
    
    public void MoveToNosePosition()
    {
        _objNose.GetComponent<MoveController>().enabled = false;
        _objNose.transform.DOMove(_piggysNoseFinishPosition, 0.5f).OnComplete(RunFinishLVLActions);
    }

    private void RefreshSizeCamera()
    {
        try
        {        
            Camera _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            _camera.orthographic = true;
            _camera.orthographicSize = cameraSize;
        }
        catch
        {
            // ignored
        }
    }
    
    private void OnDestroy()
    {
        RefreshSizeCamera();
    }

    // Start is called before the first frame update
    void Start()
    {
        Camera _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        _camera.orthographic = true;
        cameraSize = _camera.orthographicSize;
        _camera.orthographicSize = 7.0f;
        
        _spriteLightSwitch = _objLightSwitch.GetComponent<SpriteRenderer>();
        _spriteLightSwitch.sprite = _spriteSwitchOn;
        _objLightBackground.SetActive(true);
    }
}
