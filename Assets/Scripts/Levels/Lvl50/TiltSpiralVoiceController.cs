using System.Collections;
using System.Collections.Generic;
using App.Global;
using UnityEngine;
using DG.Tweening;
using Swimmer.Library;

public class TiltSpiralVoiceController : MonoBehaviour
{

    [SerializeField] private GameObject _spiralFront = null;
    [SerializeField] private GameObject _spiral = null;
    [SerializeField] private BoxCollider2D _spiralFrontColliderArea = null;
    [SerializeField] private VoiceVolumeController _voiceVolumeController = null;

    private float spiralAngle = 0;
    private bool _isPlaceSpiral = false;
    private bool isFinish = false;

    // Start is called before the first frame update
    void Start()
    {
        RefreshAccelerationData();
    }

    public void RefreshAccelerationData()
    {
        float loudness = _voiceVolumeController.GetLoudness();
        if (loudness>0.3f)
        {
            spiralAngle += (loudness/2.5f);
        }
    }
    
    public void StopRotationAndPlayFinishActions()
    { 
        _spiralFrontColliderArea.enabled = false;
        isFinish = true;
         ShowCoolBtn();
        _spiralFront.transform.eulerAngles = new Vector3(0f, 0f, 30f);
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(_spiral.transform.DOScale(3f, 4f));
    }

    private void ShowCoolBtn()
    {
        Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
    }
    
    void Update()
    {
        if (isFinish == false)
        {
            RefreshAccelerationData();
            Quaternion currentRotation = _spiralFront.transform.rotation;
            Quaternion wantedRotation = Quaternion.Euler(0,0,spiralAngle);
            _spiralFront.transform.rotation = Quaternion.RotateTowards(currentRotation, wantedRotation, Time.deltaTime * 32f);
        }
    }
}
