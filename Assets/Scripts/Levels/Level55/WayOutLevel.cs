using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.UI;

public class WayOutLevel : MonoBehaviour
{
    [SerializeField] private GameObject textFind;
    [SerializeField] private Button fakeBtn;
    [SerializeField] private GameObject canvas;
    private GameObject backBtn;

    private void Awake()
    {
        canvas.SetActive(true);
    }

    private void Start()
    {
        backBtn = GameObject.FindWithTag("backBtn");
        backBtn.SetActive(false);
        fakeBtn.onClick.AddListener(() =>
        {
            Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
            textFind.SetActive(false);
            Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
            backBtn.SetActive(true);
            fakeBtn.gameObject.SetActive(false);
        });
    }
}
