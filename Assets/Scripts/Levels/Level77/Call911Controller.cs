using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class Call911Controller : MonoBehaviour
{
    [SerializeField] private GameObject _handSet = null;
    [SerializeField] private GameObject _phonePanel = null;
    [SerializeField] private GameObject _lightBackground = null;
    [SerializeField] private List<SpriteRenderer> all;
    private int numberCount;
    private int[] phoneNumber = new int[3];
    private bool isFlooding = false;

    private void OnDestroy()
    {
        DOTween.Kill(_lightBackground.transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        _phonePanel.SetActive(false);
        phoneNumber[0] = 0;
        phoneNumber[1] = 0;
        phoneNumber[2] = 0;
    }

    public void SetPhoneNumber(int nuber)
    {
        phoneNumber[0] = phoneNumber[1];
        phoneNumber[1] = phoneNumber[2];
        phoneNumber[2] = nuber;
        if (phoneNumber[0] == 9 && (phoneNumber[1] == 1) && phoneNumber[2] == 1)
        {
            StopFlood();
        }
    }

    public void ShowPhonePanel()
    {
        //Context.SoundPlayer.PlaySound("panic_3", AudioMixerController.MasterMixerGroup.SFX, 1);
        _phonePanel.SetActive(true);
    }

    public void StartFlood()
    {
        if (!isFlooding)
        {
            isFlooding = true;
            _lightBackground.transform.DOMove( new Vector3(27.57717f, -10f), 17f).SetEase(Ease.Flash).OnComplete(RestartLVL);
        }
    }

    private void StartFinishActions()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Join(_lightBackground.transform.DOMove(new Vector3(_lightBackground.transform.position.x, _lightBackground.transform.position.y-20f), 0.5f)).OnComplete(ShowCoolBtn);
        all.ForEach(obj => { obj.DOFade(0, 0.2f).SetEase(Ease.Linear); });
    }

    private void ShowCoolBtn()
    {
        _phonePanel.SetActive(false);
        Context.SoundPlayer.StopPlayingSounds();
        Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
    }
    
    public void StopFlood()
    {
        DOTween.Kill(_lightBackground.transform);
        _handSet.GetComponent<Call911HandsetController>().StopDrop();
        StartFinishActions();
    }

    public void RestartLVL()
    {
        Context.SoundPlayer.StopPlayingSounds();
        Debug.Log("Рестарт лвл");
        _handSet.transform.position = new Vector3(-0.02f, 1.06f);
        _handSet.GetComponent<Call911HandsetController>().RestartHandset();
        _phonePanel.SetActive(false);
        DOTween.Kill(_lightBackground.transform);
        _lightBackground.transform.position = new Vector3(-13.05f, -23.42f);
        isFlooding = false;
        
    }
}